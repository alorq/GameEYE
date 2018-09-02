using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlJugador : MonoBehaviour {
    public float velocidadMovimiento;
    public float fuerzaSalto;
    public bool terreno;
    public bool salto;

    [SerializeField] Transform canionR;
    [SerializeField] Transform canionL;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float velocidadBala;
    [SerializeField] int totalAmountOfBullets;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();

    private Rigidbody2D rbd;
    private Animator ani;
    private int p;

    public int vidaMax = 250;
    public int vidaActual;
    public bool vivo = true;
    public Slider barraVida;
    float secondsCounter = 0;
    float secondsToCount = 5;

    private double count = 100;
    private bool disp;

    void Awake()
    {
        vidaActual = vidaMax;
        barraVida.value = vidaActual;
    }

    void Start(){
        rbd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        for (int i = 0; i < totalAmountOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            bullets.Add(bullet);
        }
        p = 10;
    }

    void Update(){
        ani.SetFloat("velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("gravedad", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("terreno", terreno);
        ani.SetBool("vivo", vivo);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            disp = true;
        }
        barraVida.value = vidaActual;
    }

    void FixedUpdate() {
        SpriteRenderer ren = GetComponent<SpriteRenderer>();
        Debug.Log(p);
        if (p<2)
        {
            Recargar();
        }
        if (disp==true && p>1)
        {
            GameObject bullet = bullets[1];
            bullets.RemoveAt(1);
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
            if (ren.flipX == false)
            {
                rb.velocity = new Vector2(velocidadBala, Random.Range(-0.2f, 0.2f));
            }
            if (ren.flipX == true)
            {
                rb.velocity = new Vector2(-velocidadBala, Random.Range(-0.2f, 0.2f));
            }
            p--;
            disp = false;
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
            if ((vidaActual + 100) > vidaMax)
            {
                vidaActual = vidaMax;
            }
            else
            {
                vidaActual += 100;
            }
        }

        if (col.gameObject.tag == "Bala")
        {
            vidaActual -= 20;
        }

        if (col.gameObject.tag == "Espina")
        {
            vidaActual -= 50;
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

    void Recargar()
    {
        Debug.Log(rbd.velocity);
        secondsCounter += Time.deltaTime;
        if (secondsCounter >= secondsToCount)
        {
            secondsCounter = 0;
            for (int i = 0; i < 9; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                bullets.Add(bullet);
            }
            p = 10;
        }
        disp = false;
    }
}