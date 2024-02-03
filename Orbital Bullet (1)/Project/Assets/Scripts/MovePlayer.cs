using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePlayer : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particle; 
    public float rotationSpeed, jumpSpeed, gravity;
    public GameObject UI;
    public GameObject LevelManeger;
    Vector3 startDirection;
    public float speedY;
    bool cicle; // false -> outern 
    public bool changeButton;
    public bool inmortal;
    public float inmortalTimer;

    public bool godMode;
    bool changeInmortal;

    bool changeLifeMore;
    bool changeLifeLess;
    // Start is called before the first frame update
    public bool dir;
    public float dashTimer;
    bool changeDash;
    public int vida;
    public float dashEnergy;
    public bool bigJump;
    public bool enter;

    public GameObject foot;

    public AudioSource jump;
    public AudioSource dash;
    public AudioSource damaged;
    public AudioSource teletransport;
    public AudioSource BigJump;
    public AudioSource steep;

    public bool FirstGrounded;
    bool godChange;
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
        inmortalTimer = 0.0f;
        dashTimer = 0;
        changeDash = false;
        vida = 3;
        dashEnergy = 0.0f;
        bigJump = false;
        changeLifeMore = false;
        changeLifeLess = false;
        godMode = false;
        enter = false;
        animator = gameObject.transform.Find("Character@Idle").GetComponent<Animator>();
        FirstGrounded = true;
        godChange = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterController charControl = GetComponent<CharacterController>();
        Vector3 position;
        if(vida <= 0){
            //Destroy(gameObject);
            LevelManeger.GetComponent<LevelManeger>().die();
        }
        // Left-right movement
        if (dashTimer >= 0){
            inmortal = true;
            Dashing();
        }
        else if (Input.GetKey(KeyCode.E) && !changeDash){
            animator.SetBool("Moving", false);
            dashTimer = 0.1f;
            changeDash = true;
            dash.Play();
        }
        else if (Input.GetKeyUp(KeyCode.E) && changeDash){
            changeDash = false;
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && dashTimer < 0)
        {
            animator.SetBool("Moving", true);
            Moving();
        }else if (dashTimer < 0 && inmortal){
            inmortal = false;
            
        }else{
            animator.SetBool("Moving", false);
        }

        if(inmortalTimer > 0.0f){
            inmortalTimer -= Time.deltaTime;
            if(!inmortal) inmortal = true;
        }else if (inmortal){
            inmortal = false;
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
            if(!FirstGrounded) {
                particle.Play();
                FirstGrounded = true;
            }
            if(bigJump && jumpSpeed != 10){
                jumpSpeed = 15;
            }else if(!bigJump && jumpSpeed != 5){
                jumpSpeed = 7.8f;
            }
            if (speedY < 0.0f)
                speedY = 0.0f;
            if (Input.GetKey(KeyCode.W)) {
                speedY = jumpSpeed;
                jump.Play();
                FirstGrounded = false;
            }
            
        }
        else{
            speedY -= gravity * Time.deltaTime;
        }
        
        if(speedY >= -0.5){
            if(foot.activeSelf) foot.SetActive(false);
        }else{
            if(!foot.activeSelf) foot.SetActive(true);
        }

        if(speedY == -0.5f || speedY == 0.0f){
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
        }else if(speedY < 0.0f){
            animator.SetBool("Down", true);
        }else if(speedY > 0.0f){
            animator.SetBool("Up", true);
        }
        
        if (enter){
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
                teletransport.Play();
            }

        }
        if(!Input.GetKey(KeyCode.Q) && changeButton){
                changeButton = false;
        }


        if (Input.GetKey(KeyCode.R) && !changeInmortal){
            changeInmortal = true;
        }else if(Input.GetKeyUp(KeyCode.R) && changeInmortal){
            changeInmortal = false;
            inmortal = !inmortal;
        }

        if (Input.GetKey(KeyCode.I) && !changeLifeMore){
            changeLifeMore = true;
        }else if(Input.GetKeyUp(KeyCode.I) && changeLifeMore){
            changeLifeMore = false;
            VidaPlusOne();
        }


        if (Input.GetKey(KeyCode.U) && !changeLifeLess){
            changeLifeLess = true;
        }else if(Input.GetKeyUp(KeyCode.U) && changeLifeLess){
            changeLifeLess = false;
            VidaMinusOne();
        }

        

        if (Input.GetKey(KeyCode.G)&& !godChange){
            godMode = !godMode;
            godChange = true;
        }else if (!Input.GetKey(KeyCode.G) && godChange){
            godChange = false;
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
        //Debug.Log("Entered collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Jumper")
            bigJump = true;
        else if (other.gameObject.tag == "Fireball" && !godMode && !inmortal){
            VidaMinusOne();
            damaged.Play();
        }
        else if (other.gameObject.tag == "trap" && !godMode && !inmortal){
            VidaMinusOne();
            damaged.Play();
        }
        else if (other.gameObject.tag == "Enemy" && !godMode && !inmortal){
            VidaMinusOne();
            damaged.Play();
        }
        else if (other.gameObject.tag == "Sheild" && !godMode && !inmortal){
            VidaMinusOne();
            damaged.Play();
        }else if (other.gameObject.tag == "Enter"){
            enter = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited collision with " + other.gameObject.name);
        if(other.gameObject.tag == "Jumper")
            bigJump = false;
        else if(other.gameObject.tag == "Enter"){
            enter = false;
        }
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

    void VidaMinusOne(){
        inmortalTimer = 5.0f;
        inmortal = true;
        vida = vida - 1;
        if(vida == 2){
            UI.transform.Find("lives").Find("life3").gameObject.SetActive(false);
        }else if(vida == 1){
            UI.transform.Find("lives").Find("life2").gameObject.SetActive(false);
            
        }
    }

    void VidaPlusOne(){
        vida = vida + 1;
        if(vida > 3) vida = 3;
        if(vida == 2){
            UI.transform.Find("lives").Find("life2").gameObject.SetActive(true);
        }else if(vida == 3){
            UI.transform.Find("lives").Find("life3").gameObject.SetActive(true);
        }
    }

    public void smallJump(){
        speedY = 5;
        steep.Play();
    }
}


