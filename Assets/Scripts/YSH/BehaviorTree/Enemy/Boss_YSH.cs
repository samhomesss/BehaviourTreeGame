using UnityEngine;
using Unity.Behavior;

public class Boss_YSH : MonoBehaviour
{
    BehaviorGraphAgent _behaviorAgent;
    GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<Player_YSH>().gameObject;
        _behaviorAgent = GetComponent<BehaviorGraphAgent>();
        _behaviorAgent.SetVariableValue("Target", _player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
