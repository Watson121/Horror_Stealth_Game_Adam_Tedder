using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class Task_Patrol : Node
{

    private Transform _transform;
    private Transform[] _waypoints;

    private int currentWayPointIndex = 0;
    private float speed = 2f;

    private float waitTime = 1.0f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Task_Patrol(Transform transform, Transform[] waypoints)
    {
        this._transform = transform;
        this._waypoints = waypoints;
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
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWayPointIndex = (currentWayPointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
