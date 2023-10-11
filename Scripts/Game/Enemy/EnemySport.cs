using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySport : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform targetPos;
    private Animator anim;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        targetPos = this.gameObject.transform.parent.parent.GetChild(1);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        agent.destination = targetPos.position;
        Debug.Log(targetPos.position);
        if (agent.isStopped)
        {
            anim.SetBool("WtoG",true);
        }
    }
}
