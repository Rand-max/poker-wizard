using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellActivate : MonoBehaviour
{
    //spell icon
    public GameObject FireIcon;
    public GameObject AccelIcon;
    public GameObject ShieldIcon;
    public GameObject IceIcon;
    public GameObject LoveIcon;
    public GameObject ThunderIcon; 

    private GameObject SpellIcon;

    //spell text
    public VertexGradient fire_color;
    public VertexGradient accel_color;
    public VertexGradient shield_color;
    public VertexGradient ice_color;
    public VertexGradient love_color;
    public VertexGradient thunder_color;

    public VertexGradient brown_color;
    public VertexGradient purp_color;

    public TextMeshProUGUI spellname;

    //place text
    public TextMeshProUGUI placeNum;
    public TextMeshProUGUI placeEng;
    
    //ripple
    [SerializeField]
    public ParticleSystem ripeffect=null;

    public float fadeval;
    public float countdown;
    public bool counting=false;
    bool isUnDissolving=false;

    //spell material
    Material FireMat;
    Material AccelMat;
    Material ShieldMat;
    Material IceMat;
    Material LoveMat;
    Material ThunderMat;
    
    public bool isEnabled=true;

    public void ButtonClicked(){

        fadeval=0f;
        countdown=1f;
        isEnabled=!isEnabled;
        ripeffect.Play();
        
        SpellIcon=FireIcon;
        spellname.text = "Flame";
        spellname.colorGradient=fire_color;

        counting=true;
        isUnDissolving=false;

        //名次
        int Whichplace=Random.Range(0,4);
        switch(Whichplace){
            case 0:
                placeNum.text="1";
                placeEng.text="st";
                placeNum.colorGradient=fire_color;
                placeEng.colorGradient=fire_color;
                break;
            case 1:
                placeNum.text="2";
                placeEng.text="nd";
                placeNum.colorGradient=ice_color;
                placeEng.colorGradient=ice_color;
                break;
            case 2:
                placeNum.text="3";
                placeEng.text="rd";
                placeNum.colorGradient=brown_color;
                placeEng.colorGradient=brown_color;
                break;
            case 3 :
                placeNum.text="4";
                placeEng.text="th";
                placeNum.colorGradient=purp_color;
                placeEng.colorGradient=purp_color;
                break;
            default:
                break;
        }
        
        //咒語
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
 
    }
    void Start() {
        FireMat=FireIcon.GetComponent<Image>().material;
        AccelMat=AccelIcon.GetComponent<Image>().material;
        ShieldMat=ShieldIcon.GetComponent<Image>().material;
        IceMat=IceIcon.GetComponent<Image>().material;
        LoveMat=LoveIcon.GetComponent<Image>().material;
        ThunderMat=ThunderIcon.GetComponent<Image>().material;
    }
    void Update(){
        //dissolve delay
        if(counting){
            countdown-=Time.deltaTime;
            if(countdown<0){
                countdown=0;
                isUnDissolving=true;
                counting=false;
                SpellIcon.SetActive(isEnabled);
            }
        }

        if(isUnDissolving){
            fadeval+=Time.deltaTime;
            if(fadeval>1f){
                fadeval=1f;
                isUnDissolving=false;
            }
            FireMat.SetFloat("_FadeValue",fadeval);
        }
    }

}
