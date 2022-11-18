using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject[] cameraList;
    public float countdown;
    public  bool isCounting;
    private int currentCamera;
    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < cameraList.Length; i++){
        //     cameraList[i].gameObject.SetActive(false);
        // }
     
        // if (cameraList.Length > 0){
        //     cameraList[0].gameObject.SetActive (true);
        // }
        countdown=3;
        cameraList[0].gameObject.SetActive(true);
        cameraList[1].gameObject.SetActive(false);
        isCounting=true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonUp(0)){
        //     currentCamera ++;
        //     if (currentCamera < cameraList.Length){
        //         cameraList[currentCamera - 1].gameObject.SetActive(false);
        //         cameraList[currentCamera].gameObject.SetActive(true);
        //     }
        //     else {
        //         cameraList[currentCamera].gameObject.SetActive(true);
        //     }
        // }
        if(isCounting){
            countdown-=Time.deltaTime;
        }
        if(countdown<0){
            countdown=0;
        }
        if(countdown==0){
            cameraList[0].gameObject.SetActive(false);
            cameraList[1].gameObject.SetActive(true);
            isCounting=false;
        }
        if(!isCounting){
            countdown=3;
        }
    }
}
