using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public float lap;
    public float distance;
    public float simulationspeed=20;
    public GameObject player;
    private float innertimer;
    public GameObject[] Checkpoints;
    public int activeindex=0;
    // Start is called before the first frame update
    void Start()
    {
        innertimer=0;
        lap=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(innertimer>1/simulationspeed){
            innertimer=0;
            distance=Vector3.Distance(player.transform.position,GetComponentInParent<CheckpointController>().Checkpoints[activeindex].transform.position);
        }
        innertimer+=Time.deltaTime;
    }
}
