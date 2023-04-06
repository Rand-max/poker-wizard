using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStopper : MonoBehaviour
{
    WarpDetect1 wd;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PlayerController>()!=null){
            wd.warpTrigger=false;
        }
    }
}
