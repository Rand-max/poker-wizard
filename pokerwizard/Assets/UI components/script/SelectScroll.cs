using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectScroll : MonoBehaviour
{
    public RectTransform ctlctn;
    // Start is called before the first frame update
    void Start()
    {
      AsyncRight();
      FindObjectOfType<AudioManager>().Play("desk_open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Async Workflow

    async void AsyncRight(){ 
        await ctlctn.DOAnchorPosX(210,Random.Range(.7f,1f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
    }
    #endregion
}
