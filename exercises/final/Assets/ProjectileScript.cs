using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject explosion;
    private AudioSource audioSource;
	private AudioClip[] soundClips;
    // Start is called before the first frame update
    void Start()
    {
        soundClips = Resources.LoadAll<AudioClip>("Sounds");
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Random.Range(12.0f, 25.0f) * Time.deltaTime;   
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("enemy")) { // If the projectile hits, 'kill' the other unit
            playAudioClip();
            Vector3 position = other.gameObject.transform.position;
            GameObject boom = Instantiate(explosion, position, other.gameObject.transform.rotation);
            Vector3 dir = transform.forward;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(dir.normalized * 80);

            Destroy(other.gameObject, 3);
            Destroy(boom, 3);
        
        }
        Destroy(gameObject);
    }
	private void playAudioClip()
	{
		audioSource.clip = soundClips[Random.Range(0,soundClips.Length)];
		audioSource.Play();
	}
}
