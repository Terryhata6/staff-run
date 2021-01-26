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
        if (other.gameObject.CompareTag("Player"))
        {
            _stickModel.IncreaseLenghtOfStick();
            Destroy(gameObject);
        }
        
    }
}
