using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cena : MonoBehaviour
{
    public void MudarCena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    public void sair()
    {
        Application.Quit();
    }
}