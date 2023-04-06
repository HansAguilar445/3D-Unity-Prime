using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallRoll : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed = 100f;
    public GameObject[] camera;
    public GameObject[] projectile;
    public Transform firePoint, missilePoint, bombPoint, bombDeploy;
    public Text load, health;
    public static float charge = 0;
    public static bool charged = false;
    bool rolling = true, onGround = false;
    bool inMorphBall = true;
    bool trigger1Down = false, trigger2Down = false;
    int chargeshot = 0;
    int missiles = 0;
    int energy = 99;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        Vector3 dir = camera[2].transform.TransformDirection(movement);
        load.text = missiles.ToString();
        health.text = energy.ToString();
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true || Input.GetButtonDown("B") && onGround == true)
        {
            rigidbody.AddForce(new Vector3(0, 400, 0));
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("X"))
        {
            rolling = !rolling;
        }
        if (rolling == true)
        {
            camera[0].SetActive(true);
            camera[1].SetActive(false);
            camera[2].SetActive(true);

            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = true;
            (gameObject.GetComponent(typeof(CapsuleCollider)) as Collider).enabled = false;
            speed = 300f;
            rigidbody.AddForce(dir);
            rigidbody.freezeRotation = false;
            inMorphBall = true;
        }
        else if (rolling != true)
        {
            camera[0].SetActive(false);
            camera[1].SetActive(true);
            camera[2].SetActive(false);
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;
            (gameObject.GetComponent(typeof(CapsuleCollider)) as Collider).enabled = true;
            speed = 10f;
            
            transform.Translate(movement);
            if (inMorphBall == true)
            {
                rigidbody.freezeRotation = true;
                rigidbody.velocity = Vector3.zero;
                inMorphBall = false;
                transform.localRotation = Quaternion.Euler(camera[2].transform.localRotation.y, camera[2].transform.localRotation.y, camera[2].transform.localRotation.z);
            }
        }

        if (Input.GetMouseButton(0) && rolling != true || Input.GetAxis("ZR") != 0 && rolling != true)
        {
            trigger1Down = true;
            
            if (charge < 120)
            {
                charge++;
            }
            if (charge < 60)
            {
                chargeshot = 0;
            }
            else if (charge > 60 && 120 > charge)
            {
                chargeshot = 1;
            }
            else if (charge >= 120)
            {
                chargeshot = 2;
            }
        
        }
        
        else if (Input.GetMouseButtonUp(0) && rolling != true || Input.GetAxis("ZR") == 0 && rolling != true)
        {
            if (trigger1Down == true)
            {
                Instantiate(projectile[chargeshot], firePoint.position, firePoint.rotation);
                charge = 0.05f;
                trigger1Down = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && rolling != true && missiles > 0 || Input.GetAxis("ZL") != 0 && rolling != true && missiles > 0 && trigger2Down == false)
        {
            Instantiate(projectile[3], missilePoint.position, missilePoint.rotation);
            missiles--;
            trigger2Down = true;
        }
        else if (Input.GetAxis("ZL") == 0)
        {
            trigger2Down = false;
        }
        if (Input.GetMouseButtonDown(0) && rolling == true && onGround == true || Input.GetButtonDown("R") && rolling == true && onGround == true)
        {
            transform.position = bombDeploy.position;
            rigidbody.velocity = Vector3.zero;
            Instantiate(projectile[4], bombPoint.position, bombPoint.rotation);
           
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.StartsWith("Missile Launcher"))
        {
            missiles++;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Hookable")
        {
            onGround = true;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            energy--;
            onGround = true;
        }
        
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
}
