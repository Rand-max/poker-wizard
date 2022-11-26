using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudTrigger : MonoBehaviour
{
    public bool isTrigger;
    public string AudName;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger){
            FindObjectOfType<AudioManager>().Play(AudName);
            isTrigger=false;
        }
    }
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.GetComponent<PlayerController>()!=null){
            isTrigger=true;
        }
    }
}
