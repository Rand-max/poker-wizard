using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image Loadingbar_fill;
    
    public void LoadtheScene(int sceneID){
        StartCoroutine(LoadSceneAsync(sceneID));
    }
    IEnumerator LoadSceneAsync(int sceneID){
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneID);
        LoadingScreen.SetActive(true);

        //progress bar
        while (!operation.isDone)
        {
            float progress_val=Mathf.Clamp01(operation.progress/0.9f);
            Loadingbar_fill.fillAmount=progress_val;
            yield return null;
        }
    }
}
