using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireTrigger : MonoBehaviour
{
    [SerializeField]
    public VisualEffect fire_breath=null;
    public bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger){
            fire_breath.Play();
            isTrigger=false;
        }
    }
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.GetComponent<PlayerController>()!=null){
            isTrigger=true;
        }
    }
}
