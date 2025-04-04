using System;
using Unity.Behavior;

[BlackboardEnum]
public enum BossPatternState
{
    Dash,
	Attack,
	Idle,
	Jump,
	AirDash,
	RangeAttack,
	AirStrike
}
