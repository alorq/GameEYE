using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boom : MonoBehaviour {
    private float lpoints;
    private bool alive;
    private Animator ani;
    float secondsCounter = 0;
    float secondsToCount = 0.3f;
    public Slider barraVida;

    private void Awake()
    {
        lpoints = 50;
        alive = true;
        barraVida.value = lpoints;
    }
    void Start () {
        ani = GetComponent<Animator>();
    }

  
    void Update()
    {
        barraVida.value = lpoints;
        if (lpoints <= 0)
        {
            alive = false;
        }
       
        if (alive == false)
        {
            ani.SetBool("alive", alive);
            secondsCounter += Time.deltaTime;
            if (secondsCounter >= secondsToCount)
            {
                Destroy(gameObject);
            }
        }
    }


   void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Metal")
        {
            lpoints -= 10;
        }
        if (c.gameObject.tag == "Espina")
        {
            lpoints -= 10;
        }
        if (c.gameObject.tag == "lava")
        {
            lpoints -= 30;
        }
    }
    
}
