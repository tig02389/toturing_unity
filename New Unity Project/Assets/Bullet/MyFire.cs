﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFire : MonoBehaviour
{


    public Color _color = Color.yellow;
    public float _radius = 0.1f;


    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
