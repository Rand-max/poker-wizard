using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MapCallOut : MonoBehaviour
{
    //dotween obj
    public RectTransform charUI;
    public RectTransform mapUI;
    public RectTransform readyTable;
    public RectTransform startWord;
    public RectTransform mapTag;
    public RectTransform bgc;
    public RectTransform clipIcon;
    //icon
    public RawImage mapIcon;
    public Texture[] mapTex;
    public bool iconChange=false;
    public float icon_cd;
    //gate
    public RawImage mapGate;
    public Texture[] gateTex;
    public Image whiteGate;
    public float gate_cd=1f;
    public bool gatecdAllowed;
    public bool phoneFinish=false;
    public float tex_cd;
    //minimap
    public RawImage miniMap;
    public Texture[] miniTex;
    public Image miniBg;
    //phone
    public Animator phoneAni;
    public bool aniTrigger=false;

    //ready ui
    public GameObject readyBG;
    public RawImage readyIcon;
    public Texture[] readyTex;

    public TextMeshProUGUI title;
    public TextMeshProUGUI uiTag;
    public float fadeTime=1f;
    public float countDown=2.5f;
    public bool FadeInable=false;
    public bool startCtn=false;
    public bool switchAble=false;
    public static int mapNum=1;
    // Start is called before the first frame update
    void Start()
    {
        readyBG.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //wizard fade out,map fade in
        StartMapUI();
        if(startCtn){
            countDown-=Time.deltaTime;
            if(countDown<0f){
                countDown=0;
            }
            if(countDown==0){
                startCtn=false;
                FadeInable=true;
            }
        }
        if(FadeInable){
            BGScale();
            MapUIFadeIn();
            switchAble=true;
        }
        //switch page
        if(switchAble){
            SwitchGate();
            ChangeGateImg();
            ChangeClip();
            uiTag.text="Select Your Gate";
            JudgeMap();
            SelectMap();
        }
        whiteGate.color=new Color(1.0f, 1.0f, 1.0f, gate_cd);
        miniBg.color=new Color(1.0f, 1.0f, 1.0f, gate_cd);
    }
    //啟動
    public void StartMapUI(){
        if(Keyboard.current.zKey.wasPressedThisFrame){
            WizardFadeOut();
            startCtn=true;
            gatecdAllowed=true;
            mapIcon.texture=mapTex[0];
        }
    }
    public void WizardFadeOut(){
        charUI.transform.localPosition=new Vector3(0f,0f,0f);
        charUI.DOAnchorPos(new Vector2(-2500f,0f),fadeTime,false).SetEase(Ease.InOutQuint);
        mapIcon.texture=mapTex[1];
    }
    public void MapUIFadeIn(){
        mapUI.transform.localPosition=new Vector3(0f,-1000f,0f);
        mapUI.DOAnchorPos(new Vector2(0f,0f),fadeTime,false).SetEase(Ease.OutCirc);
        mapTag.DOAnchorPos(new Vector2(0f,-420f),fadeTime+10f,false).SetEase(Ease.OutCirc);
    }
    //flip coin,play tele ani,change gate,map,title
    public void JudgeMap(){
        if(mapNum==1){
            title.text="Colossal Chessboard";
            if(aniTrigger){
                phoneAni.SetTrigger("to_gate1");
                aniTrigger=false;
                
            }
        }
        if(mapNum==2){
            title.text="Ice Kingdom";
            if(aniTrigger){
                phoneAni.SetTrigger("to_gate2");
                aniTrigger=false;
            }
        }
    }
    public void SwitchGate(){
        if(Keyboard.current.rightArrowKey.wasPressedThisFrame){
            if(mapNum<2){
                mapNum+=1;
                gatecdAllowed=true;
                gate_cd=0;
                icon_cd=0.35f;
                aniTrigger=true;
            }
        }
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame){
            if(mapNum>1){
                mapNum-=1;
                gatecdAllowed=true;
                gate_cd=0;
                icon_cd=0.35f;
                aniTrigger=true;
            }
        }
    }
    //等待1秒更換
    public void ChangeGateImg(){   
        if(gatecdAllowed){
            gate_cd+=Time.deltaTime;
            if(gate_cd>1){
                gate_cd=1;
                gatecdAllowed=false;
                phoneFinish=true;
                SpinClip();
                iconChange=true;
            }
        }
        if(phoneFinish){
            gate_cd-=Time.deltaTime;
            if(gate_cd<0){
                gate_cd=0;
                phoneFinish=false;
            }
        }
    }
    public void BGScale(){
        bgc.DOScaleY(.5f,1.0f);
    }
    public void SpinClip(){
        clipIcon.DOLocalRotate(new Vector3(360, 360, 360), 1f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    }
    //change tex
    public void ChangeClip(){
        if(iconChange){
            icon_cd-=Time.deltaTime;
            tex_cd-=Time.deltaTime;
            if(icon_cd<0){
                icon_cd=0;
                if(mapNum==1){
                    mapIcon.texture=mapTex[0];
                }else if(mapNum==2){
                    mapIcon.texture=mapTex[1];
                }
                iconChange=false;
            }
            if(tex_cd<0){
                if(mapNum==1){
                    miniMap.texture=miniTex[0];
                    mapGate.texture=gateTex[0];
                }else if(mapNum==2){
                    miniMap.texture=miniTex[1];
                    mapGate.texture=gateTex[1];
                }
            }
        }
    }
    public void SelectMap(){
        if(Keyboard.current.f9Key.wasPressedThisFrame){
            readyBG.gameObject.SetActive(true);
            readyTable.DOAnchorPos(new Vector2(0f,0f),fadeTime,false).SetEase(Ease.OutCirc);
            if(mapNum==1){
                readyIcon.texture=readyTex[0];
            }else if(mapNum==2){
                readyIcon.texture=readyTex[1];
            }
        }
    }
}
