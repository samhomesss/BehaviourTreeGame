using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop_KDY", story: "[Self] Stop", category: "Action", id: "c0f84023b0b8ecc15830bbd085a334c7")]
public partial class StopKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        //속도 초기화
        Rigidbody2D rb = Self.Value.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // 속도를 0으로 설정
            rb.angularVelocity = 0f; // 각속도도 0으로 설정
        }
        return Status.Success;
    }
}

