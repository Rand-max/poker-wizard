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
    public List<bool>finished;
    public LoadScene loadScene;
    public ScoreContainer scoreContainer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            rank[i]=checkpointmanager[i].GetComponent<CheckpointController>().lap*100+checkpointmanager[i].GetComponent<CheckpointController>().activeindex+1-checkpointmanager[i].GetComponent<CheckpointController>().distance/1000f*(finished[i]?4f-leaderboard[i]:1f);
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
    public void finish(int index){
        if(rank[index]>300f){
            finished[index]=true;
        }
        if(finished[0]==finished[1]==finished[2]==finished[3]==true){
            scoreContainer.SaveScore();
            loadScene.LoadtheScene(3);
        }
    }
}
