using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuleBtn : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public RectTransform button;
    //public LoadScene loaer;
    // Start is called before the first frame update
    void Start()
    {
        // button.GetComponent<Animator>().Play("bookhover_on");
    }
    public void OnPointerDown(PointerEventData eventData){
        FindObjectOfType<AudioManager>().Play("book_flip");
    }
    public void OnPointerEnter(PointerEventData eventData){
        button.GetComponent<Animator>().Play("bookhover_on");
        FindObjectOfType<AudioManager>().Play("btn_hover");
    }
    public void OnPointerExit(PointerEventData eventData){
        button.GetComponent<Animator>().Play("bookhover_off");
    }
    public void OnPointerDown(CursorKey cursor){
        FindObjectOfType<AudioManager>().Play("book_flip");
        //loaer.LoadtheScene("Rules");
    }
    public void OnPointerEnter(CursorKey cursor){
        button.GetComponent<Animator>().Play("bookhover_on");
        FindObjectOfType<AudioManager>().Play("btn_hover");
    }
    public void OnPointerExit(CursorKey cursor){
        button.GetComponent<Animator>().Play("bookhover_off");
    }
}