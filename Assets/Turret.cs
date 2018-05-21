﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private float timeshot;
    public float outshot;
    public GameObject bullet;
    private Transform player;
    public Transform partToRotate;
    public float turnSpeed;

    // Use this for initialization
    void Start()
    {
        timeshot = outshot;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //establecimiento de los tiempos de disparo, esto para determinar cuan seguido la torreta creara y lanzara disparos

        Vector3 dir = (player.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Debug.Log(lookRotation);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

        if (timeshot <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeshot = outshot;
        }
        else
        {
            timeshot -= Time.deltaTime;
        }
	}
}
