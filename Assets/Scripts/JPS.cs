using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;
using System.Text;

public class JPS : MonoBehaviour
{
    public float target_angular_uncertainty = 2f;
    public float relaxed_angular_uncertainty = 10f;
    public float position_hold_time = 3f;
    public float reposition_hold_time = 1f;
    public float initial_zero_hold_time = 3f;
    public float return_zero_hold_time = 2f;
    public float angular_velocity_averaging_time = 0.25f;
    public float angular_velocity_uncertainty = 5f; 
    public int user_id;
    
    public int num_trials = 1;
    public int curr_trial = 0;
    public int[] target_angles = {45, 35};
    float clock = 0;

    bool running = false;
    public string state = "initial";
    public TMP_Text text;
    public Audio audio;
    public List<float> angles = new List<float>();
    public List<string> markers = new List<string>();
    public Vector3 downwards = new Vector3(0,1,0);

    public GameObject buttonWithout;
    public GameObject buttonWith;

    public void Set_Trial_From_Name(string trial_name){
        string trial_dest = Application.persistentDataPath + "/trials/"+trial_name; 
        StreamReader sr = new StreamReader(trial_dest);
        string line = sr.ReadLine();
        string[] values = line.Split(",");
        target_angles = new int[values.Length-1];
        for (int i = 0; i < values.Length-1; i++){
            target_angles[i] = int.Parse(values[i]);
        }
        Debug.Log(target_angles);
    }

    public void Return_to_menu(){
        state = "initial";
        audio.Stop();
        downwards = new Vector3(0,1,0);
        angles = new List<float>();
        markers = new List<string>();
        running = false;
        clock = 0;
        buttonWith.SetActive(true);
        buttonWithout.SetActive(true);

    }
    public void start_without_zero(){
        state = "start";
        buttonWith.SetActive(false);
        buttonWithout.SetActive(false);
    }

    public void start_with_zero(){
        if (GravitySensor.current.enabled){
            var gravity = GravitySensor.current.gravity.ReadValue();
            downwards = gravity;
        }
        state = "start";
        buttonWith.SetActive(false);
        buttonWithout.SetActive(false);
    }

    public void update_user_id(string inp){
        user_id = int.Parse(inp);
        Random.InitState(user_id);
    }
    public void update_target_angular_uncertainty(string inp){
        target_angular_uncertainty = float.Parse(inp);
        //Debug.Log(target_angular_uncertainty);
    }

    public void update_relaxed_angular_uncertainty(string inp){
        relaxed_angular_uncertainty = float.Parse(inp);
    }

    public void update_position_hold_time(string inp){
        position_hold_time = float.Parse(inp);
    }

    public void update_initial_zero_hold_time(string inp){
        initial_zero_hold_time = float.Parse(inp);
    }

    public void update_return_zero_hold_time(string inp){
        return_zero_hold_time = float.Parse(inp);
    } 

    public void update_angular_velocity_averaging_time(string inp){
        angular_velocity_averaging_time = float.Parse(inp);
    }

    public void update_angular_velocity_uncertainty(string inp){
        angular_velocity_uncertainty = float.Parse(inp);
    }

    public void update_reposition_hold_time(string inp){
        reposition_hold_time = float.Parse(inp);
    }
    public void run(){
        running = true;
        if (SystemInfo.supportsGyroscope){
            InputSystem.EnableDevice(GravitySensor.current);
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        InputSystem.EnableDevice(Accelerometer.current);
        for (int i = num_trials-1; i > 0; i--){
            int k = Random.Range(0, i+1);
            int val = target_angles[k];
            Debug.Log(val);
            target_angles[k] = target_angles[i];
            target_angles[i] = val;
        }
    }
    void Reset(bool finished){
        string destination = Application.persistentDataPath + "/data_"+curr_trial.ToString();
        string data = "";
        for (int i = 0; i < angles.Count; i++){
            data += angles[i].ToString() +","+markers[i]+ "\n";
        }
        Save(destination, data);
        angles = new List<float>();
        markers = new List<string>();
        if (finished){
            state = "finished";
        }else {
            state = "start";
        }
        clock = 0;
    }

    void Save(string destination, string data){
        FileStream file;
        if(File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);
        file.Write(Encoding.ASCII.GetBytes(data), 0, Encoding.ASCII.GetByteCount(data));
		file.Close();
    }
    // Update is callesave_target_angular_uncertaintyd once per frame
    void FixedUpdate()
    {
        try{
            if (running){
                var marker = "";
                float angle=0;
                if (SystemInfo.supportsGyroscope){
                    var gravity = GravitySensor.current.gravity.ReadValue();
                    angle = Vector3.Angle(downwards, gravity);
                    text.text = ("Angle1: " + angle);
                }else{
                    if (Accelerometer.current.enabled){
                        var acceleration = Accelerometer.current.acceleration.ReadValue();
                        angle = Mathf.Atan(Mathf.Sqrt((acceleration[0]*acceleration[0])+(acceleration[2]*acceleration[2]))/acceleration[1])* Mathf.Rad2Deg;
                        text.text = ("Angle2: " + angle+"\n"+"Acceleration"+acceleration);
                    }else{
                        text.text="BADD";
                    }
                }
                angles.Add(angle);

                var velocity = new Vector3(0,1,0);

                if (SystemInfo.supportsGyroscope){
                    velocity = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
                }
                //var gravity = new Vector3(0,1,0);
                //var velocity = new Vector3(0,1,0);
                //float angle_test = Mathf.Atan(Mathf.Sqrt((acceleration[0]*acceleration[0])+(acceleration[2]*acceleration[2]))/acceleration[1])* Mathf.Rad2Deg;
                                //text.text = ("Angle: " + angle);
                switch (state){
                    case "test":
                        //text.text = acceleration.ToString() +'\n' +angle;
                        break;
                    case "start":
                        if (clock == 0){
                            clock = Time.time;
                        }
                        if (Time.time-clock >= initial_zero_hold_time && angle < relaxed_angular_uncertainty){
                            //audio.play_find_target();
                            state = "guided_lift";
                            Debug.Log("here");
                        }
                        text.text = ("Angle: " + angle +"\n Clock" + clock.ToString() + "\n Current trial " + curr_trial.ToString() + "\n Target Angles + " + target_angles.Length.ToString());
                        break;
                    case "guided_lift":
                        if (angle < target_angles[curr_trial] - target_angular_uncertainty){
                            audio.play_low_tone();
                            clock = 0;
                        }
                        else if  (angle > target_angles[curr_trial] + target_angular_uncertainty){
                            audio.play_high_tone();
                            clock = 0;
                        } else{
                            if (clock == 0){
                                clock = Time.time;
                                audio.Stop();
                            }else {
                                if (Time.time - clock >= position_hold_time){
                                    state = "guided_reset";
                                    clock = 0;
                                    marker = "HeldDirected";
                                    audio.play_relax();
                                    //text.text = ("RESET: "+ angle);
                                }
                            }
                        }
                        break;
                    case "guided_reset":
                        if (angle<relaxed_angular_uncertainty){
                            if (clock == 0){
                                clock = Time.time;
                            }else { 
                                if (Time.time - clock >= return_zero_hold_time){
                                    state = "unguided_lift";
                                    clock = 0;
                                    marker = "HeldZero";
                                    audio.play_find_target();
                                }
                            }
                        }else{
                            clock = 0;
                        }
                        break;
                    case "unguided_lift":
                        //text.text = ("Count:" + angles.Count +"\n" + "Index: " + (angles.Count- (int) (angular_velocity_averaging_time*(1/Time.fixedDeltaTime))));
                        var ang_velocity =  (angle-angles[angles.Count- (int) (angular_velocity_averaging_time*(1/Time.fixedDeltaTime))]);
                        //text.text = ("Velocity" + velocity);
                        if (clock == 0 || angle < relaxed_angular_uncertainty){
                                clock = Time.time;
                        }
                        if (ang_velocity < angular_velocity_uncertainty){
                            if (Time.time - clock >=reposition_hold_time){
                                state = "unguided_reset";
                                marker = "HeldUndirected";
                                audio.play_relax();
                            }
                        }
                        break;
                    case "unguided_reset":
                        if (angle<relaxed_angular_uncertainty){
                            if (clock == 0){
                                clock = Time.time;
                            }else { 
                                if (Time.time - clock >= return_zero_hold_time){
                                    state = "RESET";
                                    clock = 0;
                                }
                            }
                        }else{
                            clock = 0;
                        }
                        break;
                    case "RESET":
                        curr_trial++;
                        marker = "HeldZero";
                        if (curr_trial == target_angles.Length) {
                            text.text = "Finished All Trials";
                            markers.Add(marker);
                            Reset(true);
                        }
                        else{
                            markers.Add(marker);
                            Reset(false);
                        }
                        break;
                    case "finished":
                        text.text = "Finished All Trials And Saved Data";
                        break;
                }
                markers.Add(marker);
            }
        }catch (System.Exception e){
            text.text = e.ToString() + curr_trial + target_angles.Length;
            Debug.Log(e);
        }
    }
}
