using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoldenEgg : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem boom=null;
    public GameObject egg;
    public bool alive;
    public float countdown;
    private bool par_start;
    public NavMeshAgent nmA;
    public List<GameObject> players;
    public float AlarmDistance;
    public Vector3 spawnPoint;
    public float arearange;
    public bool playerinrange;
    public float cooldown;
    float innertimer=0f;
    // Start is called before the first frame update
    void Start()
    {
        alive=true;
        countdown=2f;
        par_start=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive){
            egg.SetActive(false);
            countdown-=Time.deltaTime;
        }
        if(par_start){
            boom.Play();
            par_start=false;
        }
        if(countdown<0){
            countdown=0;
        }
        if(countdown==0){
            Destroy(this.gameObject);
        }
        playerinrange=false;
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i]&&Vector3.Distance(players[i].transform.position,transform.position)<AlarmDistance){
                playerinrange=true;
            }
        }
        if(playerinrange){
            nmA.acceleration=50f;
            nmA.speed=80f;
            if(innertimer<cooldown/2){
                innertimer+=Time.deltaTime;
            }
            else{
                innertimer=0;
                Vector2 randomvec=Random.insideUnitCircle*arearange;
                nmA.SetDestination(spawnPoint+new Vector3(randomvec.x,0f,randomvec.y));
            }
        }
        else{
            nmA.acceleration=30f;
            nmA.speed=70f;
            if(innertimer<cooldown){
                innertimer+=Time.deltaTime;
            }
            else{
                innertimer=0;
                Vector2 randomvec=Random.insideUnitCircle*arearange;
                nmA.SetDestination(spawnPoint+new Vector3(randomvec.x,0f,randomvec.y));
            }
        }
    }
    void OnTriggerEnter(Collider col){
        if(col.gameObject.GetComponent<PlayerController>()!=null){
            alive=false;
            par_start=true;
            FindObjectOfType<AudioManager>().Play("get_egg");
        }
    }
}
