using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusScript : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		int unitCount =  GameObject.FindGameObjectsWithTag("unit").Length; 
        if (unitCount == 1)
        {
            StartCoroutine(WaitForEnd());
        }
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);

    }
}
