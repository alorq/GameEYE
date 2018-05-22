using UnityEngine;

/*Script para Main Camera (1.2)
 * Este script permitira que la camara siga libremente al objeto que se 
 * especifique. Ademas, cuenta con un suavizado en el movimiento para
 * generar mas comodidad visual
 *
 *Instrucciones
 * Agregar este script a "Main Camera" y arrastrar el objeto a seguir
 * a la casilla "Target" en el inspector, luego se recomienda modificar 
 * el valor z en "offset" a "-10" y no cambiar el valor de "velocidad" a
 * menos que sea necesario
 * 
 *Notas
 * FixedUpdate y LateUpdate se pueden intercambiar en el caso de que 
 * el movimiento de la camara presente problemas
 * 
 *Parametros modificables
 * Target 
 * Velocidad
 * offset
 * 
 *Autores
 * FlaminEon
 *
 *Changelog
 * Version (1.0) lunes 7 de mayo 2018
 * Creacion del script inicial
 *
 * Version (1.1) domingo 13 de mayo 2018
 * Modificacion de la documentacion y subida del script a GitHub
 *
 * Version (1.2) martes 15 de mayo 2018
 * Correccion de errores menores
 */

public class Camara : MonoBehaviour
{
    private Transform Target;
    private float Velocidad = 0.125f;
    private Vector3 offset;

    void FixedUpdate()
    {
        Vector3 posicion = Target.position + offset;
        Vector3 suavizado = Vector3.Lerp(transform.position, posicion, Velocidad);
        transform.position = suavizado;
    }
}