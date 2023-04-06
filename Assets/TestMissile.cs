using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMissile : MonoBehaviour
{
    int ticks = 0;
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        ticks++;
        if (ticks % 300 == 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 80;
    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
