using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectScroll : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image greenbtn;
    [SerializeField] private Sprite btn_before,btn_after;
    
    public void OnPointerDown(PointerEventData eventData){
        greenbtn.sprite=btn_after;
    }
    public void OnPointerUp(PointerEventData eventData){
        greenbtn.sprite=btn_before;
    }
    public void OnPointerEnter(PointerEventData eventData){
        greenbtn.sprite=btn_after;
    }
    public void OnPointerExit(PointerEventData eventData){
        greenbtn.sprite=btn_before;
    }
}
