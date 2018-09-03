using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{

    [SerializeField] public float velocidadSalto;
    [SerializeField] Transform player;
    [SerializeField] public float velocidad;
    [SerializeField] public Transform deteccion;
    [SerializeField] public float zonahostil;
    [SerializeField]  bool v;
    [SerializeField] bool s;
    [SerializeField] bool p;
    float secondsCounter = 0;
    float secondsToCount = 2;

    private bool movimientoDerecha = true;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v = true;
    }

    private void FixedUpdate()
    {
        if (v == true)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * velocidad * 1.5f * Time.deltaTime);
        }
        RaycastHit2D contactoSuelo = Physics2D.Raycast(deteccion.position, Vector2.down, 5f);
        RaycastHit2D contactoPared = Physics2D.Raycast(deteccion.position, Vector2.right, 0.1f);
        if (contactoSuelo.collider == false)
        {
            s = false;
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
        else
        {
            secondsCounter += Time.deltaTime;
            if (secondsCounter >= secondsToCount)
            {
                secondsCounter = 0;
                s = true;
            }
        }
        if (contactoPared.collider == true && contactoPared.collider.gameObject.tag != "Jugador")
        {
            p = false;
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
        else
        {
            secondsCounter += Time.deltaTime;
            if (secondsCounter >= secondsToCount)
            {
                secondsCounter = 0;
                p = true;
            }
        }
        if(p==true && s==true)
        {
            float k = transform.position.x - player.position.x;
            if (Mathf.Abs(k) < zonahostil && Mathf.Abs(transform.position.y - player.position.y) < 8f)
            {
                v = false;
                if (k < 0 && movimientoDerecha == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movimientoDerecha = false;
                }
                if (k > 0 && movimientoDerecha == false)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movimientoDerecha = true;
                }
            }
            else
            {
                v = true;
            }
        }
        
        
           
        
        
    }
}
