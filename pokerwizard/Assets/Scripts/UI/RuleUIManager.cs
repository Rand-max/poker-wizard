using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RuleUIManager : MonoBehaviour
{
    public RawImage ruleimg;
    public Texture[] ruleTex=new Texture[2];
    public GameObject[] pagedot;

    //btn
    public GameObject leftbtn;
    public GameObject rightbtn;

    //scene content
    public GameObject right_content;
    public GameObject leftimg;
    public GameObject card;

    //card
    public GameObject card1_ct1;
    public GameObject card1_ct2;
    public GameObject card2t1;
    public GameObject card2t2;
    public GameObject card3t1;
    public GameObject card3t2;

    //text
    public TextMeshProUGUI pgtitle;
    public TextMeshProUGUI pgcontent;
    public TextMeshProUGUI title_word;
    public TextMeshProUGUI ctwoct;
    public TextMeshProUGUI cthreect;

    //guide img
    public GameObject GuideImg;

    //bgc
    [SerializeField]
    public Image c1bgc;
    [SerializeField]
    private Color c1color;

    [SerializeField]
    public Image c3bgc;
    [SerializeField]
    private Color c3color;

    //img
    public RawImage[] cardimg =new RawImage[2];
    public Texture hit_tex;
    public Texture shd_tex;
    public Texture egg_tex;
    public Texture buff_tex;
    

    public static int pagenum=1;

    //declare dotween
    // public RectTransform titlecard;
    // public RectTransform upbg;

    public void BtnNext(){
        if(pagenum<4){
            pagenum +=1;
            FindObjectOfType<AudioManager>().Play("btn_click");
        }
    }
    public void BtnPre(){
        if(pagenum>1){
            pagenum -=1;
            FindObjectOfType<AudioManager>().Play("btn_click");
        }
    }
    //判定頁數
    public void JudgePage(){
        switch(pagenum){
            case 1:
                ruleimg.texture=ruleTex[0];
                pgtitle.text="競速團隊賽";
                pgcontent.text="本遊戲結合競速與對戰，並採用2v2的團隊賽模式，1p 2p為一隊，3p 4p為一隊";
                leftbtn.SetActive(false);
                pagedot[0].SetActive(true);
                pagedot[1].SetActive(false);
                pagedot[2].SetActive(false);
                pagedot[3].SetActive(false);
                break;
            case 2:
                title_word.text="How to play ?";
                card.SetActive(false);
                right_content.SetActive(true);
                leftimg.SetActive(true);
                ruleimg.texture=ruleTex[1];
                pgtitle.text="計分制";
                pgcontent.text="團隊賽模式為計分制，各自努力為自己的團隊得分唄";
                leftbtn.SetActive(true);
                pagedot[0].SetActive(false);
                pagedot[1].SetActive(true);
                pagedot[2].SetActive(false);
                break;
            case 3:
                title_word.text="How to get points ?";
                right_content.SetActive(false);
                leftimg.SetActive(false);
                rightbtn.SetActive(true);
                pagedot[1].SetActive(false);
                pagedot[2].SetActive(true);
                pagedot[3].SetActive(false);
                //card
                card.SetActive(true);
                card1_ct1.SetActive(true);
                card1_ct2.SetActive(false);
                card2t1.SetActive(true);
                card2t2.SetActive(false);
                card3t1.SetActive(true);
                card3t2.SetActive(false);
                ctwoct.text="遊戲内玩家可以藉由咒語點獲得並施展咒語，撃中敵人能獲取1分";
                cthreect.text="當第一名玩家完成第2圈時，金蛋便會隨機出現，用咒語撃中能獲得5分";
                c1color=new Color (.78f,.95f,.75f,1f);
                c3color=new Color (.95f,.92f,.68f,1f);
                cthreect.color=new Color(.38f,.33f,.13f,1f);
                cardimg[0].texture=hit_tex;
                cardimg[1].texture=egg_tex;
                break;
            case 4:
                title_word.text="Spell System";
                rightbtn.SetActive(false);
                pagedot[2].SetActive(false);
                pagedot[3].SetActive(true);
                //card
                card1_ct1.SetActive(false);
                card1_ct2.SetActive(true);
                card2t1.SetActive(false);
                card2t2.SetActive(true);
                card3t1.SetActive(false);
                card3t2.SetActive(true);
                ctwoct.text="能抵禦敵人的攻撃，同時敵人撃中也無法獲取分數";
                cthreect.text="提升自己在場上優勢的咒語";
                c1color=new Color (1f,.84f,.84f,1f);
                c3color=new Color (.8f,.95f,.7f,1f);
                cthreect.color=new Color(.18f,.37f,.13f,1f);
                cardimg[0].texture=shd_tex;
                cardimg[1].texture=buff_tex;
                break;
            default:
                break;
        }
    }
    void InputBtn(){
        if(Keyboard.current.rightArrowKey.wasPressedThisFrame){
            if(pagenum<4){
                pagenum +=1;
                FindObjectOfType<AudioManager>().Play("btn_click");
            }
        }
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame){
             if(pagenum>1){
                pagenum -=1;
                FindObjectOfType<AudioManager>().Play("btn_click");
            }
        }
    }
    public void LoadScene(string SceneName){
        SceneManager.LoadScene(SceneName);
        FindObjectOfType<AudioManager>().Play("btn_click");
    }
    // Start is called before the first frame update
    void Start()
    {
        card.SetActive(false);
        right_content.SetActive(true);
        leftimg.SetActive(true);
        GuideImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        JudgePage();
        c1bgc.color=c1color;
        c3bgc.color=c3color;
        if(Keyboard.current.oKey.wasPressedThisFrame){
            GuideImg.SetActive(true);
        }
        if(Keyboard.current.oKey.wasReleasedThisFrame){
            GuideImg.SetActive(false);
        }
        if(Keyboard.current.rightAltKey.wasPressedThisFrame){
            LoadScene("Multiple_UI");
        }
        InputBtn();
    }
    
    #region Async Workflow

    /*async void AsyncUp(){ 
        await upbg.DOAnchorPosY(150,3f).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        await titlecard.DOAnchorPosY(150,3f).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
    }*/
    #endregion
}
