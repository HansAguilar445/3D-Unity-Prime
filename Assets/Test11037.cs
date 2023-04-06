using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test11037 : MonoBehaviour
{
    public float health = 180;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Zerocharge(Clone)")
        { 
            health -= 15; 
        }
        else if (coll.gameObject.name == "Semicharge(Clone)")
        {
            health -= 45;
        }
        else if (coll.gameObject.name == "Fullcharge(Clone)" || coll.gameObject.name == "Missile(Clone)")
        {
            health -= 90;
        }
    }
}
