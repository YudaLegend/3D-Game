using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float rotationSpeed, jumpSpeed, gravity;

    Vector3 startDirection;
    public bool dir;
    CharacterController charControl;
    Vector3 direction, position;
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
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collision with " + other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(charControl);

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
