using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanManager : MonoBehaviour
{
    public List<CheckpointController> cpc;
    public Transform startpoint;
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
            CheckpointController playercpc=cpc.Find(x=>x.player==playercon.Normal.gameObject);
            Debug.Log(playercpc);
            if(playercpc.activeindex==0){
                playercon.rb.transform.position=startpoint.position;
            }
            else{
                playercon.rb.transform.position=playercpc.Checkpoints[playercpc.activeindex-1].transform.position;
            }
            playercon.rb.velocity=Vector3.zero;
        }
    }
}
