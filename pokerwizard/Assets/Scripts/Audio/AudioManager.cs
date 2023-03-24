using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    //public string[] bgms;
    
    public bool spinMusic=false;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake() {
        if(instance==null){
            instance=this;
        }else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
            s.source.loop=s.loop;
        }
    }
    void Start(){
        Play("bgm4");
    }
    // private void Update() {
    //     if(spinMusic){
    //         Play(bgms[0]);
    //     }
    // }
    public void Play(string name){
        Sound s=Array.Find(sounds,sound => sound.name==name);
        if(s==null){
            Debug.LogWarning("Sound: "+name+" not found");
            return;
        }
        s.source.Play();
    }
    public void Play(AudioClip clip){
        Sound s=Array.Find(sounds,sound => sound.clip==clip);
        if(s==null){
            Debug.LogWarning("Sound: "+clip.name+" not found");
            return;
        }
        s.source.Play();
    }
    public void PlayDelayed(string name,float sec){
        Sound s=Array.Find(sounds,sound => sound.name==name);
        if(s==null){
            Debug.LogWarning("Sound: "+name+" not found");
            return;
        }
        s.source.PlayDelayed(sec);
    }
    //play random bgm
    /*public void PlayRand(){
      
    }*/
}
