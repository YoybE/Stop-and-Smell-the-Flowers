using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;
    public EnemyState shouldBeNextState = EnemyState.Default;

    public override void Start()
    {
        base.Start();
        GameRestart(); // clear powerup in the beginning, go to start state
    }

    // this should be added to the GameRestart EventListener as callback
    public void GameRestart()
    {
        currentPowerupType = PowerupType.Default;
        TransitionToState(startState);
    }

    public void SetPowerup(PowerupType i)
    {
        currentPowerupType = i;
    }
}
