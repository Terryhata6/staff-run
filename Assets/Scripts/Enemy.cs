using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _visibleDistance;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private bool _isLooking = true;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _rigsNormal;
    [SerializeField] private GameObject _rigsRagdoll;

    private Transform _player;
    private float _distance;
    private Animator _animator;
    public Rigidbody EnemyModelRigidbody;
    private Vector3 ForceVector;
    private PlayerMovement _playerState;
    private CapsuleCollider _collider;
    private bool _finalState = false;

    


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(NameManager.Player).transform;
        _playerState = _player.GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
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
        if (!_finalState) 
        {
            if (_playerState.GetState() == CharacterState.Final)
            {
                _finalState = true;
                //_collider.isTrigger = false;
                //_box.SetActive(false);
                _rigsNormal.SetActive(false);
                _rigsRagdoll.SetActive(true);
                //_animator.enabled = false;
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
