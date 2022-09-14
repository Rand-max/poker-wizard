using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuleBtn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public RectTransform button;
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Animator>().Play("bookhover_on");
    }

    public void OnPointerEnter(PointerEventData eventData){
        button.GetComponent<Animator>().Play("bookhover_on");
    }
    public void OnPointerExit(PointerEventData eventData){
        button.GetComponent<Animator>().Play("bookhover_off");
    }
}