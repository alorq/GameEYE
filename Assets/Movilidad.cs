using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movilidad : MonoBehaviour {
    [SerializeField] private float velocidadmovi;
    private Rigidbody2D rbd;
    [SerializeField] private bool derecho;

    // Use this for initialization
    void Start() {
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = new Vector2(velocidadmovi, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (rbd.velocity.x > 1)
        {
            derecho = true;
        }
        if (rbd.velocity.x < -1)
        {
            derecho = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Terreno" || col.gameObject.tag == "Muro")
        {
            if (derecho == true)
            {
                rbd.velocity = new Vector2(-velocidadmovi, 0);
            }
            if (derecho == false)
            {
                rbd.velocity = new Vector2(velocidadmovi, 0);
            }
        }
    }
}
