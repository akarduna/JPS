using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


//using UnityEngine.InputEventTrace;
using TMPro;

public class test : MonoBehaviour
{
    //float speed = 10.0f;
    public TMP_Text text;
    UnityEngine.InputSystem.Gyroscope gyro;
    public Audio audio;
    //void Start(){
      //  InputSystem.EnableDevice(Gyroscope.current);
    //}

    void Start(){
        gyro = UnityEngine.InputSystem.Gyroscope.current;
        InputSystem.EnableDevice(gyro);
    }

    void Update()
    {
        Vector3 angularVelocity;
        // Get the gyroscope device

        // Check if the device is available and enabled
        if (gyro != null && gyro.enabled)
        {
            // Read the angular velocity
            angularVelocity = gyro.angularVelocity.ReadValue();
            // Print the value to the console
            text.text = ("Gyroscope angular velocity: " + angularVelocity);
        }
    }
    /*
    void FixedUpdate()
    {
        foreach (var e in trace)
        {
            text.text = e.ToString();
        }
        trace.Clear();
        
        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis

        //dir.x = -InputSystem.acceleration.y;
        //dir.z = Input.gravity.x;

        //text.text = dir.z.ToString();
    }*/
}