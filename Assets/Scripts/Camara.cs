/*
 * Camara es un script asociado al GameObject Camara que 
 * se encarga de seguir al jugador durante el nivel
 */
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] public float altura;
    public Transform Target;
    public float Velocidad = 0.125f;
    public Vector3 offset;
   

    void FixedUpdate()
    {
        if (Target.position.y > altura)
        {
            Vector3 posicion = Target.position + offset;
            Vector3 suavizado = Vector3.Lerp(new Vector3(Target.position.x, altura, Target.position.z), posicion, Velocidad));
            transform.position = suavizado;
        }
        else
        {
            Vector3 posicion = Target.position + offset;
            Vector3 suavizado = Vector3.Lerp(transform.position, posicion, Velocidad);
            transform.position = suavizado;
        }
        
    }
}
