using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vitoria : MonoBehaviour
{
    public AudioSource audioVictory;
    public GameObject panel; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0; 
            panel.SetActive(true);
            audioVictory.Play();
        }
    }
}
