using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    public GameObject musicCheckbox;
    public bool musicMuted=false;
    public GameObject soundCheckbox;
    public bool soundMuted=false;
    void Start(){

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
}
