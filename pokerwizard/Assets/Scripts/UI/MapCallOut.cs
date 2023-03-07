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
    public RectTransform mapTag;
    public RectTransform bgc;
    public RectTransform clipIcon;
    //icon
    public RawImage mapIcon;
    public Texture[] mapTex;
    //gate
    public RawImage mapGate;
    public Texture[] gateTex;
    public Image whiteGate;
    public float gate_cd=1f;
    public bool gatecdAllowed;
    //minimap
    public RawImage miniMap;
    public Texture[] miniTex;
    //phone
    public Animator phoneAni;
    public bool aniTrigger;

    public TextMeshProUGUI title;
    public TextMeshProUGUI uiTag;
    public float fadeTime=1f;
    public float countDown=2.5f;
    public bool FadeInable;
    public bool startCtn;
    public bool switchAble;
    public static int mapNum=1;
    // Start is called before the first frame update
    void Start()
    {
        FadeInable=false;
        startCtn=false;
        switchAble=false;
        gatecdAllowed=false;
        aniTrigger=false;
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
            uiTag.text="Select Your Gate";
            JudgeMap();
        }
        whiteGate.color=new Color(1.0f, 1.0f, 1.0f, gate_cd);
    }
    //啟動
    public void StartMapUI(){
        if(Keyboard.current.zKey.wasPressedThisFrame){
            WizardFadeOut();
            startCtn=true;
            gatecdAllowed=true;
            
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
    }
    //flip coin,play tele ani,change gate,map,title
    public void JudgeMap(){
        if(mapNum==1){
            mapIcon.texture=mapTex[0];
            title.text="Colossal Chessboard";
            miniMap.texture=miniTex[0];
            mapGate.texture=gateTex[0];
            if(aniTrigger){
                phoneAni.SetTrigger("to_gate1");
                aniTrigger=false;
            }
        }
        if(mapNum==2){
            mapIcon.texture=mapTex[1];
            title.text="Ice Kingdom";
            miniMap.texture=miniTex[1];
            mapGate.texture=gateTex[1];
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
                gate_cd=1;
                aniTrigger=true;
            }
        }
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame){
            if(mapNum>1){
                mapNum-=1;
                gatecdAllowed=true;
                gate_cd=1;
                aniTrigger=true;
            }
        }
    }
    //等待1秒更換
    public void ChangeGateImg(){
        if(gatecdAllowed){
            gate_cd-=Time.deltaTime;
            if(gate_cd<0){
                gate_cd=0;
                gatecdAllowed=false;
            }
            SpinClip();
        }

    }
    public void BGScale(){
        bgc.DOScaleY(.5f,1.0f);
    }
    public void SpinClip(){
        clipIcon.DOPunchRotation(new Vector3(0f,90f,0f),10,1);
    }
}
