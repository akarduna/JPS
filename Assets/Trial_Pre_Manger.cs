using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class Trial_Pre_Manger : MonoBehaviour
{
    public string name;
    public TMP_Text label;
    public JPS main_script;
    public UI_Manager ui;
    public TestSelector selecter;
    public void Awake(){
        main_script = GameObject.Find("AudioManager").GetComponent<JPS>();
        ui = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        selecter = GameObject.Find("Use_Test").GetComponent<TestSelector>();
    }
    public void Set_Name(string inp){
        name = inp;
        label.text = inp;
    }

    public void Selected(){
        if (selecter.delete){
            FileStream file;
            Debug.Log("Delete: " + name);
            string trial_dest = Application.persistentDataPath + "/trials/"+name; 
            File.Delete(trial_dest);
            Object.Destroy(this.gameObject);
        }else{
            main_script.Set_Trial_From_Name(name);
            ui.Open_Running();
            main_script.run();
        }
    }
}
