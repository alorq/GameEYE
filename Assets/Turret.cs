using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    [SerializeField] public float outshot;
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform partToRotate;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float range;
    private Transform player;
    private float timeshot;

    void Start(){
        timeshot = outshot;
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
    }
	
	void FixedUpdate(){
        //establecimiento de los tiempos de disparo, esto para determinar cuan seguido la torreta creara y lanzara disparos
        float distance = Vector3.Distance(player.position, transform.position);
        if ((player.position.x - transform.position.x) < 0.5f && distance < range){
            Vector3 dir = (player.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Debug.Log(lookRotation);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

            if (timeshot <= 0){
                Instantiate(bullet, transform.position, Quaternion.identity);
                timeshot = outshot;
            }
            else{
                timeshot -= Time.deltaTime;
            }
        }
    }   
}
