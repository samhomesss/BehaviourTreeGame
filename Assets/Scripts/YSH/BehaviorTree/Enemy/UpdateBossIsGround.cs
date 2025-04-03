using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateBossIsGround", story: "Update [Self] is [_IsGround]", category: "Action", id: "5826be695ed409a8778554daf3183c96")]
public partial class UpdateBossIsGround : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> IsGround;
    const float IsgroundDis = 0.6f;
    LayerMask GroundLayer = 1 << 6; 

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        IsGround.Value = isGrounded();
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
        
    }

    /// <summary>
    /// 땅 판정 
    /// </summary>
    /// <returns></returns>
    bool isGrounded()
    {
        //if (Physics2D.Raycast(Self.Value.transform.position, Vector2.down, IsgroundDis, GroundLayer))
        //{
        //    return true;
        //}
        return Physics2D.Raycast(Self.Value.transform.position, Vector2.down, IsgroundDis, GroundLayer);
    }
}

