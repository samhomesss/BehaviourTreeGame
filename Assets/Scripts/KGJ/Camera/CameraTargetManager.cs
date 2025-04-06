using Unity.Cinemachine;
using UnityEngine;

public class CameraTargetManager
{
    CinemachineTargetGroup _targetGroup;

    public void Init()
    {
        _targetGroup = GameObject.FindAnyObjectByType<CinemachineTargetGroup>();
    }

    public void AddTarget(Transform newTarget, float radius, float weight)
    {
        foreach (var target in _targetGroup.Targets)
        {
            // ������ �� : ���� ������Ʈ �̸����� �˻���
            if (target.Object.name == newTarget.gameObject.name)
            {
                return;
            }
                
        }
        _targetGroup.AddMember(newTarget, weight, radius);
    }

    public void RemoveTarget(Transform target)
    {
        _targetGroup.RemoveMember(target);
    }
}
