using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] StickModel _staff;

    public void StaffAttackStarted()
    {
        Debug.Log("Атака прошла из аниматора");
        _staff.StaffAttackStart();
    }

    public void StaffAttackEnded()
    {
        Debug.Log("Атака закончилась из аниматора");
        _staff.StaffAttackEnd();
    }

    


}
