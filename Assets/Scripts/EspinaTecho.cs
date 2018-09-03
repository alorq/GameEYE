using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinaTecho : MonoBehaviour
{
    public GameObject origenDeteccion;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        origenDeteccion.GetComponent<Transform>().position = new Vector2(origenDeteccion.GetComponent<Transform>().position.x - Random.Range(0f,3f), origenDeteccion.GetComponent<Transform>().position.y);
    }

    void FixedUpdate()
    {
        Caidaespina();
    }

    void Caidaespina()
    {
        RaycastHit2D deteccionJugador = Physics2D.Raycast(origenDeteccion.transform.position, Vector2.down);
        if (deteccionJugador.collider.gameObject.tag == "Jugador")
        {
            rb.gravityScale = 7;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag!="Bala" && c.gameObject.tag != "Metal")
        {
            Destroy(gameObject);
        }
    }
   
}