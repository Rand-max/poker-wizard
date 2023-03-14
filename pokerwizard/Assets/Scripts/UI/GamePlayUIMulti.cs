using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
    public PlayerManager playerman;
    public GameObject[] SpellIcon;
    public GameObject[] aim;
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
    public bool GetSpell(int playerNumber,int spellnumber=-1){
        if(!isDissolving[playerNumber]){
            int srand;
            if(spellnumber==-1){
                srand=Random.Range(0,GetComponent<SpellManager>().allSpell.Count);
            }
            else{
                srand=spellnumber;
            }
            RandSpell(playerNumber,srand);
            fadeval[playerNumber]=0f;
            ripeffect[playerNumber].Play();
            counting[playerNumber]=true;
            isUnDissolving[playerNumber]=false;
            return true;
        }
        else{
            return false;
        }
    }
    public void RandSpell(int player_num,int srand_num){
        ShootingController playershooter=playerman.Players[player_num].transform.parent.GetComponentInChildren<ShootingController>();
        playershooter.CurrentSpell=GetComponent<SpellManager>().allSpell[srand_num];
        playershooter.ammo=playershooter.CurrentSpell.SpellAmout;
        playershooter.preparetime=playershooter.CurrentSpell.BulletCoolDownTime;
        playershooter.maxdistance=playershooter.CurrentSpell.MaxDistance;
        SpellIcon[player_num].GetComponent<Image>().sprite=playershooter.CurrentSpell.SpellSprite;
        SpellMat[player_num].SetTexture("_MainTex",playershooter.CurrentSpell.IconTexture);
        SpellMat[player_num].SetColor("_Color",playershooter.CurrentSpell.IconColor);
        SpellMat[player_num].SetFloat("_FadeValue",0);
        spellname[player_num].alpha=0.0f;
        spellname[player_num].colorGradient=playershooter.CurrentSpell.TextColor;
        spellname[player_num].text=playershooter.CurrentSpell.SpellName;
        countdown[player_num]=playershooter.CurrentSpell.BulletPrepareTime;
    }
    //Use Spell
    public void UseSpell(int playerNumber){
        use_countdown[playerNumber]=1f;
        use_counting[playerNumber]=true;
        isDissolving[playerNumber]=true;
    }
    void Start()
    {
        playerman=null;
        foreach (var plm in FindObjectsOfType<PlayerManager>())
        {
            if(!plm.isOld){
                playerman=plm;
            }
        }
        if(playerman==null){
            Debug.Log("No pm");
            return;
        }
        for(int i=0;i<4;i++){
            SpellIcon[i].GetComponent<Image>().sprite=null;
            SpellMat[i].SetFloat("_FadeValue",0);
            counting[i]=false;
            use_counting[i]=false;
            isUnDissolving[i]=false;
            isDissolving[i]=false;
            spellname[i].alpha=0.0f;
        }
    }
    void Update(){
        for(int i=0;i<(GetComponent<SpellManager>().allSpell.Count<=10?GetComponent<SpellManager>().allSpell.Count:10);i++){
            if(((KeyControl)Keyboard.current[i.ToString()]).wasPressedThisFrame){
                GetSpell(0,i);
            }
        }
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
            /*if(use_counting[i]){
                use_countdown[i]-=Time.deltaTime;
                if(use_countdown[i]<0){
                    use_countdown[i]=0;
                    isDissolving[i]=true;
                    use_counting[i]=false;
                }
            }*/
            if(isUnDissolving[i]&&!isDissolving[i]){
                fadeval[i]+=Time.deltaTime;
                if(fadeval[i]>1f){
                    fadeval[i]=1f;
                    isUnDissolving[i]=false;
                }
                SpellMat[i].SetFloat("_FadeValue",fadeval[i]);
                spellname[i].alpha=fadeval[i];
            }
            if(isDissolving[i]&&!isUnDissolving[i]){
                fadeval[i]-=Time.deltaTime;
                if(fadeval[i]<0f){
                    fadeval[i]=0f;
                    isDissolving[i]=false;
                    ShootingController playershooter=playerman.Players[i].transform.parent.GetComponentInChildren<ShootingController>();
                    playershooter.CurrentSpell=null;
                    spellname[i].text="";
                }
                SpellMat[i].SetFloat("_FadeValue",fadeval[i]);
                spellname[i].alpha=fadeval[i];
            }
        }
    }
}
