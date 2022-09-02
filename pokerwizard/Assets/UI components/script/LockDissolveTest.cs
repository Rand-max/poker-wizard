using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockDissolveTest : MonoBehaviour
{
    //key
    public GameObject key_p1;
    public GameObject key_p2;
    public GameObject key_p3;
    public GameObject key_p4;
    //lock
    public GameObject lock_club;
    public GameObject lock_diamond;
    public GameObject lock_heart;
    public GameObject lock_spade;
    
    Material Matclub;
    Material Matdiamond;
    Material Matheart;
    Material Matspade;

    private float fadeval;
    private float countdown;
    bool counting=false;
    bool isDissolving=false;
    private bool isEnabled=true;

    //text
    public TextMeshProUGUI playernum_club;
    public TextMeshProUGUI playernum_diamond;
    public TextMeshProUGUI playernum_heart;
    public TextMeshProUGUI playernum_spade;

    public TextMeshProUGUI Connected1;
    public TextMeshProUGUI Connected2;
    public TextMeshProUGUI Connected3;
    public TextMeshProUGUI Connected4;
    public TextMeshProUGUI p1_text;
    public TextMeshProUGUI p2_text;
    public TextMeshProUGUI p3_text;
    public TextMeshProUGUI p4_text;

    //circle
    private bool circleAppear;

    [SerializeField]
    public Image cirimg1;
    [SerializeField]
    public Image cirimg2;
    [SerializeField]
    public Image cirimg3;
    [SerializeField]
    public Image cirimg4;

    [SerializeField]
    private Color circolor;

    public void ButtonClicked(){
        //lock
        fadeval=1.5f;
        countdown=1f;
        isEnabled=!isEnabled;
        playernum_club.text="P1";
        isDissolving=true;
        circleAppear=true; 
        //controller
        Connected1.text="Connected !";
        Connected2.text="Connected !";
        Connected3.text="Connected !";
        Connected4.text="Connected !";
        p1_text.text="P1";
        p2_text.text="P2";
        p3_text.text="P3";
        p4_text.text="P4";
        //key
        key_p1.SetActive(isEnabled);
    }
    // Start is called before the first frame update
    void Start()
    {
        Matclub=lock_club.GetComponent<Image>().material;
        Matclub.SetFloat("_FadeValue",1f);
        circleAppear=false;
        circolor= new Color (1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolving){
            fadeval-=Time.deltaTime;
            if(fadeval<0f){
                fadeval=0f;
                isDissolving=false;
            }
            Matclub.SetFloat("_FadeValue",fadeval);
        }
        if(circleAppear){
            countdown -=Time.deltaTime;
            if(countdown<0f){
                countdown=0f;
                circleAppear=false;
                cirimg1.color=circolor;
            }
            circolor= new Color (1, 1, 1, 1);
        }
    }
    
}
