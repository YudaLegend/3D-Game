using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePlayer : MonoBehaviour
{
    public float rotationSpeed, jumpSpeed, gravity;
    Vector3 startDirection;
    public float speedY;
    bool cicle; // false -> outern 
    bool changeButton;
    public bool inmortal;
    bool changeInmortal;
    // Start is called before the first frame update
    public bool dir;
    public float dashTimer;
    bool changeDash;
    public int vida;
    public float dashEnergy;
    public bool bigJump;
    void Start()
    {
        // Store starting direction of the player with respect to the axis of the level
        startDirection = transform.position - transform.parent.position;
        startDirection.y = 0.0f;
        startDirection.Normalize();
        dir = false;
        speedY = 0;
        cicle = false;
        changeButton = false;
        inmortal = false;
        changeInmortal = false;
        dashTimer = 0;
        changeDash = false;
        vida = 5;
        dashEnergy = 0.0f;
        bigJump = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterController charControl = GetComponent<CharacterController>();
        Vector3 position;
        // Left-right movement
        if (dashTimer >= 0){
            inmortal = true;
            Dashing();
        }
        else if (Input.GetKey(KeyCode.E) && !changeDash){
            dashTimer = 0.1f;
            changeDash = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && changeDash){
            changeDash = false;
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && dashTimer < 0)
        {
            inmortal = false;
            Moving();
        }

        // Correct orientation of player
        // Compute current direction
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
        
        if(!dir)transform.rotation = orientation;
        else{
            transform.rotation = orientation;
            transform.rotation *= Quaternion.Euler(0,180f,0);
        }

        // Apply up-down movement
        position = transform.position;
        if (charControl.Move(speedY * Time.deltaTime * Vector3.up) != CollisionFlags.None)
        {
            transform.position = position;
            Physics.SyncTransforms();
        }
        if (charControl.isGrounded)
        {
            if(bigJump && jumpSpeed != 10){
                jumpSpeed = 10;
            }else if(!bigJump && jumpSpeed != 5){
                jumpSpeed = 5;
            }
            if (speedY < 0.0f)
                speedY = 0.0f;
            if (Input.GetKey(KeyCode.W))
                speedY = jumpSpeed;
        }
        else
            speedY -= gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q) && !changeButton){
            changeButton = true;
            cicle = !cicle;
            float angle = Mathf.Atan2(transform.position.x, transform.position.z);
            if(cicle){
                //intern
                Vector3 pos = transform.position;
                pos.x = (float)1.84*Mathf.Sin(angle);
                pos.z = (float)1.84*Mathf.Cos(angle);
                pos.y = (float)(pos.y + 0.38);
                transform.position = pos;
            }else {
                //outern
                Vector3 pos = transform.position;
                pos.x = (float)2.91*Mathf.Sin(angle);
                pos.z = (float)2.91*Mathf.Cos(angle);
                pos.y = (float)(pos.y - 0.37);
                transform.position = pos;
            }
        }else if(Input.GetKeyUp(KeyCode.Q) && changeButton){
            changeButton = false;
        }

        if (Input.GetKey(KeyCode.R) && !changeInmortal){
            changeInmortal = true;
        }else if(Input.GetKeyUp(KeyCode.R) && changeInmortal){
            changeInmortal = false;
            inmortal = !inmortal;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered collision with " + collision.gameObject.name);

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Jumper")
            bigJump = true;
        if (other.gameObject.tag == "Fireball")
            vida = vida - 1;
        if (other.gameObject.tag == "trap")
            vida = vida - 1;

    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Jumper")
            bigJump = false;
    }

    void Moving(){
        CharacterController charControl = GetComponent<CharacterController>();
        Vector3 position;
        float angle;
        Vector3 direction, target;
        rotationSpeed = 100;
        position = transform.position;
        angle = rotationSpeed * Time.deltaTime;
        direction = position - transform.parent.position;
        if (Input.GetKey(KeyCode.A))
        {
            target = transform.parent.position + Quaternion.AngleAxis(angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            dir = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            target = transform.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            dir = true;
        }
    }
    
    void Dashing(){
        dashTimer -= Time.deltaTime;
        CharacterController charControl = GetComponent<CharacterController>();
        Vector3 position;
        float angle;
        Vector3 direction, target;
        rotationSpeed = 500;
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
            dir = false;
        }
        else if (dir)
        {
            target = transform.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            dir = true;
        }

    }
}


