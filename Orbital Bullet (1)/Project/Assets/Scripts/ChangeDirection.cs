using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Obstacle"){
            Debug.Log("Entered");
            this.transform.parent.GetComponent<Enemy3>().dir = !this.transform.parent.GetComponent<Enemy3>().dir;
        }
    }
}
