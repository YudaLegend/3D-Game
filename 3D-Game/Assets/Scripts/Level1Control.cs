using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        level = 0;
        counter = 0;
        opended = false;
        closed = true;
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1){
            counter = 0;
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "Enemy"){
                    //Debug.Log(child.tag);
                    counter++;
                }  
            }
            if(counter <= 0) state = 2;
        }else if(state == 2){

            if(counter <= 0 && !opended){
                OpenDoor(level);
                opended = true;
                closed = false;
            }
            Debug.Log(gameObject.transform.Find("Character").transform.position.y);
            if(gameObject.transform.Find("Character").transform.position.y >= 4.5f + (level-1)*3.5f && !closed){
                CloseDoor(level);
                closed = true;
                opended = false;
                state = 3;
            }
        }else if(state == 3){
            Instanciate(level);
            state = 1;
        }
    }
    void OpenDoor(int l){
        level++;
        gameObject.transform.Find("Tower").Find("Plataform " + level).Find("JumpPlataform").GetComponent<Plataform>().open();
        Debug.Log(level);
    }
    void CloseDoor(int l){
        gameObject.transform.Find("Tower").Find("Plataform " + level).Find("JumpPlataform").GetComponent<Plataform>().close();
        Debug.Log(level);
    }

    void Instanciate(int l){
        Instantiate(enemy2, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
