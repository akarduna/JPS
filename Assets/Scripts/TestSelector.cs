using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;
using System.Text;


public class TestSelector : MonoBehaviour
{
    public GameObject trial_prefab;
    public Transform parent;
    public bool delete = false;
    public TMP_Text text;
    public TMP_Text button_text;
    public void Start(){
        Load();
    }

    public void Load(){
        foreach (Transform child in parent){
            Destroy(child.gameObject);
        }
        string destination = Application.persistentDataPath + "/trials";
        Debug.Log(destination);
        DirectoryInfo DirInfo = new DirectoryInfo(destination);
        foreach (var f in DirInfo.EnumerateFiles()){
            var temp_trial = Instantiate(trial_prefab, new Vector3(0,0,0), Quaternion.identity, parent);
            temp_trial.GetComponent<Trial_Pre_Manger>().Set_Name(f.Name);
        }
    }

    public void Toggle(){
        delete = !delete;
        if (delete){
            text.text = "Select Trial To Delete";
            button_text.text = "Run Trial";
        }else{
            text.text = "Select Trial To Run";
            button_text.text = "Delete Trials";
        }
    }

}
