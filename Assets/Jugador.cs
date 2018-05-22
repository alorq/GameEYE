using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador : MonoBehaviour {
    [SerializeField] private Estadistica vida;
    private bool vivo = true;

    void Awake() {
        vida.Inicializar();

    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Espina")
        {
            vida.ValorActual -= 2;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bala")
        {
            vida.ValorActual -= 45;
        }
    }

    private void FixedUpdate()
    {
        if (vida.ValorActual == 0) {
            vivo = false;
        }
    }

    public bool Vivo
    {
        get
        {
            return vivo;
        }
        set
        {
            this.vivo = vivo;
        }
    }
}
