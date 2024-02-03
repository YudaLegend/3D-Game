using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float rotationSpeed, jumpSpeed, gravity;
    public GameObject particle;
    Vector3 startDirection;
    public bool dir;
    CharacterController charControl;
    Vector3 direction, position;
    float timer;
    public AudioSource hit;
    // Start is called before the first frame update
    void Start()
    {
        startDirection = transform.position - transform.parent.position;
        startDirection.y = 0.0f;
        startDirection.Normalize();
        GameObject player = GameObject.Find("Character");
        MovePlayer playerScript = player.GetComponent<MovePlayer>();

        dir = playerScript.dir;

        position = transform.position;
        direction = position - transform.parent.position;
        timer = 2.0f;
        particle = Resources.Load("Hit") as GameObject;
        hit = GetComponent<AudioSource>();
        hit.clip = Resources.Load<AudioClip>("DM-CGS-48");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Gun Entered collision with " + other.gameObject.tag);
        if(other.gameObject.tag == "Enemy") {
            hit.Play();
            if (particle!= null) {
                Instantiate(particle, transform.position, Quaternion.identity);
            } else {
                Debug.LogError("Failed to load 'Hit' prefab from Resources folder.");
            }
            Enemy1 e1 = other.gameObject.GetComponent<Enemy1>();
            if (e1 != null)
            {
                e1.BeDamaged();
                Destroy(gameObject);
            }else{
                Enemy2 e2 = other.gameObject.GetComponent<Enemy2>();
                if (e2 != null)
                {
                    e2.BeDamaged();
                    Destroy(gameObject);
                }else{
                    Enemy3 e3 = other.gameObject.GetComponent<Enemy3>();
                    e3.BeDamaged();
                    Destroy(gameObject);
                }
            }
        }
        if(other.gameObject.tag != "bullet") {
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(timer < 0) Destroy(gameObject);
        else{
            timer-=Time.deltaTime;
        }
        CharacterController charControl = GetComponent<CharacterController>();

        Moving();
        Vector3 currentDirection = transform.position - transform.parent.position;
        currentDirection.y = 0.0f;
        currentDirection.Normalize();
        // Change orientation of player accordingly
        Quaternion orientation;
        if ((startDirection - currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(0.0f, Vector3.up);
        else if ((startDirection + currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(180.0f, Vector3.up);
        else
            orientation = Quaternion.FromToRotation(startDirection, currentDirection);
        transform.rotation = orientation;

    }

    void Moving()
    {
        CharacterController charControl = GetComponent<CharacterController>();
        //Debug.Log(charControl);

        Vector3 position;
        float angle;
        Vector3 direction, target;
        rotationSpeed = 100;
        position = transform.position;
        angle = rotationSpeed * Time.deltaTime;
        direction = position - transform.parent.position;


        if (!dir)
        {
            target = transform.parent.position + Quaternion.AngleAxis(angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            
        }
        if (dir)
        {
            target = transform.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            
        }
    }
}
