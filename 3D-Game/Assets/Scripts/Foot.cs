using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject father;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Gun Entered collision with " + other.gameObject.tag);
        if(other.gameObject.tag == "Enemy") {
            Enemy1 e1 = other.gameObject.GetComponent<Enemy1>();
            if (e1 != null)
            {
                e1.BeDamaged();
            }else{
                Enemy2 e2 = other.gameObject.GetComponent<Enemy2>();
                if (e2 != null)
                {
                    e2.BeDamaged();
                }else{
                    Enemy3 e3 = other.gameObject.GetComponent<Enemy3>();
                    e3.BeDamaged();
                }
            }
            father.transform.GetComponent<MovePlayer>().smallJump();
            father.transform.GetComponent<MovePlayer>().inmortal= true;
            father.transform.GetComponent<MovePlayer>().inmortalTimer= 0.5f;
        }
        
    }
}
