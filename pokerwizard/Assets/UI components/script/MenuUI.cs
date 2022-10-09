using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuUI : MonoBehaviour
{
    public RectTransform[] btns;
    // Start is called before the first frame update
    void Start()
    {
        AsyncBaby();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #region Async Workflow

    async void AsyncBaby(){
        foreach (var btn in btns)
        {
            FindObjectOfType<AudioManager>().Play("btn_hover");
            await btn.DOAnchorPosX(-9.45f,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
    }
    
    #endregion
}
