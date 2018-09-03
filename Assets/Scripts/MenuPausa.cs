using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    private float tiempo = 1f;

    public void Confirmar()
    {
        Debug.Log("Regresando al menú principal");
        StartCoroutine(confirmarSalida());
    }

    IEnumerator confirmarSalida()
    {
        Debug.Log("Esperando SFX");
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene(0);
        Debug.Log("Nivel cerrado");
        scriptPausa.juegoEnPausa = false;
    }
}