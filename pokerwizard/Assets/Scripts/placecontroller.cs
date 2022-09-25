using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class placecontroller : MonoBehaviour
{
    public int playercount;
    public GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text=score.GetComponent<ScoreManager>().leaderboard[playercount].ToString();
    }
}
