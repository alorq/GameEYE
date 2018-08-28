using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPausa : MonoBehaviour {

    public static bool juegoEnPausa;
    public GameObject IUPausa;

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
}
