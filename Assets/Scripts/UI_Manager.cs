using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Home;
    public GameObject Running;
    public GameObject Create;
    public GameObject Use_Test;
    public JPS script;
    public TestSelector ts;
    public TMP_InputField trial_name_text;
    public TMP_InputField target_angular_uncertainty_text;
    public TMP_InputField relaxed_angular_uncertainty_text;
    public TMP_InputField position_hold_time_text;
    public TMP_InputField reposition_hold_time_text;
    public TMP_InputField initial_zero_hold_time_text;
    public TMP_InputField return_zero_hold_time_text;
    public TMP_InputField angular_velocity_averaging_time_text;
    public TMP_InputField angular_velocity_uncertainty_text;

    public void Open_Setting(){
        Setting.SetActive(true);
        Home.SetActive(false);
        Running.SetActive(false);
        Create.SetActive(false);
        target_angular_uncertainty_text.text = script.target_angular_uncertainty.ToString();
        relaxed_angular_uncertainty_text.text = script.relaxed_angular_uncertainty.ToString();
        position_hold_time_text.text = script.position_hold_time.ToString();
        reposition_hold_time_text.text = script.reposition_hold_time.ToString();
        initial_zero_hold_time_text.text = script.initial_zero_hold_time.ToString();
        return_zero_hold_time_text.text = script.return_zero_hold_time.ToString();
        angular_velocity_averaging_time_text.text = script.angular_velocity_averaging_time.ToString();
        angular_velocity_uncertainty_text.text =script.angular_velocity_uncertainty.ToString();
    }

    public void Open_Home(){
        Setting.SetActive(false);
        Home.SetActive(true);
        Running.SetActive(false);
        Create.SetActive(false);
        Use_Test.SetActive(false);
    }

    public void Open_Running(){
        Setting.SetActive(false);
        Home.SetActive(false);
        Running.SetActive(true);
        Create.SetActive(false);
        Use_Test.SetActive(false);
    }

    public void Open_Create(){
        Setting.SetActive(false);
        Home.SetActive(false);
        Running.SetActive(false);
        Create.SetActive(true);
        Use_Test.SetActive(false);
    }

    public void Open_Use_Test(){
        Setting.SetActive(false);
        Home.SetActive(false);
        Running.SetActive(false);
        Create.SetActive(false);
        Use_Test.SetActive(true);
        ts.Load();
    }

    public void Load(){
        string destination = Application.persistentDataPath + "/trials";
        DirectoryInfo DirInfo = new DirectoryInfo(destination);
        foreach (var f in DirInfo.EnumerateFiles()){
            Debug.Log(f.Name);
        }
    }
}