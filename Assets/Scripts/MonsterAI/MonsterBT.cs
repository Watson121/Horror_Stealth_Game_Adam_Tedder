using System.Collections.Generic;
using BehaviourTree;

/// <summary>
/// This is the behaviour tree for the Monster
/// </summary>

public class MonsterBT : Tree
{
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    // Waypoints
    public UnityEngine.Transform[] waypoints;
    public static float speed = 3f;
    public static float chaseSpeed = 5f;
    public static float detectionRange = 10f;

    // Setting up the behaviour tree for this monster
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
