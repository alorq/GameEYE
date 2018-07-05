using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour{
    [SerializeField] public float velocidadcaida;
    [SerializeField] public float velocidadmovi;
    [SerializeField] public float fuerzasalto;
    [SerializeField] public bool terrenofirme;
    [SerializeField] public bool colgado;
    [SerializeField] public float max;
    private Rigidbody2D rbd;
    private Animator ani;
    private bool disparadorsalto;
    private int vida = 250;
    private int vidaActual;
    public bool vivo = true;
    private double count = 100;

    void Start(){
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        vidaActual = vida;
    }

    void Update(){
        ani.SetFloat("Velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("Caida", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("Terrenofirme", terrenofirme);
        ani.SetBool("Colgado", colgado);
        ani.SetBool("Vivo", vivo);
        if (Input.GetKeyDown(KeyCode.UpArrow) && terrenofirme){
            disparadorsalto = true;
        }
    }

    void FixedUpdate() {

        float h = Input.GetAxis("Horizontal");
        if (vivo)
        {
            rbd.velocity = new Vector2(velocidadmovi*h, rbd.velocity.y);
            float limite = Mathf.Clamp(rbd.velocity.x, -max, max);
            rbd.velocity = new Vector2(limite, rbd.velocity.y);
            if (h == 0)
            {
                rbd.velocity = new Vector2(0f, rbd.velocity.y);
            }
            if (disparadorsalto)
            {
                rbd.AddForce(Vector2.up * fuerzasalto, ForceMode2D.Impulse);
                disparadorsalto = false;
            }
        }
        if (h < 0)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }
        if (h > 0)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        if (vidaActual <= 0){
            vivo = false;
            count -= 1.2;  
        }
       
        if (count < 0){
            SceneManager.LoadScene("Menu");
        }
    }

    void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme = false;
        }
      
        if (col.gameObject.tag == "Movil")
        {
            terrenofirme = false;
            transform.parent = null;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme = true;
        }
        if (col.gameObject.tag == "Movil")
        {
            terrenofirme = true;
            transform.parent = col.transform;
        }
        if (col.gameObject.tag == "Finish"){
            SceneManager.LoadScene("Menu");
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        float p = Input.GetAxis("Horizontal");
        if (col.gameObject.tag == "Terreno")
        {
            terrenofirme = true;
        }
        if (col.gameObject.tag == "Movil")
        {
            terrenofirme = true;
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Espina" && vidaActual>0){
            vidaActual -= 2;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Bala"){
            vidaActual -= 45;
        }
    }

}