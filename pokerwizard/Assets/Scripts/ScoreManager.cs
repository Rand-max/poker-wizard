using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> checkpointmanager;
    public List<int> leaderboard;
    public List<float> rank;
    public List<float> sortedrank;
    public List<int>ShootPoint;
    public List<TextMeshProUGUI>shootpointui;
    public List<int>gold;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            rank[i]=checkpointmanager[i].GetComponent<CheckpointController>().lap*100+checkpointmanager[i].GetComponent<CheckpointController>().activeindex+1-checkpointmanager[i].GetComponent<CheckpointController>().distance/1000;
        }
        sortedrank=new List<float>(rank);
        sortedrank.Sort();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(rank[i]==sortedrank[j]){
                    leaderboard[i]=4-j;
                }
            }
        }
    }
    public void AddScore(int playernum,int score){
        ShootPoint[playernum]+=score;
        shootpointui[0].text=(ShootPoint[0]+ShootPoint[1]).ToString();
        shootpointui[1].text=(ShootPoint[2]+ShootPoint[3]).ToString();
    }
}
