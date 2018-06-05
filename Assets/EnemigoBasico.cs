using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBasico : MonoBehaviour {
    [SerializeField] private LayerMask enemigo;
    [SerializeField] private float velocidad = 1;
    private Rigidbody2D rb;
    private Transform sprite;
    private float ancho;

	void Start () {
        //inicializacion de elementos y busqueda del borde del sprite
        sprite = this.sprite;
        rb = this.GetComponent<Rigidbody2D>();
        ancho = this.GetComponent<SpriteRenderer>().bounds.extents.x;
	}
	
	void FixedUpdate () {
        //Revisa si en el borde del sprite hay suelo
        Vector2 borde = sprite.position - sprite.right * ancho;
        bool suelo = Physics2D.Linecast(borde, borde + Vector2.down, enemigo);

        //Si deja de tocar suelo el sprite va a cambiar de direccion
        if (!suelo){
            Vector3 rotacion = sprite.eulerAngles;
            rotacion.y += 180;
            sprite.eulerAngles = rotacion;
        }

        //Movimiento constante
        Vector2 movimiento = rb.velocity;
        movimiento.x = velocidad;
        rb.velocity = movimiento;
	}
}
