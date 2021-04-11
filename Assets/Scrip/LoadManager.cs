using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider mySlider;

    public void LoadNextScene(string SceneName)
    {
        StartCoroutine(LoadScene(SceneName));
    }

    IEnumerator LoadScene(string SceneName)
    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            mySlider.value = operation.progress;
            if(operation.progress>=0.9f)
            {
                mySlider.value = 1;
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
