using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject gameover;

    public void restartGame()
    {
        gameover.SetActive(false);
        SceneManager.LoadScene("LearnScene");
        
    }
}
