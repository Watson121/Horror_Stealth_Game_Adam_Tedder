using System.Collections.Generic;
using UnityEngine.AI;
using BehaviourTree;

public class MonsterBT : Tree
{
    public NavMeshAgent navMeshAgent;
    
    public UnityEngine.Transform[] waypoints;
    public static float speed = 3f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new Task_Chase(transform, navMeshAgent),
            }),
            new Task_Patrol(transform, waypoints, navMeshAgent),

        });

        return root;
    }
}
