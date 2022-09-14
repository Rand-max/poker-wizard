using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject cd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartDelay(){
        Time.timeScale=0;
        float pauseTime=Time.realtimeSinceStartup+4.25f;
        while (Time.realtimeSinceStartup<pauseTime)
        {
            yield return 0;
        }
        cd.gameObject.SetActive(false);
        Time.timeScale=1;
    }
}
