using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReferenceController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded+=OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    }
}
