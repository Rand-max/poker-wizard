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
    public bool isdone=false;
   

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayDelayed("btn_hover",wait_time[0]);
        FindObjectOfType<AudioManager>().PlayDelayed("point_appear",wait_time[1]);
        FindObjectOfType<AudioManager>().PlayDelayed("point_appear2",wait_time[2]);
        FindObjectOfType<AudioManager>().PlayDelayed("point_appear3",wait_time[3]);
        FindObjectOfType<AudioManager>().PlayDelayed("point_appear4",wait_time[4]);
        FindObjectOfType<AudioManager>().PlayDelayed("crown",wait_time[5]);
        FindObjectOfType<AudioManager>().PlayDelayed("cheer",wait_time[5]);
        AsyncBtn();
         
    }

    // Update is called once per frame
    void Update()
    {
        /*for(int i=0;i<5;i++){
            if(cdtime==wait_time[i]){
                FindObjectOfType<AudioManager>().Play("point_appear"); 
                i++;
            }
        }
        if(cdtime==9f){
            FindObjectOfType<AudioManager>().Play("crown");
        }else if(cdtime==10f){
            FindObjectOfType<AudioManager>().Play("cheer");
        }*/
        // if(isCele){
        //     FindObjectOfType<AudioManager>().PlayDelayed("cheer",25f);
        // }
    }
    #region Async Workflow

    async void AsyncBaby(){
        foreach (var flg in flags)
        {
            FindObjectOfType<AudioManager>().Play("flag_scroll");
            await flg.DOMoveY(-8,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
        isdone=true;
    }
    
    async void AsyncBtn(){
        foreach (var btn in okbtn)
        {
            await btn.DOAnchorPosY(-462,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
     }
    #endregion
}
