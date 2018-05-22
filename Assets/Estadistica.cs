using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script independiente asociado a Player (1.0)
 * asigna el valor maximo y actual de vida del jugador para luego enviar los datos
 * a "BarraVida.cs" para poder mostrarlos por pantalla
 *
 *Instrucciones
 * Asignar el Game Object correspondiente a la barra de vida a la seccion "Script Vida"
 * del inspector
 * 
 *Notas
 * Este scipt requiere la existencia de los scripts complementarios
 * "Jugador.cs" y "BarraVida.cs", junto con un barra de vida implementada en canvas
 * 
 *Parametros modificables
 * Maximo Vida y Valor Actual se pueden modificar desde el inspector
 * 
 *Autores
 * FlaminEon
 *
 *Changelog
 * Version (1.0) domingo 20 de mayo 2018
 * Creacion del script inicial
 */

[Serializable]
public class Estadistica{
    [SerializeField] private BarraVida vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private float valorActual;

    public float ValorActual{
        get{
            return valorActual;
        }
        set{
            this.valorActual = Mathf.Clamp(value, 0, MaximoVida);
            vida.Valor = valorActual;
        }
    }

    public float MaximoVida{
        get{
            return maximoVida;
        }
        set{
            maximoVida = value;
            vida.ValorMaximo = maximoVida;
        }
    }

    public void Inicializar(){
        this.MaximoVida = maximoVida;
        this.ValorActual = valorActual;
    }
}