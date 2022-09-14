using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectImgBtn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public RectTransform button;
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Animator>().Play("selectimg_ho");
    }

    public void OnPointerEnter(PointerEventData eventData){
        button.GetComponent<Animator>().Play("selectimg");
    }
    public void OnPointerExit(PointerEventData eventData){
        button.GetComponent<Animator>().Play("selectimg_ho");
    }
}