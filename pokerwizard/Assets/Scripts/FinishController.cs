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
        if(playercon!=null&&scoreman.rank[playercon.playerNumber]>300f){
            scoreman.finish(playercon.playerNumber);
            Destroy(playercon.rb);
            Destroy(playercon.Normal.GetChild(0).gameObject);
            Destroy(playercon.transform.parent.GetComponentInChildren<ShootingController>());
        }
    }
}
