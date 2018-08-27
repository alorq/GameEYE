using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour{
    [SerializeField] public float velocidadmovi;
    [SerializeField] public float fuerzasalto;
    [SerializeField] public bool terreno;
    [SerializeField] public float fuerzaimpulso;
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
        ani.SetFloat("velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("gravedad", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("terreno", terreno);
        ani.SetBool("Vivo", vivo);
       
    }

    void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.UpArrow) && rbd.velocity.y == 0)
        {
            disparadorsalto = true;
        }
        float h = Input.GetAxis("Horizontal");
        if (vivo)
        {
            rbd.velocity = new Vector2(velocidadmovi * h, rbd.velocity.y);
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
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (h > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (vidaActual <= 0) {
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
            terreno = false;
        }
      
        if (col.gameObject.tag == "Movil")
        {
            terreno = false;
            transform.parent = null;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Terreno")
        {
            terreno = true;
        }
        if (col.gameObject.tag == "Movil")
        {
            terreno = true;
            transform.parent = col.transform;
        }
        if (col.gameObject.tag == "Finish"){
            SceneManager.LoadScene("Menu");
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag == "Terreno")
        {
            terreno = true;
        }
        if (col.gameObject.tag == "Muro")
        {
            terreno = true;
        }
        if (col.gameObject.tag == "Movil")
        {
            terreno = true;
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