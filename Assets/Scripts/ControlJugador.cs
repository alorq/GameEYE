using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour {
    [SerializeField] public float velocidadMovimiento;
    [SerializeField] public float fuerzaSalto;
    [SerializeField] public bool terreno;
    [SerializeField] private Transform canionR;
    [SerializeField] private Transform canionL;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float velocidadBala;
    [SerializeField] int totalAmountOfBullets;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
    private Rigidbody2D rbd;
    private Animator ani;
    private bool salto;
    private int vidaMax = 250;
    private int vidaActual;
    public bool vivo = true;
    private double count = 100;

    void Start(){
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        vidaActual = vidaMax;
        for (int i = 0; i < totalAmountOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            bullets.Add(bullet);
        }
    }

    void Update(){
        ani.SetFloat("velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("gravedad", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("terreno", terreno);
        ani.SetBool("vivo", vivo);
       
    }

    void FixedUpdate() {
        SpriteRenderer ren = GetComponent<SpriteRenderer>();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullet = bullets[0];
            bullets.RemoveAt(0);
            Debug.Log(bullet.GetInstanceID());
            bullet.SetActive(false);
            if (ren.flipX == false)
            {
                bullet.transform.position = canionR.position;
                bullet.transform.rotation = canionR.rotation;
            }
            if (ren.flipX == true)
            {
                bullet.transform.position = canionL.position;
                bullet.transform.rotation = canionL.rotation;
            }
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            if (ren.flipX == false){
                rb.velocity = new Vector2(velocidadBala, Random.Range(-0.2f, 0.2f));
            }
            if (ren.flipX == true)
            {
                rb.velocity = new Vector2(-velocidadBala, Random.Range(-0.2f, 0.2f));
            }


        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && rbd.velocity.y == 0)
        {
            salto = true;
        }
        float h = Input.GetAxis("Horizontal");
        if (vivo)
        {
            rbd.velocity = new Vector2(velocidadMovimiento * h, rbd.velocity.y);
            if (h == 0)
            {
                rbd.velocity = new Vector2(0f, rbd.velocity.y);
            }
            if (salto)
            {
                rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                salto = false;
            }
        }
        if (h < 0)
        {
            if (ren.flipX == false)
            {
                ren.flipX = true;
            }
        }
        if (h > 0)
        {
            if (ren.flipX == true)
            {
                ren.flipX = false;
            }
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

        if (col.gameObject.tag == "consumibleVida")
        {
            if ((vidaActual + 50) > vidaMax)
            {
                vidaActual = vidaMax;
            }
            else
            {
                vidaActual += 50;
            }
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

    IEnumerator ReUseBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        bullet.transform.position = new Vector3(-1000, -1000, -1000);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        bullets.Add(bullet);
    }

}