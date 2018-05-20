using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float max;
    public float jumpp;
    public bool grounded;
    public bool death;

    private Rigidbody2D rbd;
    private Animator ani;
    private bool jump;

    // Use this for initialization
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Control de ñas animaciones en base a las distintas variables de posicion y velocidad
        ani.SetFloat("Speed", Mathf.Abs(rbd.velocity.x));
        ani.SetBool("Grounded", grounded);
        ani.SetBool("Death", death);
        //*
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        //Adhesion de fuerza al jugador según las flechaz horizontales, además de establecimiento de su limite
        float p = Input.GetAxis("Horizontal");
        //Cambio en la dirección del spritem de jugador acorde a su dirección de velocidad
        if (p > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rbd.velocity = new Vector2(max, rbd.velocity.y);
        }
        else if (p < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rbd.velocity = new Vector2(-max, rbd.velocity.y);
        }
        else
        {
            rbd.velocity = new Vector2(0, rbd.velocity.y);
        }
            //Impulso del salto en caso de presionar la flecha superior*
        if (jump)
        {
            rbd.AddForce(Vector2.up * jumpp, ForceMode2D.Impulse);
            jump = false;
        }



    }
}