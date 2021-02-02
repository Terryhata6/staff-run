using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] StickModel _staff;
    [SerializeField] PlayerMovement _player;

    public void StaffAttackStarted()
    {
        
        _staff.StaffAttackStart();
    }

    public void StaffAttackEnded()
    {
        _staff.StaffAttackEnd();
    }

    private void OnFinalMovementStart()
    {
        _player.SetAnimatorApplyMotion(true);
    }

    private void OnFinalMovement()
    {
        //Механика финального уровня - бросок посоха
        StaffAttackStarted();
        _staff.FinalMove();
    }
}
