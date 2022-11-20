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
            scoreman.finish(playercon.playerNumber);
            Destroy(playercon.transform.parent.gameObject);
        }
    }
}
