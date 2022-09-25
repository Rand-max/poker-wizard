using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpProjectile : MonoBehaviour
{
    private Rigidbody bulletrigid;
    public float lifetime=3;
    public float bspeed=100;
    public GameObject end;
    public LayerMask layer= 1<<0;
    private bool triggered=false;
    // Start is called before the first frame update
    void Start()
    {
        bulletrigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered){
            bulletrigid.velocity=transform.forward*bspeed;
            if(lifetime<0){
                Debug.Log("No hit");
                Destroy(gameObject);
            }
            lifetime-=Time.deltaTime;
        }
        else{
            bulletrigid.velocity=new Vector3(0,0,0);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(((1<<other.gameObject.layer) & layer) != 0){
            triggered=true;
            Debug.Log("HP-1");
            if(end!=null){
                end.SetActive(true);
            }
            Destroy(gameObject,0.5f);
        }
    }
}
