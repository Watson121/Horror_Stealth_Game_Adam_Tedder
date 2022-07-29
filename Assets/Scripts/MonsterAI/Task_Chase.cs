using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class Task_Chase : Node
{

    private Transform _transform;
    private NavMeshAgent navAgent;


    public Task_Chase(Transform transform, NavMeshAgent agent)
    {
        this._transform = transform;
        this.navAgent = agent;
        this.navAgent.speed = MonsterBT.speed;
    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");

        Debug.Log(Vector3.Distance(navAgent.nextPosition, target.position));
        if (Vector3.Distance(navAgent.nextPosition, target.position) > 1.5f)
        {
            navAgent.SetDestination(target.position);
        }
   

        state = NodeState.RUNNING;
        return state;
    }

}

