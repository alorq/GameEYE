/*
 * Este script se encarga del proceso de pausa durante el juego.
 * Permite tambien reiniciar el nivel asi como salir de el.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    // Dado que los botones son hijos de Pausa, estaran ocultos
    // junto a el, es decir, solo Pausa requiere el tag "enPausa"
    GameObject objetoPausa;
    // Lo mismo aplica para Muerte
    GameObject objetoMuerte;
    // Para poder acceder a la variable de vivo del Jugador
    ControlJugador controlJugador;

    // Se inicia con tiempo normal y se hallan los GameObject que corresponden a los
    // menues
	void Start ()
    {
        Time.timeScale = 1;
        objetoPausa = GameObject.FindGameObjectWithTag("enPausa");
        objetoPausa.SetActive(false);
        objetoMuerte = GameObject.FindGameObjectWithTag("enMuerte");
        objetoMuerte.SetActive(false);

        controlJugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<ControlJugador>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) && controlJugador.vivo == true)
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

        if(controlJugador.vivo == false)
        {
            Time.timeScale = 0;
            objetoMuerte.SetActive(true);
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
