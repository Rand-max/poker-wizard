using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CursorKey : MonoBehaviour
{
    RectTransform cursor;
    public Texture2D cursortext;
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
        
        Vector2 cursorpos= Mouse.current.position.ReadValue();

        cursor.anchoredPosition = cursorpos;
        
    }
}
