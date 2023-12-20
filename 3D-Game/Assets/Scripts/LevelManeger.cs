using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManeger : MonoBehaviour
{
    public void play(){
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void play1(){
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void creadit(){
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void howtoplay(){
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void quit(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void die(){
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    public void win(){
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }
}
