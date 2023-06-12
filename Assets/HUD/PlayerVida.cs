using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    public float Health;
    public Slider HealthBar;
    public GameObject GameOver;

    void Start()
    {
        HealthBar.value = Health;
    }

    void Update()
    {
        if (Health == 0)
        {
            GameOver.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tiro")
        {
            Health -= 10;
            HealthBar.value = Health;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(8))
        {
            Health -= 10;
            HealthBar.value = Health;
        }
    }
}
