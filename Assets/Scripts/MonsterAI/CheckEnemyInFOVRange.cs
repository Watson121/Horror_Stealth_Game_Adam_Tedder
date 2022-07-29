using BehaviourTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{

    private static LayerMask _playerLayerMask = LayerMask.GetMask("Player");
    private Transform _transform;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
      

        object t = GetData("target");
        Collider[] colliders;

        if (t == null)
        {
            colliders = Physics.OverlapSphere(_transform.position, MonsterBT.detectionRange, _playerLayerMask.value);

            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                Transform target = (Transform)GetData("target");
                PlayerController player = target.GetComponent<PlayerController>();

                if(player.Hidden == true)
                {
                    state = NodeState.SUCCESS;
                }
                else if (player.Hidden == false)
                {
                    state = NodeState.FAILURE;
                }

                return state;
            }
        

            state = NodeState.FAILURE;
            return state;
        }
        else if(t != null)
        {
            Transform target = (Transform)GetData("target");

            if(Vector3.Distance(_transform.position, target.position) > MonsterBT.detectionRange)
            {
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }


}
