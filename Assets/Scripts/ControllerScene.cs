using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScene : MonoBehaviour
{
    [SerializeField]
    int NumEscena = 2;
    
    public void IniciarJuego()
    {
        SceneManager.LoadScene(NumEscena);
    }
}
