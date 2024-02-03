using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera ca;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Quaternion.LookRotation(transform.position-ca.transform.position);
    }
}
