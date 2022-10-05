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
    float[] fadeval=new float[4];

    //spell text
    public TextMeshProUGUI[] spellname;
    public VertexGradient[] st_color;

    //place text
    public TextMeshProUGUI[] placeNum;
    public TextMeshProUGUI[] placeEng;

    //ripple
    [SerializeField]
    public ParticleSystem[] ripeffect=null;

    public float countdown;
    public float use_countdown;
    public bool counting=false;
    public bool use_counting=false;
    public bool isUnDissolving=false;
    public bool isDissolving=false;

    //Get Spell
    public void GetSpell(){
        for(int i=0;i<4;i++){
            RandSpell(i);
            fadeval[i]=0f;
            ripeffect[i].Play();
        }
        countdown=1f;
        counting=true;
        isUnDissolving=false;
    }
    public void RandSpell(int player_num){
        int srand_num=Random.Range(0,6);
        switch(srand_num){
            case 0:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[0];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[0]);
                SpellMat[player_num].SetColor("_Color",si_color[0]);
                spellname[player_num].text="Flame";
                spellname[player_num].colorGradient=st_color[0];
                break;
            case 1:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[1];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[1]);
                SpellMat[player_num].SetColor("_Color",si_color[1]);
                spellname[player_num].text="Accel";
                spellname[player_num].colorGradient=st_color[1];
                break;
            case 2:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[2];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[2]);
                SpellMat[player_num].SetColor("_Color",si_color[2]);
                spellname[player_num].text="Shield";
                spellname[player_num].colorGradient=st_color[2];
                break;
            case 3:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[3];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[3]);
                SpellMat[player_num].SetColor("_Color",si_color[3]);
                spellname[player_num].text="Ice";
                spellname[player_num].colorGradient=st_color[3];
                break;
            case 4:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[4];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[4]);
                SpellMat[player_num].SetColor("_Color",si_color[4]);
                spellname[player_num].text="Chaos";
                spellname[player_num].colorGradient=st_color[4];
                break;
            case 5:
                SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[5];
                SpellMat[player_num].SetTexture("_MainTex",si_tex[5]);
                SpellMat[player_num].SetColor("_Color",si_color[5]);
                spellname[player_num].text="Thunder";
                spellname[player_num].colorGradient=st_color[5];
                break;
            default:
                break;
        }
    }
    //Use Spell
    public void UseSpell(){
        use_countdown=1f;
        use_counting=true;
        isDissolving=true;
    }
    void Start()
    {
        for(int i=0;i<4;i++){
            SpellIcon[i].GetComponent<Image>().sprite=null;
            SpellMat[i].SetFloat("_FadeValue",0);
        }
        isUnDissolving=false;
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
        if(use_counting){
            use_countdown-=Time.deltaTime;
            if(use_countdown<0){
                use_countdown=0;
                isDissolving=true;
                use_counting=false;
            }
        }

        if(isUnDissolving){
            /*for(int i=0;i<4;i++){
                fadeval[i]+=Time.deltaTime;
                if(fadeval[i]>1f){
                    fadeval[i]=1f;
                    isUnDissolving[i]=false;
                }
                
                SpellMat[i].SetFloat("_FadeValue",fadeval[i]);
            }*/
            fadeval[0]+=Time.deltaTime;
            fadeval[1]+=Time.deltaTime;
            fadeval[2]+=Time.deltaTime;
            fadeval[3]+=Time.deltaTime;
            if(fadeval[0]>1f){
                fadeval[0]=1f;
                fadeval[1]=1f;
                fadeval[2]=1f;
                fadeval[3]=1f;
                isUnDissolving=false;
            }
            SpellMat[0].SetFloat("_FadeValue",fadeval[0]);
            SpellMat[1].SetFloat("_FadeValue",fadeval[1]);
            SpellMat[2].SetFloat("_FadeValue",fadeval[2]);
            SpellMat[3].SetFloat("_FadeValue",fadeval[3]); 
        }        
        
        if(isDissolving&&!isUnDissolving){
            fadeval[0]-=Time.deltaTime;
            fadeval[1]-=Time.deltaTime;
            fadeval[2]-=Time.deltaTime;
            fadeval[3]-=Time.deltaTime;
            if(fadeval[0]<0f){
                fadeval[0]=0f;
                fadeval[1]=0f;
                fadeval[2]=0f;
                fadeval[3]=0f;
                isDissolving=false;
            }
            SpellMat[0].SetFloat("_FadeValue",fadeval[0]);
            SpellMat[1].SetFloat("_FadeValue",fadeval[1]);
            SpellMat[2].SetFloat("_FadeValue",fadeval[2]);
            SpellMat[3].SetFloat("_FadeValue",fadeval[3]); 
        }
    }
}
