using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SelectImgBtn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public CursorKey currentCursor;
    public Texture playertexture;
    public Texture playerfasten;
    public RectTransform button;
    public LockDissolveTest unlock;
    public CameraShake shake;
    public bool isclicked=false;
    public Vector2 punch;
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Animator>().Play("original_scale");
    }

    public void OnPointerEnter(PointerEventData eventData){
        button.GetComponent<Animator>().Play("selectimg");
    }
    public void OnPointerEnter(CursorKey cursor){
        currentCursor=cursor;
        button.GetComponent<Animator>().Play("selectimg");
    }
    public void OnPointerExit(PointerEventData eventData){
        button.GetComponent<Animator>().Play("selectimg_ho");
    }
    public void OnPointerExit(CursorKey cursor){
        if(currentCursor==cursor){
            currentCursor=null;
            button.GetComponent<Animator>().Play("selectimg_ho");
        }
    }
    public void OnPointerDown(CursorKey cursor,GameObject locker){
        if(!isclicked){
            isclicked=true;
            unlock.ButtonClicked(cursor,locker,playertexture,playerfasten);
            shake.ShakeIt();
            AsyncPunch();
            OnPointerExit(currentCursor);
        }
    }
    #region Async Workflow

    async void AsyncPunch(){ 
       await button.DOPunchAnchorPos(punch,.5f,20,1).SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
    }
    #endregion
}