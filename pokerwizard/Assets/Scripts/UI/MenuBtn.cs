using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuBtn : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,ISelectHandler,IDeselectHandler
{
    public RectTransform btn;
    [SerializeField] private Image btnimg;
    [SerializeField] private Sprite btn_before,btn_after;

    [SerializeField] private Image logoimg;
    [SerializeField] private Sprite logo_bef,logo_af;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Color title_bef,title_af;

    void Awake() {
        title.color=title_bef;
        btnimg.sprite=btn_before;
        logoimg.sprite=logo_bef;
    }
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData){
        btnimg.sprite=btn_after;
        logoimg.sprite=logo_af;
        title.color=title_af;
        FindObjectOfType<AudioManager>().Play("btn_click");
    }
    public void OnPointerUp(PointerEventData eventData){
        btnimg.sprite=btn_before;
        logoimg.sprite=logo_bef;
        title.color=title_bef;
    }
    public void OnPointerEnter(PointerEventData eventData){
        btnimg.sprite=btn_after;
        logoimg.sprite=logo_af;
        title.color=title_af;
        btn.GetComponent<Animator>().Play("menu_hover_on");
        FindObjectOfType<AudioManager>().Play("btn_hover");
    }
    public void OnSelect(BaseEventData eventData){
        btnimg.sprite=btn_after;
        logoimg.sprite=logo_af;
        title.color=title_af;
        btn.GetComponent<Animator>().Play("menu_hover_on");
        FindObjectOfType<AudioManager>().Play("btn_hover");
    }
    public void OnDeselect(BaseEventData eventData){
        btnimg.sprite=btn_before;
        logoimg.sprite=logo_bef;
        title.color=title_bef;
        btn.GetComponent<Animator>().Play("menu_hover_off");
    }
    public void OnPointerExit(PointerEventData eventData){
        btnimg.sprite=btn_before;
        logoimg.sprite=logo_bef;
        title.color=title_bef;
        btn.GetComponent<Animator>().Play("menu_hover_off");
    }
}
