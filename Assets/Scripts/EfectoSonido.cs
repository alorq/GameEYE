using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    public AudioSource Fx;
    public AudioClip hover;

    public void hoverSound()
    {
        Fx.PlayOneShot(hover);
    }
}
