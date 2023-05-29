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
        cpcooldown[0]=true;
        cpcooldown[1]=true;
        cpcooldown[2]=true;
        cpcooldown[3]=true;
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
                cpc.lap+=1;
                scoreman.CalculateRank();
                if(cpc.lap>=3){
                    if(playercon.GetComponentInChildren<Animator>()){
                        if(playercon.GetComponentInChildren<Animator>().GetBehaviour<DriftController>()){
                            if(playercon.GetComponentInChildren<Animator>().GetBehaviour<DriftController>().driftPar){
                                playercon.GetComponentInChildren<Animator>().GetBehaviour<DriftController>().driftPar.Stop();
                            }
                        }
                    }
                    FindObjectOfType<AudioManager>().StopPlaying("drift4");
                    ShootingController shootingController=playercon.transform.parent.GetComponentInChildren<ShootingController>();
                    shootingController.cvm.gameObject.SetActive(false);
                    shootingController.casttimer=0f;
                    shootingController.iscasted=false;
                    shootingController.aim.SetActive(false);
                    shootingController.enabled=false;
                    scoreman.finish(playercon.playerNumber);
                    playercon.rb.gameObject.SetActive(false);
                    playercon.Normal.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
