using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    // Start is called before the first frame update
    public int state;// 0 idel 1 openning 2 closing
    Vector3 startDirection;
    bool opencheck;
    bool closecheck;
    float radi;
    float openradi;
    float timer;
    Vector3 iniPos;
    float angle;
    void Start()
    {   
        radi = Mathf.Sqrt(transform.position.x*transform.position.x + transform.position.z*transform.position.z);
        // Store starting direction of the player with respect to the axis of the level
        startDirection = transform.position - transform.parent.position;
        startDirection.y = 0.0f;
        startDirection.Normalize();
        state = 0;
        opencheck = false;
        closecheck = false;
        openradi = -20*radi;
        iniPos = transform.position;
        angle = Mathf.Atan2(transform.position.x, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer > 0){
            timer -= Time.deltaTime;
        }else{
            timer = 0;
            state = 0;
        }
        if(Input.GetKey(KeyCode.O) && !opencheck){
            open();
            opencheck = true;
            timer = 1;
            /*Moving();
            Vector3 t = Vector3.zero;
            t.y = transform.position.y;
            transform.position = t;*/
        }
        else if(Input.GetKeyUp(KeyCode.O) && opencheck){
            opencheck = false;
        }
        if(Input.GetKey(KeyCode.P) && !closecheck){
            timer = 1;
            close();
            closecheck = true;
        }else if(Input.GetKeyUp(KeyCode.O) && closecheck){
            closecheck = false;
        }



        if(state == 1){
            openning();
        }else if(state == 2){
            closing();
        }
    }

    public void open()
    {
        state = 1;
    }

    public void close()
    {
        state = 2;
    }

    void openning()
    {
        
        Vector3 pos = iniPos;
        pos.x = (float)openradi*Mathf.Sin(angle)*(1-timer);
        pos.z = (float)openradi*Mathf.Cos(angle)*(1-timer);
        transform.position = pos;
    }

    void closing()
    {
        Vector3 pos = iniPos;
        float phrase = timer*(-20)+1;
        if(phrase > 1) phrase = 1; 
        pos.x = (float)radi*Mathf.Sin(angle)*(phrase);
        pos.z = (float)radi*Mathf.Cos(angle)*(phrase);
        
        transform.position = pos;
    }

    void Moving(){
        Vector2 xz;
        xz.x = transform.position.x;
        xz.y = transform.position.z;
        Debug.Log(transform.position);
        Debug.Log(transform.parent.position);

        /*xz = xz * 2;
        Vector3 final = transform.position;
        final.x += xz.x;
        final.z += xz.y;
        transform.position = final;
        */
    }
}
