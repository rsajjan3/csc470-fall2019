using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {   
        transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("unit")) // If the projectile hits, 'kill' the other units
        {
            ParticleSystem explosive = Instantiate(explosion, transform.position, transform.rotation);
            Vector3 dir = other.contacts[0].point - transform.position;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(dir.normalized * 60.0f);

            Destroy(other.gameObject, 3);
            Destroy(explosive, 2);
        }
    }
}
