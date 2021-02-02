using UnityEngine;

public class StackingCollectables : MonoBehaviour
{
    private StickModel _stickModel;

    private void Start()
    {
        _stickModel = FindObjectOfType<StickModel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameManager.Player))
        {
            _stickModel.GivePower();
            _stickModel.ChangeLenghtOfStick();            
            Destroy(gameObject);
        }
    }
}
