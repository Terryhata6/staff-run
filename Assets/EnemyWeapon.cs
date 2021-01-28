using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyWeapon : MonoBehaviour
{
    private MeshCollider _collider;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<MeshCollider>();
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
    }

    // Update is called once per frame
    

    public void OnEnemyDeath()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _collider.enabled = true;
        _collider.convex = true;
       
    }
}
