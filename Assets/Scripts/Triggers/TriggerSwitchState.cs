using UnityEngine;

public class TriggerSwitchState : MonoBehaviour
{
    [SerializeField] CharacterState _targetState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().ChangePalyerState(_targetState);
        }
    }
}
