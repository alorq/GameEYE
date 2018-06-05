/*
 * Este script se encarga del proceso de pausa durante el juego.
 * Permite tambien reiniciar el nivel asi como salir de el.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour {
    // Dado que los botones son hijos de TextoPausa, estaran ocultos
    // junto a el, es decir, solo TextoPausa requiere el tag "enPausa"
    GameObject objetoPausa;

	void Start ()
    {
        Time.timeScale = 1;
        objetoPausa = GameObject.FindGameObjectWithTag("enPausa");
        objetoPausa.SetActive(false);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                objetoPausa.SetActive(true);
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                objetoPausa.SetActive(false);
            }
        }
	}

    // Permite salir de pausa sin requerir apretar 'P'
    // mediante un boton grafico
    public void botonContinuar()
    {
        Time.timeScale = 1;
        objetoPausa.SetActive(false);
    }

    // Reinicia la escena volviendola a cargar
    public void botonReiniciar()
    {
        SceneManager.LoadScene("cxcvxv");
    }

    // Sale del nivel y retorna al menu principal
    public void botonSalir()
    {
        SceneManager.LoadScene("Menu");
    }
}
