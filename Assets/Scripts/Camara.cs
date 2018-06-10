/*
 * Camara es un script asociado al GameObject Camara que 
 * se encarga de seguir al jugador durante el nivel
 */
using UnityEngine;

public class Camara : MonoBehaviour{
    private GameObject jugador;
    private Vector3 offset;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        offset = transform.position - jugador.transform.position;
    }
    void LateUpdate()
    {
        transform.position = jugador.transform.position + offset;
    }
}
