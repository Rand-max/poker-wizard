using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCallOut : MonoBehaviour
{
    public GameObject ReadyUI;
    public bool isEnabled=true;

    public void ButtonClicked(){
        isEnabled=!isEnabled;
        ReadyUI.SetActive(isEnabled);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
