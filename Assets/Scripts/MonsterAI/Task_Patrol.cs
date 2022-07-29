using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class Task_Patrol : Node
{

    private Transform _transform;
    private Transform[] _waypoints;
    private NavMeshAgent navAgent;

    private int currentWayPointIndex = 0;
    private float speed = 2f;

    private float waitTime = 1.0f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Task_Patrol(Transform transform, Transform[] waypoints, NavMeshAgent agent)
    {
        this._transform = transform;
        this._waypoints = waypoints;
        this.navAgent = agent;
        this.navAgent.speed = MonsterBT.speed;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
            {
                waiting = false;
            }
        }
        else {

            Transform wp = _waypoints[currentWayPointIndex];

            

            if (Vector3.Distance(navAgent.nextPosition, wp.position) < 1.5f)
            {
                waitCounter = 0f;
                waiting = true;

                currentWayPointIndex = (currentWayPointIndex + 1) % _waypoints.Length;
            }
            else
            {
                navAgent.SetDestination(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
