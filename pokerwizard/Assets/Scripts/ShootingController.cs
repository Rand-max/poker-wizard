using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class ShootingController : MonoBehaviour
{
    public float maxdistance;
    public SpellData CurrentSpell;
    public ScoreManager scoreManager;
    public GamePlayUIMulti mirrorController;
    public GameObject playernormal;
    public GameObject animateplayer;
    public GameObject wandposition;
    public List<GameObject> enemy;
    public GameObject friend;
    public LayerMask mousecolliderlayermask;
    public LayerMask enemyLayer;
    public LayerMask FriendLayer;
    public GameObject bullet;
    public GameObject bulleteffect;
    public GameObject bulletendeffect;
    public GameObject aim;
    public CinemachineVirtualCamera cvm;
    public float cooldowntime;
    public float bulletspeed;
    public float preparetime;
    public int ammo=0;
    private float cooldown;
    float casttimer=0f;
    bool shootprepared=false;
    bool iscasted=false;
    public ScrollDown SD;
    public AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=playernormal.transform.position;
        //Cursor.lockState=CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if(!am){
            am=FindObjectOfType<AudioManager>();
        }
        if(iscasted){
            casttimer+=Time.deltaTime;
        }
        int selfnumber=transform.parent.GetComponentsInChildren<PlayerController>()[0].playerNumber;
        if(iscasted&&!shootprepared&&cooldown<=0&&CurrentSpell!=null&&!mirrorController.isUnDissolving[selfnumber]&&!mirrorController.counting[selfnumber]&&!mirrorController.isDissolving[selfnumber]&&ammo>0&&casttimer<0f){
            ammo--;
            cooldown=preparetime;
            animateplayer.GetComponent<Animator>().Play("Armature_shoot",0,0f);
            FindObjectOfType<AudioManager>().Play("female_shoot");
            shootprepared=true;
            if(ammo<1){
                mirrorController.UseSpell(selfnumber);
            }
        }
        if(iscasted&&casttimer<0){
            cvm.gameObject.SetActive(false);
            casttimer=0f;
            iscasted=false;
            aim.SetActive(false);
        }
        if(iscasted&&casttimer>0.2f){
            cvm.gameObject.SetActive(true);
            aim.SetActive(true);
        }
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }
        if(Physics.Raycast(wandposition.transform.position, wandposition.transform.forward,out RaycastHit raycasthit,maxdistance,mousecolliderlayermask)){
            transform.position=raycasthit.point;
        }
        else{
            transform.position=wandposition.transform.position+wandposition.transform.forward*maxdistance;
        }
        if(shootprepared&&cooldown<=0){
            shootprepared=false;
            cooldown=cooldowntime;
            Vector3 aimdir=(transform.position-wandposition.transform.position).normalized;
            GameObject newbullet=Instantiate(bullet,wandposition.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
            for(int i=0;i<CurrentSpell.Bullets.Count;i++){
                if(CurrentSpell.Bullets[i].sounfx!=null)am.Play(CurrentSpell.Bullets[i].sounfx.clip);
                GameObject neweffect=Instantiate(bulleteffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                GameObject boomeffect=Instantiate(bulletendeffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                GameObject newbullets;
                switch(CurrentSpell.Bullets[i].AttachedTarget){
                    default:
                    newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position+CurrentSpell.Bullets[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up));
                    break;
                    case 0:
                    newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position+CurrentSpell.Bullets[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up));
                    break;
                    case 1:
                    newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position+CurrentSpell.Bullets[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),enemy[0].transform);
                    break;
                    case 2:
                    newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position+CurrentSpell.Bullets[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),wandposition.transform);
                    break;
                    case 3:
                    newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position+CurrentSpell.Bullets[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),friend.transform);
                    break;
                }
                if(!newbullets.GetComponent<BulletpProjectile>()){
                    newbullets.AddComponent<BulletpProjectile>();
                }
                newbullets.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.Bullets[i].BulletSpeed;
                newbullets.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.Bullets[i].LifeTime;
                newbullets.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
                newbullets.GetComponent<BulletpProjectile>().enemy=enemy;
                newbullets.GetComponent<BulletpProjectile>().origin=wandposition;
                newbullets.GetComponent<BulletpProjectile>().friend=friend;
                newbullets.GetComponent<BulletpProjectile>().be=CurrentSpell.Bullets[i];
                newbullets.GetComponent<BulletpProjectile>().scoreman=scoreManager;
                for(int j=0;j<CurrentSpell.BulletStartEffects.Count;j++){
                    GameObject neweffects;
                    switch(CurrentSpell.BulletStartEffects[i].AttachedTarget){
                        default:
                        neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletStartEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up));
                        break;
                        case 0:
                        neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletStartEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up));
                        break;
                        case 1:
                        neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletStartEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),enemy[0].transform);
                        break;
                        case 2:
                        neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletStartEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),wandposition.transform);
                        break;
                        case 3:
                        neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletStartEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up),friend.transform);
                        break;
                    }
                    if(!neweffects.GetComponent<BulletpProjectile>()){
                        neweffects.AddComponent<BulletpProjectile>();
                    }
                    neweffects.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.BulletStartEffects[i].BulletSpeed;
                    neweffects.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.BulletStartEffects[i].LifeTime;
                    neweffects.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
                    neweffects.GetComponent<BulletpProjectile>().enemy=enemy;
                    neweffects.GetComponent<BulletpProjectile>().origin=wandposition;
                    neweffects.GetComponent<BulletpProjectile>().friend=friend;
                    neweffects.GetComponent<BulletpProjectile>().be=CurrentSpell.BulletStartEffects[i];
                    neweffects.GetComponent<BulletpProjectile>().selfLayer=mousecolliderlayermask;
                    neweffects.GetComponent<BulletpProjectile>().enemyLayer=enemyLayer;
                    neweffects.GetComponent<BulletpProjectile>().scoreman=scoreManager;
                }
                for(int j=0;j<CurrentSpell.BulletEndEffects.Count;j++){
                    GameObject boomeffects=Instantiate(CurrentSpell.BulletEndEffects[j].Object,newbullet.transform.position+CurrentSpell.BulletEndEffects[i].EffectOffset,Quaternion.LookRotation(aimdir,Vector3.up));
                    if(!boomeffects.GetComponent<BulletpProjectile>()){
                        boomeffects.AddComponent<BulletpProjectile>();
                    }
                    boomeffects.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.BulletEndEffects[i].BulletSpeed;
                    boomeffects.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.BulletEndEffects[i].LifeTime;
                    boomeffects.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
                    boomeffects.GetComponent<BulletpProjectile>().enemy=enemy;
                    boomeffects.GetComponent<BulletpProjectile>().origin=wandposition;
                    boomeffects.GetComponent<BulletpProjectile>().friend=friend;
                    boomeffects.GetComponent<BulletpProjectile>().be=CurrentSpell.BulletEndEffects[i];
                    boomeffects.GetComponent<BulletpProjectile>().selfLayer=mousecolliderlayermask;
                    boomeffects.GetComponent<BulletpProjectile>().enemyLayer=enemyLayer;
                    boomeffects.GetComponent<BulletpProjectile>().scoreman=scoreManager;
                    boomeffects.transform.parent=boomeffect.transform;
                }
                newbullets.GetComponent<BulletpProjectile>().end=boomeffect;
                newbullets.GetComponent<BulletpProjectile>().selfLayer=mousecolliderlayermask;
                newbullets.GetComponent<BulletpProjectile>().enemyLayer=enemyLayer;
                newbullets.GetComponent<BulletpProjectile>().FriendLayer=FriendLayer;
                neweffect.transform.parent=wandposition.transform;
                boomeffect.transform.parent=newbullets.transform;
                neweffect.AddComponent<DeleteSelf>();
                neweffect.GetComponent<DeleteSelf>().duration=CurrentSpell.EffectLifeTime;
                neweffect.SetActive(true);
                boomeffect.AddComponent<DeleteSelf>().duration=CurrentSpell.BoomLifeTime;
                boomeffect.SetActive(false);
            }
        }
    }
    public void OnCast(InputAction.CallbackContext ctx){
        Debug.Log("casted");
        iscasted=ctx.ReadValueAsButton();
        if(!iscasted&&casttimer>0){
            casttimer=-1f;
            iscasted=true;
        }
        if(transform.parent.GetComponentInChildren<PlayerController>().playerCursor)transform.parent.GetComponentInChildren<PlayerController>().playerCursor.GetComponent<CursorKey>().clickcursor();
    }
    public void OnEngine(InputAction.CallbackContext ctx){
        Debug.Log("back");
        if(SD!=null&&SD.isdone){
            foreach (var item in FindObjectsOfType<PlayerController>())
            {
                Destroy(item.transform.parent.gameObject);
            }
            PlayerManager pm=FindObjectOfType<PlayerManager>();
            foreach (var item in pm.Players)
            {
                pm.RemovePlayer(item);
            }
            FindObjectOfType<LoadScene>().LoadtheScene(2);
        }
    }
}
