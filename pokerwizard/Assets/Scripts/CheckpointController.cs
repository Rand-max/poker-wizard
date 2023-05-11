using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap;
    public float distance;
    public float simulationspeed=20;
    public GameObject player;
    private float innertimer;
    public GameObject[] Checkpoints;
    public int activeindex=0;
    public bool passedlastcheckpoint=false;
    // Start is called before the first frame update
    void Start()
    {
        innertimer=0;
        lap=0;
        passedlastcheckpoint=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>1/simulationspeed){
            innertimer=0;
            if(!player)distance=9999;
            else distance=Vector3.Distance(player.transform.position,GetComponentInParent<CheckpointController>().Checkpoints[activeindex].transform.position);
        }
        if(activeindex>=Checkpoints.Length-1&&!passedlastcheckpoint){
            passedlastcheckpoint=true;
        }
        else if(activeindex==1){
            passedlastcheckpoint=false;
        }
        innertimer+=Time.deltaTime;
    }
}
