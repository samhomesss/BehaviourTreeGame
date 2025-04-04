using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Run", story: "[Self] move to [Direction] and set [BossState]", category: "Action", id: "d261c80f84b8a717118362ea6e61b45c")]
public partial class RunAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector2> Direction;
    [SerializeReference] public BlackboardVariable<float> DistanceFromTarget;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _speed;

    protected override Status OnStart()
    {
        _speed = 5.0f;
        _rigidbody2D = Self.Value.GetComponent<Rigidbody2D>();
        _animator = Self.Value.GetComponent<Animator>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(DistanceFromTarget.Value < 3f)
        {
            Debug.Log("RunAction End");
            return Status.Failure;
        }

        Vector2 direction = Direction.Value.normalized;

        // filp
        if (direction.x > 0)
            Self.Value.transform.localScale = new Vector3(1, 1, 1);
        else
            Self.Value.transform.localScale = new Vector3(-1, 1, 1);

        _rigidbody2D.MovePosition(_rigidbody2D.position + direction * _speed * Time.fixedDeltaTime);

        return Status.Running;
    }

    protected override void OnEnd()
    {
        // TimeOut으로 강제종료 되어도 OnEnd() 호출됨
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("BossState", BossState.Idle);
    }
}

