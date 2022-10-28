using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CursorKey : MonoBehaviour
{
    RectTransform cursor;
    // Start is called before the first frame update
    void Start()
    {
        cursor= GetComponent<RectTransform>();
        Cursor.visible=false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorpos= Mouse.current.position.ReadValue();

        cursor.anchoredPosition = cursorpos;

    }
}
