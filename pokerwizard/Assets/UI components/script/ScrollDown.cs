using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollDown : MonoBehaviour
{
    public RectTransform[] flags;
    public RectTransform[] okbtn;
    public float[] wait_time;

    public float cdtime;

    // Start is called before the first frame update
    void Start()
    {
        AsyncBaby();
        AsyncBtn();
        cdtime=0;
    }

    // Update is called once per frame
    void Update()
    {
        AudioPlay();   
        // for(int i=0;i<5;i++){
        //     if(cdtime==wait_time[i]){
        //         FindObjectOfType<AudioManager>().Play("point_appear"); 
        //         i++;
        //     }
        // }
        // if(cdtime==9f){
        //     FindObjectOfType<AudioManager>().Play("crown");
        // }else if(cdtime==10f){
        //     FindObjectOfType<AudioManager>().Play("cheer");
        // }
    }

    void AudioPlay(){
        cdtime+=Time.deltaTime;
        if(cdtime>10f){
            cdtime=10f;
        }
        
    } 

    #region Async Workflow

    async void AsyncBaby(){
        foreach (var flg in flags)
        {
            FindObjectOfType<AudioManager>().Play("flag_scroll");
            await flg.DOMoveY(-8,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
    }
    
    async void AsyncBtn(){
        foreach (var btn in okbtn)
        {
            await btn.DOAnchorPosY(-462,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
     }
    #endregion
}
