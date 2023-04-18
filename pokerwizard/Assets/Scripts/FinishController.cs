using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public ScoreManager scoreman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        PlayerController playercon=other.GetComponent<PlayerController>();
        if(playercon!=null){
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
