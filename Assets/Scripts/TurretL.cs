using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretL : MonoBehaviour
{
    [SerializeField] private float outshot;
    [SerializeField] private Transform canon;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float range;
    [SerializeField] private float velocidadbala;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int totalAmountOfBullets;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
    private Transform player;
    private float timeshot;

    private void Awake()
    {
        for (int i = 0; i < totalAmountOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            bullets.Add(bullet);
        }
    }

    void Start()
    {
        timeshot = outshot;
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if ((player.position.x - transform.position.x) < -0.2f && distance < range)
        {
            Vector2 dir = (player.position - transform.position);
            Vector2 nor = dir.normalized;
            transform.right = -dir;
            Rigidbody2D rb;
            if (timeshot <= 0)
            {
                if (bullets.Count <= 0)
                {
                    CreateNewBullet();
                }
                GameObject bullet = bullets[0];
                bullets.RemoveAt(0);
                Debug.Log(bullet.GetInstanceID());
                bullet.SetActive(false);
                bullet.transform.position = canon.position;
                bullet.transform.rotation = canon.rotation;
                bullet.SetActive(true);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                rb.velocity = nor * velocidadbala;
                Debug.Log(nor);
                timeshot = outshot;
            }
            else
            {
                timeshot -= Time.deltaTime;
            }
        }
    }

    void CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        bullets.Add(bullet);
    }

    IEnumerator ReUseBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        bullet.SetActive(false);
        bullet.transform.position = new Vector3(-1000, -1000, -1000);
        bullet.SetActive(true);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        bullets.Add(bullet);
    }
}