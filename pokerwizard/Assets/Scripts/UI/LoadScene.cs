using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image Loadingbar_fill;
    
    public void LoadtheScene(string sceneName){
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneName);
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
