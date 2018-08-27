using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    private float tiempo = 1f;

    public void Jugar()
    {
        StartCoroutine(jugarNivel());
    }

    public void Salir()
    {
        Debug.Log("Saliendo de la aplicación");
        StartCoroutine(salirAplicacion());
    }

    IEnumerator jugarNivel()
    {
        Debug.Log("Esperando SFX");
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Nivel Cargado");
    }

    IEnumerator salirAplicacion()
    {
        Debug.Log("Esperando SFX");
        yield return new WaitForSeconds(tiempo);
        Debug.Log("Aplicación Cerrada");
        Application.Quit();
    }
}