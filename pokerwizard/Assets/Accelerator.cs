using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float acceleratio;
    public Vector3 forward;
    // Start is called before the first frame update
    void Start()
    {
        forward=transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.GetComponent<PlayerController>()!=null){
            collision.gameObject.GetComponent<PlayerController>().SingleDirection(acceleratio,forward);
        }
    }
}
