using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
    [SerializeField] private float lpoints;
    [SerializeField] private bool alive;
    private Animator ani;
    float secondsCounter = 0;
    float secondsToCount = 0.3f;

 
    void Start () {
        alive = true;
        ani = GetComponent<Animator>();
    }

  
    void Update()
    {
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
    }
    
}
