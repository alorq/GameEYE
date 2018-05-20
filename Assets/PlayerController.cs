using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedf;
    public float speedm;
    public float jumpp;
    public bool grounded;
    public bool walled;

    private Rigidbody2D rbd;
    private Animator ani;
    private bool jump;

    // Use this for initialization
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
        ani.SetFloat("Speed", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("Fall", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("Grounded", grounded);
        ani.SetBool("Walled", walled);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        float p = Input.GetAxis("Horizontal");
 
        if (p > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rbd.velocity = new Vector2(speedm, rbd.velocity.y);
        }
        else if (p < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rbd.velocity = new Vector2(-speedm, rbd.velocity.y);
        }
        else
        {
            rbd.velocity = new Vector2(0, rbd.velocity.y);
        }
        if (jump)
        {
            rbd.AddForce(Vector2.up * jumpp, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            walled = false;
        }
        if (col.gameObject.tag == "Wall")
        {
            grounded = true;
            walled = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (col.gameObject.tag == "Wall")
        {
            grounded = false;
            walled = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            rbd.AddForce(Vector2.down * speedf, ForceMode2D.Impulse);
        }
    }
}