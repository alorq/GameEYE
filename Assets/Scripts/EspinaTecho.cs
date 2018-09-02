using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinaTecho : MonoBehaviour{
    public GameObject origenDeteccion;
    private Rigidbody2D rb;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
	}

    void FixedUpdate () {
        caidaEspina();
	}

    void caidaEspina(){
        RaycastHit2D deteccionJugador = Physics2D.CircleCast(origenDeteccion.transform.position, 10f, Vector2.down, 10f);
        if (deteccionJugador.collider.gameObject.tag == "Jugador")
        {
            rb.gravityScale = 3;
            Debug.Log("Lol");
        }
    }
}
