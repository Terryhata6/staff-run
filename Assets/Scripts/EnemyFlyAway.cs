using UnityEngine;

public class EnemyFlyAway : MonoBehaviour
{
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ForceVector.x = (transform.position.x - other.transform.position.x) * 2000f;
            ForceVector.z = 1000f;
            EnemyModelRigidbody.AddForce(ForceVector);
        }
    }
}
