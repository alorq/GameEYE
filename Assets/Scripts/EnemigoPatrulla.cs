using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{

    [SerializeField] public float velocidadSalto;
    [SerializeField] public float velocidad;
    [SerializeField] public Transform deteccion;
    private bool movimientoDerecha = true;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    void Movimiento()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        RaycastHit2D contactoSuelo = Physics2D.Raycast(deteccion.position, Vector2.down, 5f);
        RaycastHit2D contactoPared = Physics2D.Raycast(deteccion.position, Vector2.right, 0.1f);
        if (contactoSuelo.collider == false)
        {
            if (movimientoDerecha == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movimientoDerecha = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movimientoDerecha = true;
            }
        }
        if (contactoPared.collider == true && contactoPared.collider.gameObject.tag != "Jugador")
        {
            if (movimientoDerecha == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movimientoDerecha = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movimientoDerecha = true;
            }
        }
    }
}
