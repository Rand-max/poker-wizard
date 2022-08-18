using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellActivate : MonoBehaviour
{
    //spell icon
    public GameObject FireIcon;
    public GameObject AccelIcon;
    public GameObject ShieldIcon;
    public GameObject IceIcon;
    public GameObject LoveIcon;
    public GameObject ThunderIcon; 

    private GameObject SpellIcon;

    public float fadeval;
    public float countdown=1.5f;
    public bool counting=false;
    bool isUnDissolving=false;

    //ripple
    [SerializeField]
    public ParticleSystem ripeffect=null;

    //spell tag
    string FireTag="FireIcon";

    private string SpellTag;

    //spell material
    Material FireMat;
    
    public bool isEnabled=true;

    public void ButtonClicked(){

        fadeval=0f;
        isEnabled=!isEnabled;
        ripeffect.Play();
        
        FireMat=FireIcon.GetComponent<Image>().material;
        // FireMat.SetFloat("_FadeValue",fadeval);
        SpellIcon=FireIcon;
        SpellIcon.SetActive(isEnabled);

        isUnDissolving=true;

        // FireIcon=GameObject.FindGameObjectWithTag(FireTag);
        // FireMat=FireIcon.GetComponent<Image>().material;

        // int spellNum=Random.Range(0,6);
        // switch (spellNum)
        // {
        //     case 0:
        //         SpellIcon=FireIcon;
        //         break;
        //     case 1:
        //         SpellIcon=AccelIcon;
        //         break;
        //     case 2:
        //         SpellIcon=ShieldIcon;
        //         break;
        //     case 3 :
        //         SpellIcon=IceIcon;
        //         break;
        //     case 4:
        //         SpellIcon=LoveIcon;
        //         break;
        //     case 5:
        //         SpellIcon=ThunderIcon;
        //         break;
        //     default:
        //         break;
        // }
        /*
        if(isUnDissolving){
            fadeval+=Time.deltaTime;
            if(fadeval>1f){
                fadeval=1f;
                isUnDissolving=false;
            }
            FireMat.SetFloat("_FadeValue",fadeval);
        }*/

    }
    void Update(){
        if(isUnDissolving){
            fadeval+=Time.deltaTime;
            if(fadeval>1f){
                fadeval=1f;
                isUnDissolving=false;
            }
            FireMat.SetFloat("_FadeValue",fadeval);
        }
    }

}
