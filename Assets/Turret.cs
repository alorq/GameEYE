using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private float timeshot;
    public float outshot;
    public GameObject proyectile;

	// Use this for initialization
	void Start () {
        timeshot = outshot;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //establecimiento de los tiempos de disparo, esto para determinar cuan seguido la torreta creara y lanzara disparos
		if (timeshot <= 0)
        {
            Instantiate(proyectile, transform.position, Quaternion.identity);
            timeshot = outshot;
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
	}
}
