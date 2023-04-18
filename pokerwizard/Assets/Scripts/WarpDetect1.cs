using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpDetect1 : MonoBehaviour
{
    [SerializeField]
    public VisualEffect warpEffect=null;
    public GameObject tcyl;
    public float cd=5;
    public bool isTrigger;
    public bool warpTrigger;
    public bool stopSig=false;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger=false;
        tcyl.SetActive(false);
        warpEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger){
            warpEffect.Play();
            //FindObjectOfType<AudioManager>().Play("fire_cat");
            tcyl.SetActive(true);
            cd-=Time.deltaTime;
            if(cd<0){
                cd=0;
                stopSig=true;
            }
        }
        if(stopSig){
            warpEffect.Stop();
            tcyl.SetActive(false);
            stopSig=false;
        }
    }
    //play par while in trigger,stop while out trigger
    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.GetComponent<PlayerController>()!=null){
            isTrigger=true;
        }
    }
    void OnTriggerExit(Collider collision) {
        cd=5;
    }
}
