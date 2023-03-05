using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MapCallOut : MonoBehaviour
{
    public RectTransform charUI;
    public RectTransform mapUI;
    public RawImage mapIcon;
    public RawImage mapImg;
    public RawImage whiteGate;
    public RawImage miniMap;
    public GameObject phoneNum;
    public TextMeshProUGUI title;
    public float fadeTime=1f;
    public float countDown=2.5f;
    public bool FadeInable;
    public bool startCtn;
    // Start is called before the first frame update
    void Start()
    {
        FadeInable=false;
        startCtn=false;
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
        }
    }
    public void WizardFadeOut(){
        charUI.transform.localPosition=new Vector3(0f,0f,0f);
        charUI.DOAnchorPos(new Vector2(-2500f,0f),fadeTime,false).SetEase(Ease.InOutQuint);
    }
    public void MapUIFadeIn(){
        mapUI.transform.localPosition=new Vector3(0f,-1000f,0f);
        mapUI.DOAnchorPos(new Vector2(0f,0f),fadeTime,false).SetEase(Ease.OutCirc);
    }
}
