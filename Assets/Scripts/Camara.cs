/*
 * Camara es un script asociado al GameObject Camara que 
 * se encarga de seguir al jugador durante el nivel
 */
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform Target;
    public float Velocidad = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 posicion = Target.position + offset;
        Vector3 suavizado = Vector3.Lerp(transform.position, posicion, Velocidad);
        transform.position = suavizado;
    }
}
