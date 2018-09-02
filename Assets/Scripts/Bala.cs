using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    [SerializeField] Transform player;
    private Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Jugador" && col.gameObject.tag != "Bala")
        {
            Destroy(gameObject);
        }

    }
}
