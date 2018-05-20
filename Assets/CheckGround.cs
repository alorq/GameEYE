using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
    // NOTAS DE APRENDIZAJE, IGNORAR: privado a publico trucho Public Class Jugador private int contador [SerializeField] Before barra de puntaje
    // Destroy(gameObject); 
	void Start () {
        player = GetComponentInParent<PlayerController>();
		
	}

    // Determinacion de los distintos estados en la colision, creo que este scrip puede ser transladado a PlayerController, pero no estpy seguro de como.
	void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Ground"){
            player.grounded = true;
            player.death = true;
        }
        if (col.gameObject.tag == "Spikes")
        {
            player.death = false;
        }
        // TriggeredExit2D, Enter2D, Stay 2D
    }
    void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = false;
        }
    }
}
