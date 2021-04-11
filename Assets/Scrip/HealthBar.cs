using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthPointImage;
    public Image firePointImage;

    private BeatTest beat;

    private void Awake()
    {
        beat = GameObject.FindGameObjectWithTag("Beat").GetComponent<BeatTest>();
    }

    private void Update()
    {
        healthPointImage.fillAmount = beat.currentHealth / beat.maxHealth;
        firePointImage.fillAmount = beat.currentFire / beat.maxFire;
       
    }
}
