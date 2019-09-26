using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneScript : MonoBehaviour
{
    public GameObject pinObj;
    public GameObject treeFab;
    int planeSpeed = 0;
    float stopTimer = 5;
    public Text distanceText;
    // Start is called before the first frame update
    void Start()
    {
        planeSpeed = Random.Range(1, 5);

        int pinNum = Random.Range(1, 7); //Target a random pin
        pinObj = GameObject.Find("Pin" + pinNum.ToString());
        
        transform.LookAt(pinObj.transform, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, pinObj.transform.position);
        distanceText.text = "Press Spacebar for Hacks. Distance: " + dist;
        stopTimer -= Time.deltaTime;
        if (stopTimer < 0) {
            transform.position += transform.forward * planeSpeed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            Vector3 position = transform.position + Vector3.up * 1.5f + transform.forward * 3.0f;
            GameObject tree = Instantiate(treeFab, position, transform.rotation);
            Destroy(tree, 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("pin1"))
        {
            stopTimer = 5;
        }
    }
}
