using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Enemy3 : MonoBehaviour
{
    public float rotationSpeed, jumpSpeed, gravity;
    public float health;
    public float shield;
    public float maxHealth;
    public float maxSheild;
    
    public GameObject healthBarUI;
    public Slider s;
    public Slider h;
    Vector3 startDirection;
    // Start is called before the first frame update
    public bool dir;
    void Start()
    {
        // Store starting direction of the player with respect to the axis of the level
        startDirection = transform.position - transform.parent.parent.parent.position;
        startDirection.y = 0.0f;
        startDirection.Normalize();
        dir = true;
        health = maxHealth;
        shield = maxSheild;
        s.value = shield/maxSheild;
        h.value = health/maxHealth;
        if(SceneManager.GetActiveScene().name == "Level1") rotationSpeed = 10;
        else rotationSpeed = 35;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        s.value = shield/maxSheild;
        h.value = health/maxHealth;
        if(health <= 0){
            Destroy(gameObject);
        }
        if(health > maxHealth){
            health = maxHealth;
        }
        CharacterController charControl = GetComponent<CharacterController>();
        Moving();
        // Correct orientation of player
        // Compute current direction
        Vector3 currentDirection = transform.position - transform.parent.parent.parent.position;
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
        if(!dir){
            transform.rotation *= Quaternion.Euler(0,180f,0);
        }
        transform.rotation *= Quaternion.Euler(0,-45.0f,0);
        //if(Input.GetKey(KeyCode.F)){Destroy(gameObject);}
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Obstacle"){
            dir = !dir;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Entered collision with " + other.gameObject.name);
    }

    void Moving(){
        CharacterController charControl = GetComponent<CharacterController>();
        Vector3 position;
        float angle;
        Vector3 direction, target;
        //rotationSpeed = 10;
        position = transform.position;
        angle = rotationSpeed * Time.deltaTime;
        direction = position - transform.parent.parent.parent.position;
        if (!dir)
        {
            target = transform.parent.parent.parent.position + Quaternion.AngleAxis(angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
        }
        if (dir)
        {
            target = transform.parent.parent.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
        }
    }
    public void die(){
        Destroy(gameObject);
    }

    public void BeDamaged(){
        shield = shield - 50;
        if(shield < 0)
        {
            health = health + shield;
            shield = 0;
        } 
    }

}
