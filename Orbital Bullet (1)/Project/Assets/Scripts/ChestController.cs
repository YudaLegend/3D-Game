using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject player; // referencia al objeto del jugador
    public float activationDistance = 5.0f; // distancia a la que se activar� la animaci�n

    public AudioSource chestOpening;
    private Animator animator;

    public GameObject proyectil;
    public GameObject pistol;

    private bool once = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool isPlayerNear = distanceToPlayer <= activationDistance;
        
        animator.SetBool("PlayerNearly", isPlayerNear);

        if (isPlayerNear && (!once)) {
            once = true;
            
            ProyectilController pscript = proyectil.GetComponent<ProyectilController>();
            PistolController pistolScript = pistol.GetComponent<PistolController>();
            pscript.collectChest();
            pistolScript.collectChest();

            chestOpening.PlayDelayed(0.5f);
            
            //Debug.Log("aaaaaaaaaaaaaaa");
            //Destroy(gameObject.transform.parent.gameObject,2.2f);
        }



    }
}
