using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GainMagicItem : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem boom=null;
    public GameObject uiMulti;

    public GameObject crown_obj;
    public float respawn_time;
    public bool alive;

    private bool par_start;
    private Vector3 ori_scale;
    // Start is called before the first frame update
    void Start()
    {
        alive=true;
        respawn_time=3f;
        par_start=false;
        ori_scale=crown_obj.transform.localScale;
        Scaleup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            crown_obj.SetActive(false);
            respawn_time-=Time.deltaTime;
            crown_obj.transform.DOScale(0,1f);
        }
        if(par_start){
            boom.Play();
            par_start=false;
        }
        if(respawn_time<0){
            respawn_time=0f;
            alive=true;
            Scaleup();
        }
        if(alive){
            crown_obj.SetActive(true);
            respawn_time=3f;
        }
    }
    void Scaleup(){
        crown_obj.transform.DOScale(ori_scale,1f);
    }
    void OnTriggerEnter(Collider collision){
    if(collision.gameObject.GetComponent<PlayerController>()!=null){
        if(uiMulti.GetComponent<GamePlayUIMulti>().GetSpell(collision.gameObject.GetComponent<PlayerController>().playerNumber)){
            alive=false;
            par_start=true;
            FindObjectOfType<AudioManager>().Play("magic_item");
        }
    }
 }
}