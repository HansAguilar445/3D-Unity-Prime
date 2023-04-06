using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetection : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            player.GetComponent<Grapple>().hooked = true;
            player.GetComponent<Grapple>().hookedObj = other.gameObject;
        }
        else
        {
            player.GetComponent<Grapple>().hookedObj = null;
        }
    }
}
