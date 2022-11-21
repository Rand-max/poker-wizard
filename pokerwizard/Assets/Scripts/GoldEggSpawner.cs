using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoldEggSpawner : MonoBehaviour
{
    public GameObject gold;
    public List<GameObject> players;
    public float AlarmDistance;
    public List<Transform>spawnPoints;
    public float arearange;
    public float cooldown;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn(int amount){
        if(players.Count<1){
            foreach (var item in GetComponent<ScoreManager>().checkpointmanager)
            {
                players.Add(item.GetComponent<CheckpointController>().player);
            }
        }
        for (int i = 0; i < amount; i++)
        {
            Vector2 randomvec=Random.insideUnitCircle*arearange;
            Vector3 spawnp=spawnPoints[Random.Range(0,spawnPoints.Count)].position+new Vector3(randomvec.x,0f,randomvec.y);
            RaycastHit hit;
            // note that the ray starts at 100 units
            Ray ray = new Ray (spawnp + Vector3.up * 12, Vector3.down);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {        
                if (hit.collider != null) {
                    // this is where the gameobject is actually put on the ground
                    spawnp = new Vector3 (spawnp.x, hit.point.y + 0.3f, spawnp.z);
                }
            }
            GameObject golde=Instantiate(gold,spawnp,Quaternion.identity);
            golde.GetComponent<GoldenEgg>().players=players;
            golde.GetComponent<GoldenEgg>().AlarmDistance=AlarmDistance;
            golde.GetComponent<GoldenEgg>().spawnPoint=spawnp;
            golde.GetComponent<GoldenEgg>().arearange=arearange;
            golde.GetComponent<GoldenEgg>().cooldown=cooldown;
        }
    }
}
