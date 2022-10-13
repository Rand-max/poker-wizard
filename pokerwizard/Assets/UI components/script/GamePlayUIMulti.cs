using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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

    public float[] countdown=new float[4];
    public float[] use_countdown=new float[4];
    public bool[] counting=new bool[4];
    public bool[] use_counting=new bool[4];
    public bool[] isUnDissolving=new bool[4];
    public bool[] isDissolving=new bool[4];

    //Get Spell
    public void GetSpell(int playerNumber){
        RandSpell(playerNumber);
        fadeval[playerNumber]=0f;
        ripeffect[playerNumber].Play();
        countdown[playerNumber]=1f;
        counting[playerNumber]=true;
        isUnDissolving[playerNumber]=false;
    }
    public void RandSpell(int player_num){
        int srand_num=Random.Range(0,6);
        SpellIcon[player_num].GetComponent<Image>().sprite=SpellSprite[srand_num];
        SpellMat[player_num].SetTexture("_MainTex",si_tex[srand_num]);
        SpellMat[player_num].SetColor("_Color",si_color[srand_num]);
        SpellMat[player_num].SetFloat("_FadeValue",0);
        spellname[player_num].colorGradient=st_color[srand_num];
        switch(srand_num){
            case 0:
                spellname[player_num].text="Flame";
                break;
            case 1:
                spellname[player_num].text="Accel";
                break;
            case 2:
                spellname[player_num].text="Shield";
                break;
            case 3:
                spellname[player_num].text="Ice";
                break;
            case 4:
                spellname[player_num].text="Chaos";
                break;
            case 5:
                spellname[player_num].text="Thunder";
                break;
            default:
                break;
        }
    }
    //Use Spell
    public void UseSpell(int playerNumber){
        use_countdown[playerNumber]=1f;
        use_counting[playerNumber]=true;
        isDissolving[playerNumber]=true;
    }
    void Start()
    {
        for(int i=0;i<4;i++){
            SpellIcon[i].GetComponent<Image>().sprite=null;
            SpellMat[i].SetFloat("_FadeValue",0);
            counting[i]=false;
            use_counting[i]=false;
            isUnDissolving[i]=false;
            isDissolving[i]=false;
        }
    }
    void Update(){
        for (int i = 0; i < 4; i++)
        {
            if(counting[i]){
                countdown[i]-=Time.deltaTime;
                if(countdown[i]<0){
                    countdown[i]=0;
                    isUnDissolving[i]=true;
                    counting[i]=false;
                }
            }
            if(use_counting[i]){
                use_countdown[i]-=Time.deltaTime;
                if(use_countdown[i]<0){
                    use_countdown[i]=0;
                    isDissolving[i]=true;
                    use_counting[i]=false;
                }
            }
            if(isUnDissolving[i]&&!isDissolving[i]){
                fadeval[i]+=Time.deltaTime;
                if(fadeval[i]>1f){
                    fadeval[i]=1f;
                    isUnDissolving[i]=false;
                }
                SpellMat[i].SetFloat("_FadeValue",fadeval[i]);
            }
            if(isDissolving[i]&&!isUnDissolving[i]){
                fadeval[i]-=Time.deltaTime;
                if(fadeval[i]<0f){
                    fadeval[i]=0f;
                    isDissolving[i]=false;
                }
                SpellMat[i].SetFloat("_FadeValue",fadeval[i]);
            }
        }
    }
}
