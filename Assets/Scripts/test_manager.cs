using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;
using System.Text;

public class test_manager : MonoBehaviour
{
    public int num_angles = 1;
    public GameObject text;
    public GameObject input_g;
    public GameObject count_g;
    public GameObject count_label_g;
    public Transform parent;
    public List<GameObject> text_list = new List<GameObject>();
    public List<GameObject> input_list = new List<GameObject>();
    public List<GameObject> count_list = new List<GameObject>();
    public List<GameObject> count_label_list = new List<GameObject>(); 
    public JPS jps;
    public TMP_Text title;
    public UI_Manager manager;
    public string trial_name = "";

    public void Set_Trial_Name(string inp){
        trial_name = inp;
    }
    public void Start(){
        var temp_text = Instantiate(text, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
        var temp_input = Instantiate(input_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
        var temp_count_label = Instantiate(count_label_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
        var temp_count = Instantiate(count_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
        text_list.Add(temp_text);
        input_list.Add(temp_input);
        count_label_list.Add(temp_count_label);
        count_list.Add(temp_count);
    }

    public void Exit_without_saving(){
        manager.Open_Home();
    }
    public void Exit(){
        int num_trials = 0;
        if (num_angles < 1){
            title.color = new Color(255,0,0);
            title.text = "Number of angles must be greater than 0";
        } else if(trial_name == ""){
            title.color = new Color(255,0,0);
            title.text = "Must enter valid trial name";
        } else{
            title.color = new Color(255,255,255);
            title.text = "Create New Test";
            string destination = Application.persistentDataPath + "/trials/"+trial_name;
            string data = "";
            for (int i = 0; i < num_angles; i++){
                for(int j = 0; j < int.Parse(count_list[i].GetComponent<TMP_InputField>().text);j++){
                    data += input_list[i].GetComponent<TMP_InputField>().text + ",";
                    num_trials++;
                }
            }
            data += num_trials.ToString();
            Save(destination, data);
            manager.Open_Home();
        }
    }


    public void spawn(string input){  
        int target = 1 > int.Parse(input) ? 1: int.Parse(input);
        while (target < num_angles){
            text_list[num_angles-1].SetActive(false); 
            input_list[num_angles-1].SetActive(false);
            count_label_list[num_angles-1].SetActive(false); 
            count_list[num_angles-1].SetActive(false);
            num_angles--;
        }
        while (num_angles < target){
            num_angles++;
            if (text_list.Count >= num_angles){
                text_list[num_angles-1].SetActive(true); 
                input_list[num_angles-1].SetActive(true);
                count_label_list[num_angles-1].SetActive(true); 
                count_list[num_angles-1].SetActive(true);
            }
            else{
                var temp_text = Instantiate(text, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
                var temp_input = Instantiate(input_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
                var temp_count_label = Instantiate(count_label_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
                var temp_count = Instantiate(count_g, new Vector3(num_angles * 0f, 0, 0), Quaternion.identity, parent);
                text_list.Add(temp_text);
                input_list.Add(temp_input);
                count_label_list.Add(temp_count_label);
                count_list.Add(temp_count);
            }
        }
    }

    private void Save(string destination, string data){
        FileStream file;
        if(File.Exists(destination)) file = File.OpenWrite(destination);
		else {
            if(!Directory.Exists(Application.persistentDataPath + "/trials/")){
                Directory.CreateDirectory(Application.persistentDataPath + "/trials/");
            }
            file = File.Create(destination);
        }
        file.Write(Encoding.ASCII.GetBytes(data), 0, Encoding.ASCII.GetByteCount(data));
		file.Close();
    }
}
