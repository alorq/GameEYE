using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptPausa : MonoBehaviour {

    public static bool endGame = false;
    public static bool playerIsDed = false;
    public static bool juegoEnPausa;
    public GameObject IUPausa;
    public GameObject IUGameOver;
    public GameObject IUEnd;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa)
            {
                Continuar();
            }
            else
            {
                Detener();
            }

        }

        if (playerIsDed)
        {
            StartCoroutine(GameOver());
        }

        if (endGame)
        {
            StartCoroutine(End());
        }

    }

    public void Continuar()
    {
        IUPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoEnPausa = false;
    }

    void Detener()
    {
        IUPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoEnPausa = true;
    }

    IEnumerator GameOver()
    {
        Debug.Log("Mistakes were made...");
        playerIsDed = false;
        yield return new WaitForSeconds(2f);
        Debug.Log("Activando Game Over");
        IUGameOver.SetActive(true);
        Debug.Log("Juego ahora en pausa");
    }

    IEnumerator End()
    {
        Debug.Log("The end is near...");
        IUEnd.SetActive(true);
        yield return new WaitForSeconds(3f);
        Debug.Log("volviendo al menu principal");
        SceneManager.LoadScene(0);
        endGame = false;
    }
}
