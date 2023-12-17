using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    public float speed = 10;
    public Spear spear;
    float shot = 0;

    void Start()
    {
        //spear.speed = speed;
        shot = Time.realtimeSinceStartup;
    }
    void Update()
    {
        if (Time.realtimeSinceStartup - shot > speed)
        {
            shot = Time.realtimeSinceStartup;
            Instantiate(spear, transform.position, Quaternion.identity);
        }
    }
}