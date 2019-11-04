using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Random.Range(12.0f, 25.0f) * Time.deltaTime;   
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("unit") || other.gameObject.CompareTag("dog")) { // If the projectile hits, 'kill' the other unit
            Vector3 dir = other.contacts[0].point - transform.position;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(dir.normalized * 80);

            Destroy(other.gameObject, 3);
        }
    }
}
