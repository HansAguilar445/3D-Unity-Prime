using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform ball;
    float speed = 110f;
    void Start()
    {

    }

    void Update()
    {

        transform.position = ball.position;
    }
}
