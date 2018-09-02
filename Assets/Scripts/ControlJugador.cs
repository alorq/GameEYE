using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour {
    [SerializeField] public float velocidadmovi;
    [SerializeField] public float fuerzasalto;
    [SerializeField] public bool terreno;
    [SerializeField] public float max;
    [SerializeField] private Transform canonr;
    [SerializeField] private Transform canonl;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float velocidadbala;
    [SerializeField] int totalAmountOfBullets;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
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
        ani.SetBool("Vivo", vivo);
       
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
                bullet.transform.position = canonr.position;
                bullet.transform.rotation = canonr.rotation;
            }
            if (ren.flipX == true)
            {
                bullet.transform.position = canonl.position;
                bullet.transform.rotation = canonl.rotation;
            }
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            if (ren.flipX == false){
                rb.velocity = new Vector2(velocidadbala, Random.Range(-0.2f, 0.2f));
            }
            if (ren.flipX == true)
            {
                rb.velocity = new Vector2(-velocidadbala, Random.Range(-0.2f, 0.2f));
            }


        }
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