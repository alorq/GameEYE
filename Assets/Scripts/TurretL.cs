using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretL: MonoBehaviour {
    [SerializeField] public float outshot;
    [SerializeField] public GameObject bulletd;
    [SerializeField] public Transform partToRotate;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float range;
    [SerializeField]int totalAmountOfBullets = 30;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
    private Transform player;
    private float timeshot;

    void Start(){
        timeshot = outshot;
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
    }

    private void Awake()
    {
        for (int i = 0; i < totalAmountOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletd, new Vector3(-1000, -1000, -1000), Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            bullets.Add(bullet);
        }
    }

    void FixedUpdate(){
        //establecimiento de los tiempos de disparo, esto para determinar cuan seguido la torreta creara y lanzara disparos
        float distance = Vector3.Distance(player.position, transform.position);
        if ((player.position.x - transform.position.x) > 0.7f && distance < range){
            Vector3 dir = (player.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Debug.Log(lookRotation);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

            if (timeshot <= 0){
                GameObject bullet = bullets[0];
                bullets.RemoveAt(0);
                Debug.Log(bullet.GetInstanceID());
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(90, 0, 0);
                timeshot = outshot;
                StartCoroutine(ReUseBullet(bullet));
            }
            else{
                timeshot -= Time.deltaTime;
            }
        }
    }
    void CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletd, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        bullets.Add(bullet);
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
