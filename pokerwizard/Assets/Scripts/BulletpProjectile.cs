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
    public ScoreManager scoreman;
    public GameObject friend;
    public GameObject origin;
    private bool triggered=false;
    public List<float> multipliers=new List<float>();
    public List<float> multipliers_timer=new List<float>();
    bool alreadyScore=false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager am=FindObjectOfType<AudioManager>();
        if(be.sounfx!=null)am.Play(be.sounfx.clip);
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
        if(be.targettype[2]){
            if(be.HasSlowdown){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(-be.BulletSlowdownRate,be.BulletSlowdownTime);
            }
            if(be.HasConfusion){
                origin.GetComponentInParent<PlayerController>().InvertAxis(be.BulletConfusionTime);
            }
            if(be.HasStun){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(-1,be.BulletStundownTime);
            }
            if(be.IsAccelerator){
                origin.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
            }
            if(be.HasClear){
                origin.GetComponentInParent<PlayerController>().transform.parent.GetComponentInChildren<ShootingController>().mirrorController.UseSpell(origin.GetComponentInParent<PlayerController>().playerNumber);
            }
            Debug.Log("No shoot");
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
                Destroy(gameObject);
            }
            lifetime-=Time.deltaTime;
        }
        else{
            bulletrigid.velocity=new Vector3(0,0,0);
        }
    }
    void FixedUpdate(){
        Vector3 direction=Vector3.forward;
        if(be.targettype[0]){
        }
        if(be.targettype[1]){
            if(be.HasPursuit){
                if(enemy.Count>1){
                    direction=(Vector3.Distance(this.transform.position,enemy[0].transform.position)>Vector3.Distance(this.transform.position,enemy[1].transform.position)?enemy[1].transform.position:enemy[0].transform.position)-transform.position;
                }
                else{
                    direction=enemy[0].transform.position-transform.position;
                }
            }
        }
        if(be.targettype[2]){
            if(be.HasPursuit){
                direction=origin.transform.position-transform.position;
            }
        }
        if(be.targettype[3]){
            if(be.HasPursuit){
                direction=friend.transform.position-transform.position;
            }
        }
        if(be.targettype[4]){
            if(be.HasPursuit){
                //No.
            }
        }
        if(be.HasPursuit){
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, be.PursuitStrength * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other){
        if((other.gameObject.layer==0)&&be.targettype[0]){
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
                if(be.IsShield){
                    //No.
                }
                if(be.IsTeleporter){
                    origin.gameObject.GetComponentInParent<PlayerController>().rb.transform.position=this.transform.position+Vector3.up;
                    Debug.Log(this.transform.position+Vector3.up);
                }
                if(be.IsJoke){
                    //No.
                }
                Debug.Log("collide with wall");
                Terminate();
            }
        }else if(other.gameObject.tag=="bullet"&&be.targettype[4]){
            triggered=true;
            if(be.HasSlowdown){
                other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(be.BulletSlowdownRate,be.BulletSlowdownTime);
            }
            if(be.HasConfusion){
                other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(-1,be.BulletConfusionTime);
            }
            if(be.HasStun){
                other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(-1,be.BulletStundownTime);
            }
            if(be.IsAccelerator){
                other.gameObject.GetComponent<BulletpProjectile>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
            }
            if(be.IsShield){
                Destroy(other.gameObject.GetComponent<BulletpProjectile>().gameObject);
            }
            if(be.HasClear){
                Destroy(other.gameObject.GetComponent<BulletpProjectile>().gameObject);
            }
            if(be.IsTeleporter){
                origin.gameObject.GetComponentInParent<PlayerController>().rb.transform.position=other.transform.position;
            }
            if(be.IsJoke){
                //No.
            }
            Debug.Log("shield brek");
            Terminate();
        }else if((((1<<other.gameObject.layer) & enemyLayer) != 0)&&be.targettype[1]){
            if(other.gameObject.tag=="Player"){
                Debug.Log(be.HasStun);
                triggered=true;
                other.transform.parent.GetComponentInChildren<Animator>().Play("Armature_panic");
                FindObjectOfType<AudioManager>().Play("shocked");
                if(be.HasSlowdown){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(-be.BulletSlowdownRate,be.BulletSlowdownTime);
                }
                if(be.HasConfusion){
                    other.gameObject.GetComponentInParent<PlayerController>().InvertAxis(be.BulletConfusionTime);
                }
                if(be.HasStun){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(-1,be.BulletStundownTime);
                }
                if(be.IsAccelerator){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
                }
                if(be.HasClear){
                    other.gameObject.GetComponentInParent<PlayerController>().transform.parent.GetComponentInChildren<ShootingController>().mirrorController.UseSpell(other.gameObject.GetComponentInParent<PlayerController>().playerNumber);
                }
                if(be.IsTeleporter){
                    Vector3 pos=origin.transform.GetComponentInParent<PlayerController>().transform.position;
                    origin.gameObject.GetComponentInParent<PlayerController>().rb.transform.position=other.transform.position;
                    other.gameObject.GetComponentInParent<PlayerController>().rb.transform.position=pos;
                }
                if(be.IsJoke){
                    other.gameObject.GetComponentInParent<PlayerController>().Joke(be.JokeTime,be.JokeStrength);
                }
                Debug.Log("HP-1");
                if(be.canScore){
                    alreadyScore=true;
                    scoreman.AddScore(origin.GetComponentInParent<PlayerController>().playerNumber,be.score);
                    if(other.gameObject.GetComponentInParent<PlayerController>().isJoked){
                    scoreman.AddScore(origin.GetComponentInParent<PlayerController>().playerNumber,be.score+other.gameObject.GetComponentInParent<PlayerController>().JokeStrength-1);
                }
                }
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
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(-1,be.BulletStundownTime);
                }
                if(be.IsAccelerator){
                    other.gameObject.GetComponentInParent<PlayerController>().MultiplySpeed(be.BulletBurstSpeed,be.BulletBurstTime);
                }
                if(be.HasClear){
                    other.gameObject.GetComponentInParent<PlayerController>().transform.parent.GetComponentInChildren<ShootingController>().mirrorController.UseSpell(other.gameObject.GetComponentInParent<PlayerController>().playerNumber);
                }
                if(be.IsTeleporter){
                    origin.gameObject.GetComponentInParent<PlayerController>().rb.transform.position=other.transform.position;
                }
                if(be.IsJoke){
                    scoreman.AddScore(other.gameObject.GetComponentInParent<PlayerController>().playerNumber,be.JokeStrength);
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
                    case 4:
                    //No.
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
