using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public ScoreManager scoreman;
    public bool[] cpcooldown=new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        cpcooldown[0]=false;
        cpcooldown[1]=false;
        cpcooldown[2]=false;
        cpcooldown[3]=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        PlayerController playercon=other.GetComponent<PlayerController>();
        if(playercon!=null){
            if(!cpcooldown[playercon.playerNumber]){
                CheckpointController cpc=scoreman.checkpointmanager[playercon.playerNumber].GetComponent<CheckpointController>();
                cpcooldown[playercon.playerNumber]=true;
            }
            scoreman.checkpointmanager[playercon.playerNumber].GetComponent<CheckpointController>().lap+=1;
            if(scoreman.rank[playercon.playerNumber]>300f){
                scoreman.finish(playercon.playerNumber);
                playercon.rb.gameObject.SetActive(false);
                playercon.Normal.GetChild(0).gameObject.SetActive(false);
                playercon.transform.parent.GetComponentInChildren<ShootingController>().enabled=false;
            }
        }
    }
}
