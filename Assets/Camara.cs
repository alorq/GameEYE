﻿using UnityEngine;

public class Camara : MonoBehaviour{
    [SerializeField]private Transform Target;
    private float Velocidad = 0.125f;
    [SerializeField]private Vector3 offset;

    void LateUpdate(){
        Vector3 posicion = Target.position + offset;
        Vector3 suavizado = Vector3.Lerp(transform.position, posicion, Velocidad);
        transform.position = suavizado;
    }
}
