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
        Debug.Log("Hello");

        object t = GetData("target");
        if(t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, 6f, _playerLayerMask.value);

            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }


}
