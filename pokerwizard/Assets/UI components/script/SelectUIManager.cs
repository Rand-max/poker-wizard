using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectUIManager : MonoBehaviour
{
    //key
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;

    Rigidbody2D key1body;

    Vector2 key1pos;
    public float movespeed;

    //lock
    public GameObject lock_club;
    public GameObject lock_diamond;
    public GameObject lock_heart;
    public GameObject lock_spade;

    Material ClubMat;
    Material DiamondMat;
    Material HeartMat;
    Material SpadeMat;

    public TextMeshProUGUI num_club;
    public TextMeshProUGUI num_diamond;
    public TextMeshProUGUI num_heart;
    public TextMeshProUGUI num_spade;

    public GameObject cir1;
    public GameObject cir2;
    public GameObject cir3;
    public GameObject cir4;

    void Start()
    {
        key1body=key1.GetComponent<Rigidbody2D>(); 
        key1pos=key1.GetComponent<Transform>().position;
    }

    void Update()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
           key1pos.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            key1pos.x -=1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            key1pos.x +=1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            key1pos.y -= 1.0f;
        }*/
    }
    /*private void FixedUpdate(){
        key1body.MovePosition(key1body.position+key1pos*movespeed*Time.fixedDeltaTime);
    }*/
}
