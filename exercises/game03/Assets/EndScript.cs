using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(explosion, new Vector3(0,0,0), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
