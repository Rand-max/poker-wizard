using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CursorKey : MonoBehaviour
{
    RectTransform cursor;
    public float cursoraccel=1f;
    public float Maxspeed=5f;
    public float cursorfloattime=0.5f;
    public Vector2 currentaccel;
    public Vector2 currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cursor= GetComponent<RectTransform>();
        //Cursor.SetCursor(cursortext,Vector2.zero,CursorMode.Auto);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    public void movecursor(Vector2 dire){
        currentaccel=dire*cursoraccel;
    }
}
