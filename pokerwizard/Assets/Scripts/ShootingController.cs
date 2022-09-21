using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    public float maxdistance;
    public GameObject playernormal;
    public GameObject animateplayer;
    public GameObject wandposition;
    public LayerMask mousecolliderlayermask;
    public GameObject bullet;
    public GameObject bulleteffect;
    public GameObject bulletendeffect;
    public float cooldowntime;
    public float bulletspeed;
    public float preparetime;
    private float cooldown;
    bool shootprepared=false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=playernormal.transform.position;
        //Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        animateplayer=playernormal.GetComponentInChildren<Animator>().transform.gameObject;
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }
        if(Physics.Raycast(wandposition.transform.position, wandposition.transform.TransformDirection(wandposition.transform.forward),out RaycastHit raycasthit,maxdistance,mousecolliderlayermask)){
            transform.position=raycasthit.point;
        }
        else{
            transform.position=wandposition.transform.position+wandposition.transform.forward*maxdistance;
        }
        if(Mouse.current.leftButton.isPressed&&!shootprepared&&cooldown<=0){
            cooldown=preparetime;
            animateplayer.GetComponent<Animator>().Play("Armature_shoot",0,0f);
            shootprepared=true;
        }
        if(shootprepared&&cooldown<=0){
            shootprepared=false;
            cooldown=cooldowntime;
            Vector3 aimdir=(transform.position-wandposition.transform.position).normalized;
            GameObject newbullet=Instantiate(bullet,wandposition.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
            GameObject neweffect=Instantiate(bulleteffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
            GameObject boomeffect=Instantiate(bulletendeffect,newbullet.transform.position,Quaternion.LookRotation(aimdir,Vector3.up));
            newbullet.GetComponent<BulletpProjectile>().end=boomeffect;
            newbullet.GetComponent<BulletpProjectile>().bspeed=bulletspeed;
            newbullet.GetComponent<BulletpProjectile>().layer=mousecolliderlayermask;
            neweffect.transform.parent=wandposition.transform;
            boomeffect.transform.parent=newbullet.transform;
            neweffect.SetActive(true);
            boomeffect.SetActive(false);
        }
    }
}
