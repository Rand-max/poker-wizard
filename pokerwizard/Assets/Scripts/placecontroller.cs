using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class placecontroller : MonoBehaviour
{
    public int playercount;
    public GameObject score;
    public TextMeshProUGUI placeeng;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int place=score.GetComponent<ScoreManager>().leaderboard[playercount];
        switch(place-1){
            case 0:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                placeeng.text="st";
                break;
            case 1:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                placeeng.text="nd";
                break;
            case 2:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                placeeng.text="rd";
                break;
            case 3 :
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                placeeng.text="th";
                break;
            default:
                break;
        }
    }
}
