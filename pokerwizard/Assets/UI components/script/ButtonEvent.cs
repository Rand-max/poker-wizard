using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
{
    public RectTransform btn;
    [SerializeField] private Image greenbtn;
    [SerializeField] private Sprite btn_before,btn_after;
    
    void Start(){
        btn.GetComponent<Animator>().Play("btn_hover");
    }
    public void OnPointerDown(PointerEventData eventData){
        greenbtn.sprite=btn_after;
    }
    public void OnPointerUp(PointerEventData eventData){
        greenbtn.sprite=btn_before;
    }
    public void OnPointerEnter(PointerEventData eventData){
        greenbtn.sprite=btn_after;
        btn.GetComponent<Animator>().Play("btn_hover");
        FindObjectOfType<AudioManager>().Play("btn_hover");
    }
    public void OnPointerExit(PointerEventData eventData){
        greenbtn.sprite=btn_before;
        btn.GetComponent<Animator>().Play("hoveroff");
    }
}
