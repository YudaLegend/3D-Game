using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particle;
    void Start()
    {
        particle = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        particle.Play();
        Destroy(gameObject, 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
