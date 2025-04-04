using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckGround_KDY", story: "Update [Self] [IsGround]", category: "Action", id: "db65c142c7282303c038144c1012ffff")]
public partial class CheckGroundKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> IsGround;


    private BoxCollider2D _collider;
    [Header("Ray Settings")]
    [SerializeField] private float _bossWidth;
    [SerializeField] private float _bossHeight;
    [SerializeField] private float _rayYOffset;
    protected override Status OnStart()
    {
        _collider = Self.Value.GetComponent<BoxCollider2D>();   

        _bossWidth = _collider.size.x * Self.Value.transform.localScale.x;
        _bossHeight = _collider.size.y * Self.Value.transform.localScale.y;
        _rayYOffset = 0.03f;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        // 좌측 하단에서 가로(right) 방향으로 레이 쏘기
        Ray2D ray = new Ray2D(Self.Value.transform.position - new Vector3(_bossWidth * 0.5f, _bossHeight * 0.5f + _rayYOffset, 0), Vector2.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _bossWidth * 0.5f * 2, LayerMask.GetMask("Ground")); // 레이캐스트
        Debug.DrawRay(ray.origin, ray.direction * _bossWidth * 0.5f * 2, Color.red);   // 디버깅

        //땅에 닿았을 때
        if (hit.collider != null)
        {
            if (IsGround.Value == false)
            {
                IsGround.Value = true;
            }
        }
        else // 공중에 떠있을 때
        {
            IsGround.Value = false;
        }
        
        return Status.Running;
    }
}

