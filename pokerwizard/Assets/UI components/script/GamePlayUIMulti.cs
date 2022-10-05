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
    //spell icon,4 player val
    public GameObject[] SpellIcon;
    public Texture[] si_tex;
    public Color[] si_color;
    public Material[] SpellMat;
    public Sprite[] SpellSprite;
    // public Sprite[] player_sprite;
    float[] fadeval=new float[1];

    //spell text
    public TextMeshProUGUI[] spellname;
    public VertexGradient[] st_color;

    //place text
    public TextMeshProUGUI[] placeNum;
    public TextMeshProUGUI[] placeEng;

    // private text fstnum="1";
    // private text sndnum="2";
    // private text trdnum="3";
    // private text fthnum="4";

    // private text fsteng="st";
    // private text sndeng="nd";
    // private text trdeng="rd";
    // private text ftheng="th";

    //ripple
    [SerializeField]
    public ParticleSystem[] ripeffect=null;

    public float countdown;
    public bool counting=false;
    bool isUnDissolving=false;
    bool isDissolving=false;

    //Get Spell
    public void GetSpell(){
        //random spell
        // int spellNum=Random.Range(0,6);

        /*switch (spellNum)
        {
            case 0:
                SpellIcon=FireIcon;
                spellname.text = "Flame";
                spellname.colorGradient=fire_color;
                break;
            case 1:
                SpellIcon=AccelIcon;
                spellname.text = "Boost";
                spellname.colorGradient=accel_color;
                break;
            case 2:
                SpellIcon=ShieldIcon;
                spellname.text = "Shield";
                spellname.colorGradient=shield_color;
                break;
            case 3 :
                SpellIcon=IceIcon;
                spellname.text = "Ice";
                spellname.colorGradient=ice_color;
                break;
            case 4:
                SpellIcon=LoveIcon;
                spellname.text = "Chaos";
                spellname.colorGradient=love_color;
                break;
            case 5:
                SpellIcon=ThunderIcon;
                spellname.text = "Thunder";
                spellname.colorGradient=thunder_color;
                break;
            default:
                break;
        }*/
        spellname[0].text="Flame";
        spellname[0].colorGradient=st_color[0];
        SpellMat[0].SetColor("_Color",si_color[0]);
        SpellMat[0].SetTexture("_MainTex",si_tex[4]);

        ripeffect[0].Play();
        fadeval[0]=0f;
        countdown=1f;
        counting=true;
        isUnDissolving=false;
    }
    //Use Spell
    public void UseSpell(){
        isDissolving=true;
    }
    void Start()
    {
        fadeval[0]=0f;
        isUnDissolving=false;
        SpellMat[0].SetFloat("_FadeValue",fadeval[0]);
        SpellIcon[0].GetComponent<Image>().sprite=SpellSprite[4];
    }

    void Update()
    {
        //dissolve delay
        if(counting){
            countdown-=Time.deltaTime;
            if(countdown<0){
                countdown=0;
                isUnDissolving=true;
                counting=false;
            }
        }

        if(isUnDissolving){
            fadeval[0]+=Time.deltaTime;
            if(fadeval[0]>1f){
                fadeval[0]=1f;
                isUnDissolving=false;
            }
            SpellMat[0].SetFloat("_FadeValue",fadeval[0]);
        }
        if(isDissolving){
            fadeval[0]-=Time.deltaTime;
            if(fadeval[0]<0f){
                fadeval[0]=0f;
                isDissolving=false;
            }
            SpellMat[0].SetFloat("_FadeValue",fadeval[0]);
        }
    }
}
