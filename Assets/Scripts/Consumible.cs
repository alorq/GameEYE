using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : MonoBehaviour {
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.name == "Jugador")
        {
            audio.Play();
            Destroy(this.gameObject);
        }
    }
}
