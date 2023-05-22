using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class placecontroller : MonoBehaviour
{
    public int playercount;
    public GameObject score;
    public SpellActivate spellActivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int place=score.GetComponent<ScoreManager>().leaderboard[playercount];
        switch(place){
            case 0:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                spellActivate.placeEng.text="st";
                GetComponent<TextMeshProUGUI>().colorGradient=spellActivate.fire_color;
                spellActivate.placeEng.colorGradient=spellActivate.fire_color;
                break;
            case 1:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                spellActivate.placeEng.text="nd";
                GetComponent<TextMeshProUGUI>().colorGradient=spellActivate.ice_color;
                spellActivate.placeEng.colorGradient=spellActivate.ice_color;
                break;
            case 2:
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                spellActivate.placeEng.text="rd";
                GetComponent<TextMeshProUGUI>().colorGradient=spellActivate.brown_color;
                spellActivate.placeEng.colorGradient=spellActivate.brown_color;
                break;
            case 3 :
                GetComponent<TextMeshProUGUI>().text=place.ToString();
                spellActivate.placeEng.text="th";
                GetComponent<TextMeshProUGUI>().colorGradient=spellActivate.purp_color;
                spellActivate.placeEng.colorGradient=spellActivate.purp_color;
                break;
            default:
                break;
        }
    }
}
