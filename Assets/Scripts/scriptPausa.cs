using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPausa : MonoBehaviour {

    public static bool playerIsDed = false;
    public static bool juegoEnPausa;
    public GameObject IUPausa;
    public GameObject IUGameOver;

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
        playerIsDed = false;
        Debug.Log("Mistakes were made...");
        yield return new WaitForSeconds(2f);
        Debug.Log("Activando Game Over");
        IUGameOver.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Juego ahora en pausa");
    }
}
