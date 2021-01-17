using UnityEngine;

public class TriggerSwitchState : MonoBehaviour
{
    [SerializeField] CharacterState _targetState;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("тригернул : " + other.name);
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().ChangePlayerState(_targetState);
        }
    }
}
