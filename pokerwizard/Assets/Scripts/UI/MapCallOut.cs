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
    //icon
    public RawImage mapIcon;
    public Texture[] mapTex;
    //gate
    public RawImage mapGate;
    public Texture[] gateTex;
    public Image whiteGate;
    //minimap
    public RawImage miniMap;
    public Texture[] miniTex;
    //phone
    public GameObject phoneNum;
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
    }

    // Update is called once per frame
    void Update()
    {
        //wizard fade out,map fade in
        if(Keyboard.current.zKey.wasPressedThisFrame){
            WizardFadeOut();
            startCtn=true;
        }
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
            MapUIFadeIn();
            switchAble=true;
        }
        //switch page
        if(switchAble){
            SwitchGate();
            uiTag.text="Select Your Gate";
            JudgeMap();
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
    //flip coin,play tele ani,change gate,change map
    public void JudgeMap(){
        if(mapNum==1){
            mapIcon.texture=mapTex[0];
            title.text="Colossal Chessboard";
            miniMap.texture=miniTex[0];
            mapGate.texture=gateTex[0];

        }
        if(mapNum==2){
            mapIcon.texture=mapTex[1];
            title.text="Ice Kingdom";
            miniMap.texture=miniTex[1];
            mapGate.texture=gateTex[1];
        }
    }
    public void SwitchGate(){
        if(Keyboard.current.rightArrowKey.wasPressedThisFrame){
            if(mapNum<2){
                mapNum+=1;
            }
        }
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame){
            if(mapNum>1){
                mapNum-=1;
            }
        }
    }
}
