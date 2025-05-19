using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Audio : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip find_target;
    public AudioClip relax;
    public AudioClip high_tone;
    public AudioClip low_tone;

    public bool low_tone_playing = false;
    public bool high_tone_playing = false;
    // Start is called before the first frame update

    public void play_find_target(){
        audio.Stop();
        audio.clip = find_target;
        audio.loop = false;
        audio.Play();
    }

    public void play_relax(){
        audio.Stop();
        audio.clip = relax;
        audio.loop = false;
        audio.Play();
    }

    public void play_low_tone(){
        if (!low_tone_playing){
            if (audio.isPlaying){
                if (high_tone_playing == true){
                    high_tone_playing = false;
                    audio.loop = true;
                    audio.Stop();
                    audio.clip = low_tone;
                    audio.Play();
                    low_tone_playing = true;
                }
            }else{
                audio.loop = true;
                audio.clip = low_tone;
                audio.Play();
                low_tone_playing = true;
            }
        }
    }

    public void play_high_tone(){
        if (!high_tone_playing){
            if (audio.isPlaying){
                if (low_tone_playing == true){
                    low_tone_playing = false;
                    audio.loop = true;
                    audio.Stop();
                    audio.clip = high_tone;
                    audio.Play();
                    high_tone_playing = true;
                }
            }else{
                audio.loop = true;
                audio.clip = high_tone;
                audio.Play();
                high_tone_playing = true;
            }
        }
    }

    public void Stop(){
        audio.Stop();
        low_tone_playing = false;
        high_tone_playing = false;
    }
}
