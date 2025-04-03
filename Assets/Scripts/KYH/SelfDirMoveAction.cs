using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Self Dir Move", story: "[Self] [Dir] Move", category: "Action", id: "1f91bbb2424ce11831ee22ef0f1d8201")]
public partial class SelfDirMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<int> Dir;
    [SerializeReference] public BlackboardVariable<Rigidbody2D> rb;
    protected override Status OnStart()
    {
        if(Dir.Value == -1) Self.Value.GetComponent<SpriteRenderer>().flipX = true;
        else Self.Value.GetComponent<SpriteRenderer>().flipX = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        Move();
        return Status.Running;
        
    }

    protected override void OnEnd()
    {
    }
    
    public void Move()
    {
        Self.Value.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Dir*5f, Self.Value.GetComponent<Rigidbody2D>().linearVelocity.y);
       // rb.Value.linearVelocity = new Vector2(-5f, rb.Value.linearVelocity.y);
        
    }
}

