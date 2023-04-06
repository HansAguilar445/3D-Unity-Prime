using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Samus")
        {
            Destroy(gameObject);
        }
    }
}
