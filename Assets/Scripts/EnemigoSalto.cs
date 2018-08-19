using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSalto : MonoBehaviour
{
    [SerializeField] public float velocidadSalto;
    public Rigidbody2D rb;
    public float tiempo = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(rutinaSalto());
    }

    IEnumerator rutinaSalto()
    {
        yield return new WaitForSeconds(tiempo);
        rb.AddForce(transform.up * velocidadSalto, ForceMode2D.Impulse);
        StartCoroutine(rutinaSalto());
    }
}