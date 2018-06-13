using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaEnemigo : MonoBehaviour {
    [SerializeField] public float velocidad;
    [SerializeField] public Transform deteccion;
    private bool movimientoDerecha = true;
	
	void Update () {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        RaycastHit2D contacto = Physics2D.Raycast(deteccion.position, Vector2.down, 2f);
        if (contacto.collider == false) {
            if (movimientoDerecha == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movimientoDerecha = false;
            }
            else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movimientoDerecha = true;
            }
        }
	}
}
