using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using TMPro;

public class CursorKey : MonoBehaviour
{
    RectTransform cursor;
    public float cursoraccel=1f;
    public float Maxspeed=5f;
    public float cursorfloattime=0.9f;
    public float currentaccel;
    public Vector2 currentSpeed;
    public VirtualMouseInput vm;
    public float innertimer;
    // Start is called before the first frame update
    void Start()
    {
        cursor= GetComponent<RectTransform>();
        //Cursor.SetCursor(cursortext,Vector2.zero,CursorMode.Auto);
        Cursor.visible = false;
        vm=gameObject.GetComponent<VirtualMouseInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Vector2 cursorpos= Mouse.current.position.ReadValue();
        //cursor.anchoredPosition = cursorpos;
        vm.cursorSpeed+=currentaccel;
        if(vm.cursorSpeed>Maxspeed){
            vm.cursorSpeed=Maxspeed;
        }
        
        if(vm.cursorTransform.anchoredPosition.x>Screen.width){
            vm.cursorTransform.anchoredPosition+=new Vector2(Screen.width-vm.cursorTransform.anchoredPosition.x,0);
        }
        if(vm.cursorTransform.anchoredPosition.x<0){
            vm.cursorTransform.anchoredPosition+=new Vector2(-vm.cursorTransform.anchoredPosition.x,0);
        }
        if(vm.cursorTransform.anchoredPosition.y>Screen.height){
            vm.cursorTransform.anchoredPosition+=new Vector2(0,Screen.height-vm.cursorTransform.anchoredPosition.y);
        }
        if(vm.cursorTransform.anchoredPosition.y<0){
            vm.cursorTransform.anchoredPosition+=new Vector2(0,-vm.cursorTransform.anchoredPosition.y);
        }
        //vm.cursorTransform.anchoredPosition+=currentSpeed*Time.deltaTime;
        //cursor.anchoredPosition+=currentSpeed*Time.deltaTime;
        if(currentaccel<=new Vector2(0.1f,0.1f).magnitude){
            //currentSpeed=Vector2.Lerp(currentSpeed,Vector2.zero,cursorfloattime);
            vm.cursorSpeed=cursorfloattime/vm.cursorSpeed;
        }
    }
    public void movecursor(Vector2 dire){
        currentaccel=dire.magnitude*cursoraccel;
    }
}
