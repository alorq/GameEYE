using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script para Player (1.0)
 * Este script recibira la informacion del cambio de puntos de vida del jugador
 * y se la entregara al script de "Stat.cs"
 *
 *Instrucciones
 * Asignar este script al objeto jugador. Si existe "Stat.cs" entonces se pueden 
 * modificar las variables de maximo y minimo de vida desde el inspector
 * 
 *Notas
 * Este scipt requiere la existencia de los scripts complementarios
 * "Stat.cs" y "BarraVida.cs", junto con un barra de vida implementada en canvas
 * 
 *Parametros modificables
 * En el Update se pueden especificar los modificadores de vida como se
 * requiera
 * 
 *Autores
 * FlaminEon
 *
 *Changelog
 * Version (1.0) domingo 20 de mayo 2018
 * Creacion del script inicial
 */

public class Jugador : MonoBehaviour {
    [SerializeField] private Stat scriptStat;

    void Awake(){
        scriptStat.Inicializar();
    }

    void Start () {	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)){
            scriptStat.ValorActual -= 10;
        }
        if (Input.GetKeyDown(KeyCode.W)){
            scriptStat.ValorActual += 10;
        }
    }
}
