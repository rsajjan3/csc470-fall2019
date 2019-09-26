using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject pin2Obj;
    float treeSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        int pinNum = Random.Range(2, 7); //Target a random pin
        pin2Obj = GameObject.Find("Pin" + pinNum.ToString());
        treeSpeed = Random.Range(2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pin2Obj.transform, Vector3.up);
        transform.position += transform.forward * treeSpeed * Time.deltaTime;
    }
}
