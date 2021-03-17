using UnityEngine;

public class TriggerSwitchState : MonoBehaviour
{
    [SerializeField] CharacterState _targetState;
    [SerializeField] GameObject _cyllinderStartPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().ChangePlayerState(_targetState);
            if (_targetState == CharacterState.PrepareToBalance)
            { 
                other.GetComponent<PlayerMovement>().NextCyllinder = _cyllinderStartPosition;                
            }
        }
    }
}
