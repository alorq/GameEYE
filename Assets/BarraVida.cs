using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Script para GameObject Barra de Vida (1.0)
 * Traduce y muestra por pantalla la vida actual del jugador. permite opcionalmente
 * mostrar el valor actual de vida con numeros. 
 *
 *Instrucciones
 * Asignar este script al objeto Barra de vida, luego asignar el GameObject relacionado
 * con la imagen que se desear modificar a la casilla "Current". Si se desea,
 * se puede tambien asignar un texto en la casilla "Texto Vida"
 * 
 *Notas
 * Este scipt requiere la existencia de los scripts complementarios
 * "Stat.cs" y "Jugador.cs", junto con un barra de vida implementada en canvas
 * El parametro "current" se refiere a el objeto que representara la vida actual del 
 * jugador
 * 
 *Parametros modificables
 * velocidadTransicion puede ser modificado desde el inspector para variar la velocidad de
 * actualizacion de la barra
 *
 *Changelog
 * Version (1.0) domingo 20 de mayo 2018
 * Creacion del script inicial
 */

public class BarraVida : MonoBehaviour {
    private float fillAmount;
    [SerializeField] private Image current;
    [SerializeField] private Text textoVida; //Opcional
    [SerializeField] private float velocidadTransicion;
    
    public float ValorMaximo { get; set; }
    public float Valor {
        set{
            string[] tmp = textoVida.text.Split(' '); //Opcional
            textoVida.text = tmp[0] + " " + value; //Opcional
            fillAmount = Traductor(value, 0, ValorMaximo, 0, 1);
        }
    }

	void Start () {
	}
	
	void Update () {
        Actualizador();
	}

    private void Actualizador(){
        if (fillAmount != current.fillAmount){
            current.fillAmount = Mathf.Lerp(current.fillAmount, fillAmount, Time.deltaTime * velocidadTransicion);
        }
    }

    private float Traductor(float valor, float inMin, float inMax, float outMin, float outMax){
        return (valor - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}