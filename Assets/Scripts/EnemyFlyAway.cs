using UnityEngine;

public class EnemyFlyAway : MonoBehaviour
{
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameManager.Weapon))
        {
            ForceVector.x = (transform.position.x - other.transform.position.x) * 200f;
            ForceVector.z = 1000f;
            EnemyModelRigidbody.AddForce(ForceVector);
        }
    }
}
