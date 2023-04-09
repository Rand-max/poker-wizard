using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleUICallOut : MonoBehaviour
{
    public GameObject RuleUI;
    // Start is called before the first frame update
    void Start()
    {
       RuleUI.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RuleBookClicked(){
        RuleUI.gameObject.SetActive(true);
    }
    public void ReturnRuleBook(){
        RuleUI.gameObject.SetActive(false);
    }
}