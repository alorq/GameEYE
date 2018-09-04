using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlJugador : MonoBehaviour {
    public float velocidadMovimiento;
    public float fuerzaSalto;
    public bool terreno;
    public AudioSource Fx;
    public AudioClip fire;
    public AudioClip death;
    public AudioClip walk;


    [SerializeField] Transform canionR;
    [SerializeField] Transform canionL;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float velocidadBala;
    [SerializeField] int totalAmountOfBullets;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();

    private Rigidbody2D rbd;
    private Animator ani;
    private int p;
    public Slider barraMunicion;

    public int vidaMax = 250;
    public int vidaActual;
    public bool vivo;
    public Slider barraVida;
    float secondsCounter = 0;
    float secondsToCount = 2.5f;

    private double count = 100;
    private bool disp;
    private bool salt;
    private bool mov;
    private bool rel;

    void Awake()
    {
        vidaActual = vidaMax;
        barraVida.value = vidaActual;
        p = 10;
        barraMunicion.value = 10;
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
        vivo = true;
        mov = false;
    }

    void Update(){
        ani.SetFloat("velocidad", Mathf.Abs(rbd.velocity.x));
        ani.SetFloat("gravedad", Mathf.Abs(rbd.velocity.y));
        ani.SetBool("terreno", terreno);
        ani.SetBool("vivo", vivo);
        if (Input.GetKeyDown(KeyCode.X))
        {
            disp = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            salt = true;
        }
        else
        {
            salt = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            rel = true;
        }
        barraVida.value = vidaActual;
        barraMunicion.value = p-1;
    }

    void FixedUpdate() {
        Debug.Log(secondsCounter);
        if (vidaActual <= 0)
        {
            secondsCounter += Time.deltaTime;
            vivo = false;
            scriptPausa.playerIsDed = true;
            DeathSound();
            rbd.velocity = new Vector2(0, 0);
            if (secondsCounter >= secondsToCount - 1f)
            {
                gameObject.SetActive(false);
            }
        }
        SpriteRenderer ren = GetComponent<SpriteRenderer>();
        Debug.Log(p);
        if (rel)
        {
            Recargar();
        }
        if (disp==true && p>1 && rel==false)
        {
            GameObject bullet = bullets[1];
            bullets.RemoveAt(1);
            FireSound();
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
        float h = Input.GetAxis("Horizontal");
        if (vivo)
        {
            rbd.velocity = new Vector2(velocidadMovimiento * h, rbd.velocity.y);
            if (h == 0)
            {
                rbd.velocity = new Vector2(0f, rbd.velocity.y);
            }
            if (salt == true && (rbd.velocity.y == 0 || mov==true)) { 
                rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                salt = false;
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
        if (col.gameObject.tag == "Movil")
        {
            mov = false;
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
            vidaActual -= 50;
        }
        if (col.gameObject.tag == "Espina")
        {
            vidaActual -= 60;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "final")
        {
            SceneManager.LoadScene(2);
        }

        if (col.gameObject.tag == "ending")
        {
            scriptPausa.endGame = true;
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
            mov = true;
        }
        if (col.gameObject.tag == "lava")
        {
            vidaActual -= 15;
        }
        if (col.gameObject.tag == "Bicho")
        {
            vidaActual -= 3;
        }
    }

    void Recargar()
    {
       
        secondsCounter += Time.deltaTime;
       
        if (secondsCounter >= secondsToCount)
        {
            secondsCounter = 0;
            int i;
            if (p < 10f)
            {
                for (i=p-1; i < 9; i++)
                {
                    
                    GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.isKinematic = true;
                    bullets.Add(bullet);
                }
                p = 10;
               
            }
            rel = false;
            disp = false;
        }
    }

    public void FireSound()
    {
        Fx.PlayOneShot(fire);
    }

    public void DeathSound()
    {
        Fx.PlayOneShot(death);
    }

    public void WalkSound()
    {
        Fx.PlayOneShot(walk);
    }
}