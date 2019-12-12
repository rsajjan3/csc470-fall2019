using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public GameObject beerCan;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("attack", 2, 5);
    }
	void attack()
	{
        Vector3 position = transform.position + transform.forward;
        GameObject can = Instantiate(beerCan, position, transform.rotation);
        Destroy(can, 3);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) { // If the projectile hits, 'kill' the other unit
            Destroy(other.gameObject);
            StartCoroutine("WaitForEnd");
        }
    }
    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EndGame");
    }
}
