using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject hook, hookHolder, hookedObj;
    public float travelSpeed, playerTravelSpeed, maxDistance;
    public static bool fired;
    public bool hooked;
    private float currentDistance;
    public Transform linkpoint;
    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && fired == false || Input.GetButtonDown("L") && fired == false)
        {
            fired = true;
        }
        if (fired)
        {
            hook.GetComponent<MeshRenderer>().enabled = true;
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
      
        }
        if (fired == true && hooked != true)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }
        if (hooked == true && fired == true)
        {
            hook.transform.SetParent(hookedObj.transform, true);
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 1)
            {
                if (grounded != true)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 7);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 10);
                    
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.SetParent(hookHolder.transform, true);
            
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.0000000000000000000000000000000000001f);
        ReturnHook();

        
    }

    void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = linkpoint.position;
        hookedObj = null;
        fired = false;
        hooked = false;
        hook.GetComponent<MeshRenderer>().enabled = false;
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);

        
    }
    
    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;

            hookedObj = null;
        }
        else 
        {
            grounded = false;

           
        }
    }
}
