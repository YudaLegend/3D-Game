using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject FireballPrefab;
    public Transform bulletSpawnPoint;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;


    public int health;

    public static int numberEnemy;

    private float myTime = 0.0f;

    private float nextSpell = 5;

    private float TimeEnemy = 0.0f;
    private float SpellEnemy = 5;
    private GameObject newFireball;
    private GameObject enemy;

    private int actualEnemy = 0;

    private GameObject[] enemies; 

    private static Vector3[] fireballSpawn = new Vector3[] { new Vector3(3f, 3, 0), new Vector3(-3f, 3, 0) , new Vector3(2.5f, 3, 1f), new Vector3(0, 3, -3f), new Vector3(-1.3f, 3, -3f),
    new Vector3(0, 3, 3f),new Vector3(-2f, 3, 2.4f)};


    private static Vector3[] enemySpawn = new Vector3[] { new Vector3(2.65f, -0.2f, 0), new Vector3(-3f, -0.2f, 0)};

    private int childEnemy;

    public bool locked = true;

    // Start is called before the first frame update
    void Start()
    {
        numberEnemy = 5;
        health = 10;
        childEnemy = this.transform.childCount;

        enemies = new GameObject[numberEnemy];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            //newFireball = Instantiate(FireballPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
            myTime += Time.deltaTime;
            TimeEnemy += Time.deltaTime;
            if (myTime >= nextSpell)
            {
                Vector3 pos = fireballSpawn[Random.Range(0, fireballSpawn.Length)];
                myTime = 0.0F;
                newFireball = Instantiate(FireballPrefab, this.transform.position + pos, Quaternion.identity, this.transform) as GameObject;

            }

            if (actualEnemy < numberEnemy && TimeEnemy >= SpellEnemy)
            {
                TimeEnemy = 0.0F;

                int aux = Random.Range(0, 3);
                if (aux == 0)
                {
                    enemy = enemy1;
                }
                else if (aux == 1)
                {
                    enemy = enemy2;
                }
                else
                {
                    enemy = enemy3;
                }

                GameObject fat = GameObject.Find("Plataform9");

                Vector3 pos = enemySpawn[Random.Range(0, enemySpawn.Length)];


                enemy = Instantiate(enemy, this.transform.position + pos, Quaternion.identity, this.transform) as GameObject;

                enemies[actualEnemy] = enemy;

                actualEnemy = actualEnemy + 1;
            }

            for (int i = 0; i < actualEnemy; i++)
            {
                
                if (enemies[i] == null)
                {
                    enemies[i] = new GameObject();
                    health = health - 2;
                }

            }
        }

    }

    public void unlock() {
        this.locked = false;
    }
}
