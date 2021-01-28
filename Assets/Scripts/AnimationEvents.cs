using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] StickModel _staff;

    public void StaffAttackStarted()
    {
        
        _staff.StaffAttackStart();
    }

    public void StaffAttackEnded()
    {
        _staff.StaffAttackEnd();
    }

    private void OnFinalMovement()
    {
        //Механика финального уровня - бросок посоха
        StaffAttackStarted();
        _staff.FinalMove();
    }
}
