using UnityEngine;

public class PlayerAnimationEventController : MonoBehaviour
{
    PolygonCollider2D[] _colliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _colliders = FindObjectsByType<PolygonCollider2D>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAttack1Start()
    {
        _colliders[1].enabled = true;
    }

    public void GetAttack1End()
    {
        _colliders[1].enabled = false;
    }

    public void GetAttack2Start()
    {
        _colliders[2].enabled = true;
    }

    public void GetAttack2End()
    {
        _colliders[2].enabled = false;
    }

    public void GetAttack3Start()
    {
        _colliders[3].enabled = true;
    }

    public void GetAttack3End()
    {
        _colliders[3].enabled = false;
    }
}
