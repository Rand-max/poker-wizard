using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

public class SettingCallOut : MonoBehaviour
{
    //setting btn fade in, cross btn fade out
    public GameObject settingObj;
    public RectTransform settingUI;
    public GameObject blurBg;
    public GameObject exitBtn;
    public float fadeTime=1f;
    public bool FadeInable=false;
    public bool FadeOutTrigger=false;
    public bool FadeOutable=false;
    void Start()
    {
        //settingObj.SetActive(false);
        blurBg.gameObject.SetActive(false);
    }

    void Update()
    {
        if(FadeInable){
            //settingObj.SetActive(true);
            SettingFadeIn();
            FadeOutTrigger=true;
        }
        if(FadeOutTrigger&&FadeOutable){
            SettingFadeOut();
            FadeOutTrigger=false;
        }
    }
    public void SettingFadeIn(){
        blurBg.gameObject.SetActive(true);
        settingUI.DOAnchorPos(new Vector2(0f,0f),fadeTime,false).SetEase(Ease.OutCirc);
    }
    public void SettingFadeOut(){
        blurBg.gameObject.SetActive(false);
        settingUI.DOAnchorPos(new Vector2(0f,1006f),.5f,false).SetEase(Ease.InOutQuint);
    }
}
