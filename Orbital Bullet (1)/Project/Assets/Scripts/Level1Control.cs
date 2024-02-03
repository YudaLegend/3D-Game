using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1Control : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    int level;
    int state;
    int counter;
    bool opended;
    bool closed;

    public AudioSource win;
    public AudioSource bgm;

    public AudioClip BossFight;
    void Start()
    {
        level = 1;
        counter = 0;
        opended = false;
        closed = true;
        state = 1;
        //setDeActive();
        GameObject gb = gameObject.transform.Find("Tower").Find("Plataform1").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform2").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform3").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform4").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform5").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform6").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform7").Find("BigJump").gameObject;
        gb.SetActive(false);
        gb = gameObject.transform.Find("Tower").Find("Plataform8").Find("BigJump").gameObject;
        gb.SetActive(false);
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0.0f && bgm.isPlaying){
            bgm.Pause();
        }else if(Time.timeScale == 1.0f && !bgm.isPlaying){
            bgm.Play();
        }
        if(state == 1){
            if(level != 9){
                counter = 0;
                if(Input.GetKey(KeyCode.F)){
                    killAll();
                }
                foreach (Transform child in gameObject.transform.Find("Tower").Find("Plataform" + level).Find("Enemies").gameObject.transform)
                {
                    if (child.tag == "Enemy"){
                        //Debug.Log(child.tag);
                        counter++;
                    }  
                }
                if(counter <= 0) 
                {
                    gameObject.transform.Find("Tower").Find("Plataform" + level).Find("BigJump").gameObject.SetActive(true);
                    state = 2;
                }
            }else{
                if(bgm.clip != BossFight){
                    bgm.clip = BossFight;
                    bgm.Play();
                }

            }

        }else if(state == 2){

            if(!opended){
                OpenDoor(level);
                opended = true;
                closed = false;
            }
            //Debug.Log(gameObject.transform.Find("Character").transform.position.y);
            if(gameObject.transform.Find("Character").transform.position.y >= 4.5f + (level-2)*3.5f && !closed){
                CloseDoor(level);
                closed = true;
                opended = false;
                state = 3;
            }
        }else if(state == 3){
            //Instanciate(level);
            //if (level == 9) SceneManager.LoadScene(6, LoadSceneMode.Single);
            
            state = 1;
        }

        if (level == 9) {
            GameObject boss = GameObject.Find("ghast");
            BossController bossScript = boss.GetComponent<BossController>();
            bossScript.unlock();
            if (bossScript.health == 0) {
                SceneManager.LoadScene(6, LoadSceneMode.Single);
                win.Play();
            }

        }
        
    }

    void OpenDoor(int l){
        level++;
        gameObject.transform.Find("Tower").Find("Plataform" + level).Find("Plataform Outern").Find("JumpPlataform").GetComponent<Plataform>().open();
        Debug.Log(level);
    }
    void CloseDoor(int l){
        gameObject.transform.Find("Tower").Find("Plataform" + level).Find("Plataform Outern").Find("JumpPlataform").GetComponent<Plataform>().close();
        Debug.Log(level);
    }

    void Instanciate(int l){
        Instantiate(enemy2, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void setDeActive(){
        
        for(int i = 1; i < 10; i++){
            Debug.Log(i);
            GameObject gb = gameObject.transform.Find("Tower").Find("Plataform" + level).Find("BigJump").gameObject;
            gb.SetActive(false);
        }
    }

    void killAll(){
        foreach (Transform child in gameObject.transform.Find("Tower").Find("Plataform" + level).Find("Enemies").gameObject.transform)
        {
            try
            {
                // your code segment which might throw an exception
                child.gameObject.GetComponent<Enemy1>().die();
            }
            catch (Exception ex)
            {
                //Debug.LogException(ex, this);
            }
            try
            {
                // your code segment which might throw an exception
                child.gameObject.GetComponent<Enemy2>().die();
            }
            catch (Exception ex)
            {
                //Debug.LogException(ex, this);
            }
            try
            {
                // your code segment which might throw an exception
                child.gameObject.GetComponent<Enemy3>().die();
            }
            catch (Exception ex)
            {
                //Debug.LogException(ex, this);
            }
        }
    }
}
