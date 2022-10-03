using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUIMulti : MonoBehaviour
{
    /*
    icon
        0. fire
        1. accel
        2. shield
        3. ice
        4. chaos
        5. thunder
    */
    /*
    shader property
        _FadeValue : dissolve 0~1
        _MainTex : texture
        _Color : dissolve color
        _NoiseScale
    */
    //spell icon
    public GameObject[] SpellIcon=new GameObject[4];
    public Texture[] si_tex;
    public Color[] si_color;
    Material[] SpellMat=new Material[4];

    //spell text
    public TextMeshProUGUI[] spellname=new TextMeshProUGUI[4];
    public VertexGradient[] st_color;

    //place text
    public TextMeshProUGUI[] placeNum=new TextMeshProUGUI[4];
    public TextMeshProUGUI[] placeEng=new TextMeshProUGUI[4];

    //ripple
    [SerializeField]
    public ParticleSystem[] ripeffect=null;

    //Get Spell
    public void GetSpell(){
        //random spell
        int spellNum=Random.Range(0,6);

        //名次
        int Whichplace=Random.Range(0,4);
        switch(Whichplace){
            case 0:
                placeNum.text="1";
                placeEng.text="st";
                // placeNum.colorGradient=fire_color;
                // placeEng.colorGradient=fire_color;
                break;
            case 1:
                placeNum.text="2";
                placeEng.text="nd";
                // placeNum.colorGradient=ice_color;
                // placeEng.colorGradient=ice_color;
                break;
            case 2:
                placeNum.text="3";
                placeEng.text="rd";
                // placeNum.colorGradient=brown_color;
                // placeEng.colorGradient=brown_color;
                break;
            case 3 :
                placeNum.text="4";
                placeEng.text="th";
                // placeNum.colorGradient=purp_color;
                // placeEng.colorGradient=purp_color;
                break;
            default:
                break;
        }
    }
    //Use Spell
    public void UseSpell(){
        
    }
    void Awake()
    {
        //player material declare
        for(int i=0;i<6;i++){
            SpellMat[i]=SpellIcon[i].GetComponent<Image>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
