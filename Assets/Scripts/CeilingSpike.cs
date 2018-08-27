using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpike : MonoBehaviour{
    public Transform deteccionInicio, deteccionFin;
    public Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	void Update () {
        raycast();
    }
	void FixedUpdate () {
        caida();
	}

    void caida (){
        RaycastHit2D deteccionJugador = Physics2D.Raycast(deteccionInicio.position, Vector2.down, 50f);
        if (deteccionJugador.collider.gameObject.tag == "Jugador"){
            rb.gravityScale = 3;
        }
    }

    void raycast()
    {
        Debug.DrawLine(deteccionInicio.position, deteccionFin.position, Color.green);
    }
}
