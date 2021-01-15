using UnityEngine;

public class EnemyFlyAway : MonoBehaviour
{
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ForceVector.x = (transform.position.x - other.transform.position.x) * 1000f;
            ForceVector.z = 1000f;

            Debug.Log("AddForce = " + ForceVector);
            EnemyModelRigidbody.AddForce(ForceVector);
            Debug.Log("Hit = other.transform.position : " + other.transform.position + " + transform.position :" + transform.position);
        }
    }
}
