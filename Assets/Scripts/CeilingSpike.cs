using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpike : MonoBehaviour{
    public Transform deteccion;
    public Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        caida();
	}

    void caida (){
        RaycastHit2D deteccionJugador = Physics2D.Raycast(deteccion.position, Vector2.down, 50f);
        if (deteccionJugador.collider.gameObject.tag == "Jugador"){
            rb.gravityScale = 3;
        }
    }
}
