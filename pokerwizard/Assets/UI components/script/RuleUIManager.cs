using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RuleUIManager : MonoBehaviour
{
    public RawImage ruleimg;
    public Texture[] ruleTex=new Texture[2];
    public GameObject[] pagedot;

    public TextMeshProUGUI pgtitle;
    public TextMeshProUGUI pgcontent;

    public void BtnNext(){
        ruleimg.texture=ruleTex[1];
        pgtitle.text="計分制";
        pgcontent.text="團隊賽模式為計分制，各自努力為自己的團隊得分唄";
        pagedot[0].SetActive(false);
        pagedot[1].SetActive(true);
    }
    public void BtnPre(){
        ruleimg.texture=ruleTex[0];
        pgtitle.text="競速團隊賽";
        pgcontent.text="本遊戲結合競速與對戰，並採用2v2的團隊賽模式，1p 2p為一隊，3p 4p為一隊";
        pagedot[0].SetActive(true);
        pagedot[1].SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ruleimg.texture=ruleTex[0];
        pagedot[0].SetActive(true);
        pagedot[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
