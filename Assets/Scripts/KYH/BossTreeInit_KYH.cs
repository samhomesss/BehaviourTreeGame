using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;

public class BossTreeInit_KYH : MonoBehaviour
{
    public GameObject target;
    private Animator animator;
    private BehaviorGraphAgent behaviorAgent;
    private Rigidbody2D rb;
    //private currentState cs;
    
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        rb = GetComponent<Rigidbody2D>();
        Setup();
    }

    private void Setup()
    {
        
        behaviorAgent.SetVariableValue("rb", rb);
        //behaviorAgent.GetVariable("currentState", out BlackboardVariable<currentState> cs); 
        //this.cs = cs.Value;
        //cs.Value = currentState.Idle;
        
        behaviorAgent.SetVariableValue("anim", animator);
        behaviorAgent.SetVariableValue("Target", target);
    }
}
