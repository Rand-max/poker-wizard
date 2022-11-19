using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEgg : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem boom=null;
    public GameObject egg;
    public bool alive;
    public float countdown;
    private bool par_start;
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
    }
    void OnTriggerEnter(Collider col){
        if(col.gameObject.GetComponent<PlayerController>()!=null){
            alive=false;
            par_start=true;
        }
    }
}
