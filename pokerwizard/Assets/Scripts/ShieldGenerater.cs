using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerater : MonoBehaviour
{
    public GameObject sheild;
    public bool isGenerate;
    public bool isDestroy;
    public float time;
    public float Descountdown;
    private GameObject tate;
    // Start is called before the first frame update
    void Start()
    {
        isGenerate = false;
        isDestroy = true;
        time = 10;
        Descountdown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateShield();
        DestroyShield();
    }
    void GenerateShield()
    {
        //Again();
        if (Input.GetKeyDown(KeyCode.B)&&isDestroy)
        {
            ReCreate();
            isGenerate = true; 
        }
    }
    void DestroyShield()
    {
        if (isGenerate)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            time = 0;
        }
        if (time==0)
        {
            isGenerate = false;
            isDestroy = false;
            Destroy(tate);
        }
    }
    void ReCreate()
    {
        tate = Instantiate(sheild, this.transform.position, Quaternion.identity);
    }
    void Again()
    {
        if (!isDestroy)
        {
            Descountdown -= Time.deltaTime;
        }
        if (Descountdown <= 0)
        {
            Descountdown = 0;
        }
        if (Descountdown == 0)
        {
            isDestroy = true;
        }
    }
}
