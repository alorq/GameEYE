using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidadbala;
    private Transform player;
    private Vector2 objetivo;
    // Use this for initialization
    void Start()
    {
        //Detecion de la posicion jugador y su posterior almacenamiento en la variable objetivo.
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
        objetivo = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Utilizacion de movetowards para realizar el seguimiento de la bala a la posicin objetivo y su posterior destruccion al alcanzar esta
        transform.position = Vector2.MoveTowards(transform.position, objetivo, velocidadbala * Time.deltaTime);
        if (transform.position.x == objetivo.x && transform.position.y == objetivo.y)
        {
            Destroy(gameObject);
        }

    }
    //destruccion de las balas al atravesar objetos
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TerrenoFirme")
        {
            Destroy(gameObject);
        }
    }
}