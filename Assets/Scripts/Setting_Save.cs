using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Save : MonoBehaviour
{
    public JPS script;
    public void save_target_angular_uncertainty(string inp){
        PlayerPrefs.SetFloat("target_angular_uncertainty",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_relaxed_angular_uncertainty(string inp){
        PlayerPrefs.SetFloat("relaxed_angular_uncertainty",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_position_hold_time(string inp){
        PlayerPrefs.SetFloat("position_hold_time",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_initial_zero_hold_time(string inp){
        PlayerPrefs.SetFloat("initial_zero_hold_time",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_return_zero_hold_time(string inp){
        PlayerPrefs.SetFloat("return_zero_hold_time",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_angular_velocity_averaging_time(string inp){
        PlayerPrefs.SetFloat("angular_velocity_averaging_time",float.Parse(inp));
        PlayerPrefs.Save();
    }
    public void save_angular_velocity_uncertainty(string inp){
        PlayerPrefs.SetFloat("angular_velocity_uncertainty",float.Parse(inp));
        PlayerPrefs.Save();
    }

    public void save_reposition_hold_time(string inp){
        PlayerPrefs.SetFloat("reposition_hold_time",float.Parse(inp));
        PlayerPrefs.Save();
    }

    public void OnApplicationPause(bool paused){
        if (paused){
            return;
        }else{
            script.target_angular_uncertainty = PlayerPrefs.GetFloat("target_angular_uncertainty", script.target_angular_uncertainty);
            script.relaxed_angular_uncertainty = PlayerPrefs.GetFloat("relaxed_angular_uncertainty", script.relaxed_angular_uncertainty);
            script.position_hold_time = PlayerPrefs.GetFloat("position_hold_time", script.position_hold_time);
            script.initial_zero_hold_time = PlayerPrefs.GetFloat("initial_zero_hold_time", script.initial_zero_hold_time);
            script.return_zero_hold_time = PlayerPrefs.GetFloat("return_zero_hold_time", script.return_zero_hold_time);
            script.angular_velocity_averaging_time = PlayerPrefs.GetFloat("angular_velocity_averaging_time", script.angular_velocity_averaging_time);
            script.reposition_hold_time = PlayerPrefs.GetFloat("reposition_hold_time", script.reposition_hold_time);
            script.angular_velocity_uncertainty = PlayerPrefs.GetFloat("angular_velocity_uncertainty", script.angular_velocity_uncertainty);
        }
    }
}

