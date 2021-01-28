using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _visibleDistance;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private bool _isLooking = true;
    [SerializeField] private GameObject _box;
    //[SerializeField] private GameObject _rigsNormal;
    //[SerializeField] private GameObject _rigsRagdoll;
    [SerializeField] private GameObject _bodyBone;
    [SerializeField] private Collider _bigCapsule;
    [SerializeField] private Collider _smallCapsule;
    [SerializeField] private Collider _cube;

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

        ActivateRagdoll(false);
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
                //_rigsNormal.SetActive(false);
                //_rigsRagdoll.SetActive(true);
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
            
            ForceVector.x = (transform.position.x - other.transform.position.x) * 20f;
            ForceVector.z = 60f;
            
            ActivateRagdoll(true);

            //EnemyModelRigidbody.AddForce(ForceVector, ForceMode.Impulse);
            EnemyModelRigidbody.AddForce(Vector3.left * 50, ForceMode.Impulse);
            Destroy(gameObject, 5.0f);

        }
    }

    private void ActivateRagdoll(bool state)
    {
        //GetComponent<Rigidbody>().isKinematic = state;
        //if (state == true)
        //{
        //    Destroy(GetComponent<Rigidbody>());
        //    //_bigCapsule.enabled = false;
        //    //_smallCapsule.enabled = false;
        //    _cube.enabled = false;
        //}
        var bones = GetComponentsInChildren<Rigidbody>();
        foreach(var bone in bones)
        {
            bone.isKinematic = !state;
        }
        _animator.enabled = !state;
    }
}
