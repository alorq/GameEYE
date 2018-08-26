using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : MonoBehaviour {
    private AudioSource sonido;

	void Start () {
        sonido = gameObject.GetComponent<AudioSource>();
        
    }
	
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Jugador")
        {
            sonido.Play();
            this.GetComponent<CircleCollider2D>().enabled = !this.GetComponent<CircleCollider2D>().enabled;
            this.gameObject.GetComponent<Renderer>().enabled = false;
            Object.Destroy(this.gameObject, sonido.clip.length);
        }
    }
}
