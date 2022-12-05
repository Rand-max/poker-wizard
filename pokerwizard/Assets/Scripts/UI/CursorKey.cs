using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;
using TMPro;

public class CursorKey : MonoBehaviour
{
    public string cursorname;
    RectTransform cursor;
    public float cursoraccel=1f;
    public float Maxspeed=5f;
    public float cursorfloattime=0.5f;
    public Vector2 currentaccel;
    public Vector2 currentSpeed;
    //Method
    [SerializeField]  GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    SelectImgBtn imgbtn;
    RuleBtn rlbtn;
    ButtonEvent btne;
    void Start()
    {
        cursor= GetComponent<RectTransform>();
        //Cursor.SetCursor(cursortext,Vector2.zero,CursorMode.Auto);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Keyboard.current.rightCtrlKey.wasPressedThisFrame){
           LoadScene("Rules");
           FindObjectOfType<AudioManager>().Play("book_flip");
        }
        
        //Vector2 cursorpos= Mouse.current.position.ReadValue();
        //cursor.anchoredPosition = cursorpos;
        if(currentSpeed.magnitude>Maxspeed){
            currentSpeed=currentSpeed.normalized*Maxspeed;
        }
        currentSpeed+=currentaccel;
        if(cursor.anchoredPosition.x>Screen.width){
            if(currentSpeed.x>0){
                currentSpeed.x=0;
            }
        }
        if(cursor.anchoredPosition.x<0){
            if(currentSpeed.x<0){
                currentSpeed.x=0;
            }
        }
        if(cursor.anchoredPosition.y>Screen.height){
            if(currentSpeed.y>0){
                currentSpeed.y=0;
            }
        }
        if(cursor.anchoredPosition.y<0){
            if(currentSpeed.y<0){
                currentSpeed.y=0;
            }
        }
        cursor.anchoredPosition+=currentSpeed*Time.deltaTime;
        if(currentaccel.magnitude<=new Vector2(0.1f,0.1f).magnitude){
            currentSpeed=Vector2.Lerp(currentSpeed,Vector2.zero,cursorfloattime);
        }
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = cursor.anchoredPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count > 0){
            if(results[0].gameObject.GetComponent<SelectImgBtn>()){
                if(imgbtn!=null&&imgbtn!=results[0].gameObject.GetComponent<SelectImgBtn>()){
                    imgbtn.OnPointerExit(this);
                }
                imgbtn=results[0].gameObject.GetComponent<SelectImgBtn>();
                imgbtn.OnPointerEnter(this);
            }
            else{
                if(imgbtn!=null){
                    imgbtn.OnPointerExit(this);
                    imgbtn=null;
                }
            }
            if(results[0].gameObject.GetComponent<RuleBtn>()){
                if(rlbtn!=null&&rlbtn!=results[0].gameObject.GetComponent<RuleBtn>()){
                    rlbtn.OnPointerExit(this);
                }
                rlbtn=results[0].gameObject.GetComponent<RuleBtn>();
                rlbtn.OnPointerEnter(this);
            }
            else{
                if(rlbtn!=null){
                    rlbtn.OnPointerExit(this);
                    rlbtn=null;
                }
            }
            if(results[0].gameObject.GetComponent<ButtonEvent>()){
                if(btne!=null&&btne!=results[0].gameObject.GetComponent<ButtonEvent>()){
                    btne.OnPointerExit(this);
                }
                btne=results[0].gameObject.GetComponent<ButtonEvent>();
                btne.OnPointerEnter(this);
            }
            else{
                if(btne!=null){
                    btne.OnPointerExit(this);
                    btne=null;
                }
            }
        }
    }
    public void LoadScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }
    public void movecursor(Vector2 dire){
        currentaccel=dire*cursoraccel;
    }
    public void clickcursor(){
        if(imgbtn!=null){
            imgbtn.OnPointerDown(this,imgbtn.gameObject);
        }
        if(rlbtn!=null){
            rlbtn.OnPointerDown(this);
        }
        if(btne!=null){
            btne.OnPointerDown(this);
        }
    }
}
