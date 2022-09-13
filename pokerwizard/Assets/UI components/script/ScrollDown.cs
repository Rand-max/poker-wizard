using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollDown : MonoBehaviour
{
    public RectTransform[] flags;
    public RectTransform[] okbtn;
    // Start is called before the first frame update
    void Start()
    {
        AsyncBaby();
        //AsyncBtn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Async Workflow

    async void AsyncBaby(){
        foreach (var flg in flags)
        {
            await flg.DOMoveY(-8,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
    }
     async void AsyncBtn(){
        foreach (var btn in okbtn)
        {
            await btn.DOMoveY(40,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
     }
    #endregion
}
