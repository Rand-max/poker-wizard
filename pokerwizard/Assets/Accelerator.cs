using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float acceleratio;
    public float multiplies;
    public float lasttime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.GetComponent<PlayerController>()!=null){
            collision.gameObject.GetComponent<PlayerController>().SingleDirection(acceleratio,transform.forward);
            collision.gameObject.GetComponent<PlayerController>().MultiplySpeed(multiplies,lasttime);
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color=Color.white;
        Gizmos.DrawRay(transform.position,transform.forward);
    }
}
