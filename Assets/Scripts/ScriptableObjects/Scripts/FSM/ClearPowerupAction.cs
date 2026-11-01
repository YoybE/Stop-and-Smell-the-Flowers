using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearPowerup")]
public class ClearPowerupAction : Action
{
    public override void Act(StateController controller)
    {
        EnemyStateController m = (EnemyStateController)controller;
        m.currentPowerupType = PowerupType.Default;
    }
}