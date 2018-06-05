using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private float velocidadbala;
    private Transform player;
    private Vector2 objetivo;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
        objetivo = new Vector2(player.position.x, player.position.y);
    }

    void FixedUpdate(){
        transform.position = Vector2.MoveTowards(transform.position, objetivo, velocidadbala * Time.deltaTime);
        if (transform.position.x == objetivo.x && transform.position.y == objetivo.y){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Terreno"){
            Destroy(gameObject);
        }
    }
}