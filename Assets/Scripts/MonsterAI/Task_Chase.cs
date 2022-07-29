using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class Task_Chase : Node
{

    private Transform _transform;
    private NavMeshAgent navAgent;
    private PlayerController playerController;

    public Task_Chase(Transform transform, NavMeshAgent agent)
    {
        this._transform = transform;
        this.navAgent = agent;
        this.navAgent.speed = MonsterBT.chaseSpeed;
    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");
        playerController = target.GetComponent<PlayerController>();

        if (target != null && playerController.Hidden != true)
        {
            if (Vector3.Distance(navAgent.nextPosition, target.position) > 1.5f)
            {
                navAgent.SetDestination(target.position);
            }

            state = NodeState.RUNNING;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
        
    }

}

