using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContainer : MonoBehaviour
{
    public ScoreManager scoreManager;
    public List<int> rank;
    public List<int> hitpoint;
    public List<int> gold;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveScore(){
        for (int i = 0; i < 4; i++)
        {
            rank=scoreManager.leaderboard;
            hitpoint=scoreManager.ShootPoint;
            gold=scoreManager.gold;
        }
    }
}
