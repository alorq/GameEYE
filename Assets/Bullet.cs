using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    // Use this for initialization
    void Start()
    {
        //Detecion de la posicion jugador y su posterior almacenamiento en la variable target.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Utilizacion de movetowards para realizar el seguimiento de la bala a la posicin target y su posterior destruccion al alcanzar esta
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }

    }
    //destruccion de las balas al atravesar objetos
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}