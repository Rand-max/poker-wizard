using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpProjectile : MonoBehaviour
{
    public SpellData isSpell;
    public SpellData.BulletEffect be;
    private Rigidbody bulletrigid;
    public float lifetime=3;
    public float bspeed=100;
    public GameObject end;
    public LayerMask selfLayer;
    public LayerMask enemyLayer;
    public LayerMask FriendLayer;
    private bool triggered=false;
    // Start is called before the first frame update
    void Start()
    {
        tag="bullet";
        if(GetComponent<Rigidbody>()){
            bulletrigid=GetComponent<Rigidbody>();
        }else{
            gameObject.AddComponent<Rigidbody>();
            bulletrigid=GetComponent<Rigidbody>();
            bulletrigid.useGravity=false;
        }
        if(GetComponent<DeleteSelf>()){
            Destroy(GetComponent<DeleteSelf>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered){
            bulletrigid.velocity=transform.forward*bspeed;
            if(lifetime<0){
                Debug.Log("No hit");
                Destroy(transform.parent.gameObject);
            }
            lifetime-=Time.deltaTime;
        }
        else{
            bulletrigid.velocity=new Vector3(0,0,0);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(be.targettype[2]){
            Debug.Log("No shoot");
        }else if((other.gameObject.layer==0)&&be.targettype[0]){
            if(other.gameObject.layer==0){
                triggered=true;
                Debug.Log("collide with wall");
                if(end!=null){
                    end.SetActive(true);
                }
                Destroy(gameObject,isSpell.BoomLifeTime);
            }
        }else if(other.gameObject.tag=="bullet"&&be.targettype[4]){
            if(((1<<other.gameObject.GetComponent<BulletpProjectile>().enemyLayer)&selfLayer)!=0){
                triggered=true;
                Debug.Log("shield brek");
                if(end!=null){
                    end.SetActive(true);
                }
                Destroy(gameObject,isSpell.BoomLifeTime);
            }
        }else if((((1<<other.gameObject.layer) & enemyLayer) != 0)&&be.targettype[1]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                Debug.Log("HP-1");
                if(end!=null){
                    end.SetActive(true);
                }
                Destroy(gameObject,isSpell.BoomLifeTime);
            }
            
        }else if((((1<<other.gameObject.layer) & FriendLayer) != 0)&&be.targettype[3]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                Debug.Log("friend hit");
                if(end!=null){
                    end.SetActive(true);
                }
                Destroy(gameObject,isSpell.BoomLifeTime);
            }
        }else{
            Debug.Log("a ghast encounter");
        }
    }
}
