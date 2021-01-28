using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _visibleDistance;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private bool _isLooking = true;
    private Transform _player;
    private float _distance;
    private Animator _animator;
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(NameManager.Player).transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isLooking)
        {
            _distance = Vector3.Distance(_player.position, transform.position);

            if ((_distance <= _visibleDistance) &&( _distance > 2.0f ) )
            {

                transform.LookAt(_player);
                //_headTransform.LookAt(_player);

            }
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
              
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.LogWarning("Атака прошла");
            _animator.enabled = false;
            
            ForceVector.x = (transform.position.x - other.transform.position.x) * 200f;
            ForceVector.z = 600f;
            

            EnemyModelRigidbody.AddForce(ForceVector);
            Destroy(gameObject, 5.0f);
        }
    }
}
