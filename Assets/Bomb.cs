using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    // Grenade explodes after a time delay.
    public float fuseTime = 0.5f, radius = 1, power = 400;

    void Start()
    {
        Invoke("Explode", fuseTime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, 2);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 1.0F);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        
    }
}
