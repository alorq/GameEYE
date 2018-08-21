using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    private Rigidbody2D rbd;

    public EstadoImpulso impulso;
    public float cronometro;
    public float maximoTiempo = 20f;

    public Vector2 velocidadActual;

    private void Start()
    {
        rbd = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch(impulso)
        {
            case EstadoImpulso.Listo:
                bool teclaPresionada = Input.GetKeyDown(KeyCode.LeftShift);
                if(teclaPresionada)
                {
                    rbd.velocity = new Vector2(rbd.velocity.x * 3f, rbd.velocity.y);
                    velocidadActual = rbd.velocity;
                    impulso = EstadoImpulso.EnImpulso;
                }
                break;
            case EstadoImpulso.EnImpulso:
                cronometro += Time.deltaTime * 3;

                if(cronometro >= maximoTiempo)
                {
                    cronometro = maximoTiempo;
                    rbd.velocity = velocidadActual;
                    impulso = EstadoImpulso.Reposo;
                }
                break;
            case EstadoImpulso.Reposo:
                cronometro -= Time.deltaTime;
                if(cronometro <= 0)
                {
                    cronometro = 0;
                    impulso = EstadoImpulso.Listo;
                }
                break;
        }
    }

    // Se utilizan 3 estados que permiten determinar si realizar impulso o no
    public enum EstadoImpulso { Listo, EnImpulso, Reposo }
}
