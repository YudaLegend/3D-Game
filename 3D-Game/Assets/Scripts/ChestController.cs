using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject player; // referencia al objeto del jugador
    public float activationDistance = 5.0f; // distancia a la que se activará la animación
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool isPlayerNear = distanceToPlayer <= activationDistance;
        
        animator.SetBool("PlayerNearly", isPlayerNear);


    }
}
