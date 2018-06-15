using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMov : MonoBehaviour {
    [SerializeField] private float velocidadmovi;
    [SerializeField] private Transform objetivo;
    private Vector3 inicio, final;
    private Transform jugador;
    // Use this for initialization
    void Start() {
        objetivo.parent = null;
        inicio = transform.position;
        final = objetivo.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vel = velocidadmovi * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, objetivo.position, vel);
        float h = Input.GetAxis("Horizontal");
        if (transform.position == objetivo.position)
        {
            objetivo.position = (objetivo.position == inicio) ? final : inicio;
        }

    }
        void OnCollisionStay2D(Collision2D col)
        {
            float p = Input.GetAxis("Horizontal");
            if (col.gameObject.tag == "Jugador" && jugador != null)
            {
                if (p < 0)
                {
                    transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
                }
                if (p > 0)
                {
                    transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                }
            }
        }
}
