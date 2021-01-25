using UnityEngine;

public class EnemyFlyAway : MonoBehaviour
{
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            ForceVector.x = (transform.position.x - other.transform.position.x) * 20000f;
            ForceVector.z = 1000f;
            EnemyModelRigidbody.AddForce(ForceVector);
        }
    }
}
