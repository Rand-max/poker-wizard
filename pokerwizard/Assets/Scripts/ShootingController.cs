using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    public float maxdistance;
    public SpellData CurrentSpell;
    public GamePlayUIMulti mirrorController;
    public GameObject playernormal;
    public GameObject animateplayer;
    public GameObject wandposition;
    public LayerMask mousecolliderlayermask;
    public LayerMask enemyLayer;
    public LayerMask FriendLayer;
    public GameObject bullet;
    public GameObject bulleteffect;
    public GameObject bulletendeffect;
    public float cooldowntime;
    public float bulletspeed;
    public float preparetime;
    public int ammo=0;
    private float cooldown;
    bool shootprepared=false;
    bool iscasted=false;
    // Start is called before the first frame update
    void Start()
    {
        bullet=new GameObject("Bullet");
        bulleteffect=new GameObject("Effect");
        bulletendeffect=new GameObject("Boom");
        transform.position=playernormal.transform.position;
        //Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        int selfnumber=transform.parent.GetComponentsInChildren<PlayerController>()[0].playerNumber;
        if(iscasted&&!shootprepared&&cooldown<=0&&CurrentSpell!=null&&!mirrorController.isUnDissolving[selfnumber]&&!mirrorController.counting[selfnumber]&&!mirrorController.isDissolving[selfnumber]&&ammo>0){
            ammo--;
            cooldown=preparetime;
            animateplayer.GetComponent<Animator>().Play("Armature_shoot",0,0f);
            shootprepared=true;
            if(ammo<1){
            mirrorController.UseSpell(selfnumber);
            }
        }
        /*if(Mouse.current.leftButton.isPressed&&!shootprepared&&cooldown<=0){
            cooldown=preparetime;
            animateplayer.GetComponent<Animator>().Play("Armature_shoot",0,0f);
            shootprepared=true;
        }*/
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
                GameObject neweffect=Instantiate(bulleteffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                GameObject boomeffect=Instantiate(bulletendeffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                GameObject newbullets=Instantiate(CurrentSpell.Bullets[i].Object,wandposition.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                if(!newbullets.GetComponent<BulletpProjectile>()){
                    newbullets.AddComponent<BulletpProjectile>();
                }
                newbullets.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.Bullets[i].BulletSpeed;
                newbullets.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.Bullets[i].LifeTime;
                newbullets.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
                newbullets.transform.parent=newbullet.transform;
                for(int j=0;j<CurrentSpell.BulletStartEffects.Count;j++){
                    GameObject neweffects=Instantiate(CurrentSpell.BulletStartEffects[j].Object,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                    if(!neweffects.GetComponent<BulletpProjectile>()){
                        neweffects.AddComponent<BulletpProjectile>();
                    }
                    neweffects.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.BulletStartEffects[i].BulletSpeed;
                    neweffects.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.BulletStartEffects[i].LifeTime;
                    neweffects.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
                    neweffects.transform.parent=neweffect.transform;
                }
                for(int j=0;j<CurrentSpell.BulletEndEffects.Count;j++){
                    GameObject boomeffects=Instantiate(CurrentSpell.BulletEndEffects[j].Object,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
                    if(!boomeffects.GetComponent<BulletpProjectile>()){
                        boomeffects.AddComponent<BulletpProjectile>();
                    }
                    boomeffects.GetComponent<BulletpProjectile>().bspeed=CurrentSpell.BulletEndEffects[i].BulletSpeed;
                    boomeffects.GetComponent<BulletpProjectile>().lifetime=CurrentSpell.BulletEndEffects[i].LifeTime;
                    boomeffects.GetComponent<BulletpProjectile>().isSpell=CurrentSpell;
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
    }
}
