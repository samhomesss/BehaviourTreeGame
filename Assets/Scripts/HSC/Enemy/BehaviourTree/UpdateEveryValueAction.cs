using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateEveryValue", story: "[self] [DistanceFromTarget] [Player] [Direction] [gravity] [BossState]", category: "Action", id: "d3de5e7e4942ae24fbe958e676d44a64")]
public partial class UpdateEveryValueAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> DistanceFromTarget;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<Vector2> Direction;
    [SerializeReference] public BlackboardVariable<float> Gravity;
    [SerializeReference] public BlackboardVariable<BossState> State;
    private Rigidbody2D _rb;
    //private float _maxFallSpeed;
    private float _jumpHangTime = 0;
    private float _jumpHangGravityMultiplier = 1;
    private float _fallGravityMultiplier = 3;

    protected override Status OnStart()
    {
        _rb = Self.Value.GetComponent<Rigidbody2D>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        DistanceFromTarget.Value = Vector2.Distance(Self.Value.transform.position, Player.Value.transform.position);
        float dir = Player.Value.transform.position.x - Self.Value.transform.position.x;
        if (dir < 0)
        {
            Direction.Value = Vector2.left;
        }
        else
        {
            Direction.Value = Vector2.right;
        }

        if(State.Value == BossState.Jump)
        {
            if (_rb.linearVelocity.y < 0)
            {
                _rb.gravityScale = Gravity.Value * _fallGravityMultiplier;
            }
            else
            {
                _rb.gravityScale = Gravity.Value;
            }
        }

        return Status.Running;
    }
}

