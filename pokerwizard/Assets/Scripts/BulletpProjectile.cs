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
    public List<float> multipliers;
    public List<float> multipliers_timer;
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
            for(int i=0;i<multipliers.Count;i++){
                bulletrigid.velocity*=multipliers[i];
                multipliers_timer[i]-=Time.deltaTime;
                if(multipliers_timer[i]<=0){
                    multipliers_timer.RemoveAt(i);
                    multipliers.RemoveAt(i);
                }
            }
            if(lifetime<0){
                Debug.Log("No hit");
                Destroy(transform.parent.gameObject);
            }
            lifetime-=Time.deltaTime;
        }
        else{
            bulletrigid.velocity=new Vector3(0,0,0);
        }
        if(be.targettype[0]){
        }
        if(be.targettype[1]){
            if(be.HasPursuit){
                if(enemy.Count>1){
                    transform.LookAt(Vector3.Distance(this.transform.position,enemy[0].transform.position)>Vector3.Distance(this.transform.position,enemy[1].transform.position)?enemy[1].transform:enemy[0].transform);
                }
                else{
                    transform.LookAt(enemy[0].transform);
                }
            }
        }
        if(be.targettype[2]){
            if(be.HasPursuit){
                transform.LookAt(origin.transform);
            }
        }
        if(be.targettype[3]){
            if(be.HasPursuit){
                transform.LookAt(friend.transform);
            }
        }
        if(be.targettype[4]){
            if(be.HasPursuit){
                //No.
            }
        }
        
    }
    private void OnTriggerEnter(Collider other){
        if(be.targettype[2]){
            if(be.HasSlowdown){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(-be.BulletSlowdownRate,be.BulletSlowdownTime);
            }
            if(be.HasConfusion){
                origin.GetComponentInParent<PlayerController>().InvertAxis(be.BulletConfusionTime);
            }
            if(be.HasStun){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(0,be.BulletStundownTime);
            }
            if(be.IsAccelerator){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
            }
            Debug.Log("No shoot");
        }else if((other.gameObject.layer==0)&&be.targettype[0]){
            if(other.gameObject.layer==0){
                triggered=true;
                if(be.HasSlowdown){
                    //No.
                }
                if(be.HasConfusion){
                    //No.
                }
                if(be.HasStun){
                    //No.
                }
                if(be.IsAccelerator){
                    //No.
                }
                Debug.Log("collide with wall");
                Terminate();
            }
        }else if(other.gameObject.tag=="bullet"&&be.targettype[4]){
            if(((1<<other.gameObject.GetComponent<BulletpProjectile>().enemyLayer)&selfLayer)!=0){
                triggered=true;
                if(be.HasSlowdown){
                    other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(be.BulletSlowdownRate,be.BulletSlowdownTime);
                }
                if(be.HasConfusion){
                    other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(-1,be.BulletConfusionTime);
                }
                if(be.HasStun){
                    other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(0,be.BulletStundownTime);
                }
                if(be.IsAccelerator){
                    other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
                }
                Debug.Log("shield brek");
                Terminate();
            }
        }else if((((1<<other.gameObject.layer) & enemyLayer) != 0)&&be.targettype[1]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                if(be.HasSlowdown){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(-be.BulletSlowdownRate,be.BulletSlowdownTime);
                }
                if(be.HasConfusion){
                    other.gameObject.GetComponentInParent<PlayerController>().InvertAxis(be.BulletConfusionTime);
                }
                if(be.HasStun){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(0,be.BulletStundownTime);
                }
                if(be.IsAccelerator){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
                }
                Debug.Log("HP-1");
                Terminate();
            }
            
        }else if((((1<<other.gameObject.layer) & FriendLayer) != 0)&&be.targettype[3]){
            if(other.gameObject.tag=="Player"){
                triggered=true;
                if(be.HasSlowdown){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(-be.BulletSlowdownRate,be.BulletSlowdownTime);
                }
                if(be.HasConfusion){
                    other.gameObject.GetComponentInParent<PlayerController>().InvertAxis(be.BulletConfusionTime);
                }
                if(be.HasStun){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(0,be.BulletStundownTime);
                }
                if(be.IsAccelerator){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
                }
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
    public void MultiplySpeed(float rate,float time){
        multipliers.Add(1+rate);
        multipliers_timer.Add(time);
    }
}
