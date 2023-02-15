using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class ChooseMap : MonoBehaviour
{
    public GameObject MapUI;
    public RawImage mapImg;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public Texture[] mapTex=new Texture[2];
    public TextMeshProUGUI mapTitle;
    public static int mapnum=1;
    public void BtnNext(){
        if(mapnum<2){
            mapnum +=1;
            FindObjectOfType<AudioManager>().Play("btn_click");
        }
    }
    public void BtnPre(){
        if(mapnum>1){
            mapnum -=1;
            FindObjectOfType<AudioManager>().Play("btn_click");
        }
    }
    public void BtnBack(){
        MapUI.SetActive(false);
    }
    public void JudgePage(){
        if(mapnum==1){
            mapImg.texture=mapTex[0];
            mapTitle.text="The Colossal Chessboard";
            leftBtn.SetActive(false);
            rightBtn.SetActive(true);
        }
        if(mapnum==2){
            mapImg.texture=mapTex[1];
            mapTitle.text="The Ice Kingdom";
            leftBtn.SetActive(true);
            rightBtn.SetActive(false);
        }
    }
    void InputBtn(){
        if(Keyboard.current.rightArrowKey.wasPressedThisFrame){
            if(mapnum<2){
                mapnum +=1;
                FindObjectOfType<AudioManager>().Play("btn_click");
            }
        }
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame){
             if(mapnum>1){
                mapnum -=1;
                FindObjectOfType<AudioManager>().Play("btn_click");
            }
        }
    }
    // Start is called before the first frame update
    void Start(){
        MapUI.SetActive(false);
        leftBtn.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.mKey.wasPressedThisFrame){
            MapUI.SetActive(true);
        }
        JudgePage();
        InputBtn();
    }
}
