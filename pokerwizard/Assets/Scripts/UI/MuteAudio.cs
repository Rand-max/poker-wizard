using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    public GameObject musicCheckbox;
    public bool musicMuted=false;
    public GameObject soundCheckbox;
    public bool soundMuted=false;
    public bool musicplay;
    public void Start() {
        musicplay=true;
    }
    public void MuteSound(bool muted){
        if(muted){
            AudioListener.volume=1;
            soundCheckbox.gameObject.SetActive(true);
        }else{
            AudioListener.volume=0;
            soundCheckbox.gameObject.SetActive(false);
        }
    }
    public void MuteMusic(bool muted){
        if(muted){
            musicplay=true;
            musicCheckbox.gameObject.SetActive(true);
        }else{
            musicplay=false;
            musicCheckbox.gameObject.SetActive(false);
        }
    }
}
