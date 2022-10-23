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
    public List<GameObject>enemy;
    public LayerMask FriendLayer;
    public GameObject friend;
    public GameObject origin;
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
                Terminate();
            }
        }else if(other.gameObject.tag=="bullet"&&be.targettype[4]){
            if(((1<<other.gameObject.GetComponent<BulletpProjectile>().enemyLayer)&selfLayer)!=0){
                triggered=true;
                Debug.Log("shield brek");
                Terminate();
            }
        }else if((((1<<other.gameObject.layer) & enemyLayer) != 0)&&be.targettype[1]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                Debug.Log("HP-1");
                Terminate();
            }
            
        }else if((((1<<other.gameObject.layer) & FriendLayer) != 0)&&be.targettype[3]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                Debug.Log("friend hit");
                Terminate();
            }
        }else{
            Debug.Log("a ghast encounter");
        }
    }
    public void Terminate(){
        if(end!=null){
            foreach (SpellData.BulletEffect bee in isSpell.BulletEndEffects)
            {
                switch(bee.AttachedTarget){
                    case 1:
                    end.transform.parent=enemy[0].transform;
                    break;
                    case 2:
                    end.transform.parent=origin.transform;
                    break;
                    case 3:
                    end.transform.parent=friend.transform;
                    break;
                }
            }
            end.SetActive(true);
        }
        Destroy(gameObject,isSpell.BoomLifeTime);
    }
}
