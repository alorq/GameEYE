using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    public float velocidadcaida;
    public float velocidadmovi;
    public float fuerzasalto;
    public bool terrenofirme;
    public bool colgado;

    private Rigidbody2D rbd;
    private Animator ani;
    private bool disparadorsalto;
    private int vida = 250;
    private int vidaActual;
    private bool vivo = true;
    private double count = 100;

    // Use this for initialization
    
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        vidaActual = vida;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("Velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("Caida", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("Terrenofirme", terrenofirme);
        ani.SetBool("Colgado", colgado);
        ani.SetBool("Vivo", vivo);

        if (Input.GetKeyDown(KeyCode.UpArrow) && terrenofirme)
        {
            disparadorsalto = true;
        }
    }
    void FixedUpdate()
    {
        if (vidaActual <= 0)
        {
            vivo = false;
            count -= 1.2;
        }
        
        if (vivo)
        {
            float p = Input.GetAxis("Horizontal");

            if (p > 0f)
            {
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                rbd.velocity = new Vector2(velocidadmovi, rbd.velocity.y);
            }
            else if (p < 0f)
            {
                transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
                rbd.velocity = new Vector2(-velocidadmovi, rbd.velocity.y);
            }
            else
            {
                rbd.velocity = new Vector2(0, rbd.velocity.y);
            }
            if (disparadorsalto)
            {
                rbd.AddForce(Vector2.up * fuerzasalto, ForceMode2D.Impulse);
                disparadorsalto = false;
            }   
        }
        if (count < 0)
        {
            SceneManager.LoadScene("Menu");
        }
        Debug.Log(vidaActual);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme = false;
        }
        if (col.gameObject.tag == "Muro")
        {
            colgado = false;
            terrenofirme = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme = true;
            colgado = false;
        }
        if (col.gameObject.tag == "Muro")
        {
            terrenofirme = true;
            colgado = true;
        }
        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Muro")
        {
            rbd.AddForce(Vector2.down * velocidadcaida, ForceMode2D.Impulse);
            terrenofirme = true;
        }
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme=true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Espina" && vidaActual>0)
        {
            vidaActual -= 2;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bala")
        {
            vidaActual -= 45;
        }
    }


}