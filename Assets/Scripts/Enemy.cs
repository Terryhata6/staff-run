using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _visibleDistance;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private GameObject _box;
    //[SerializeField] private GameObject _rigsNormal;
    //[SerializeField] private GameObject _rigsRagdoll;
    [SerializeField] private GameObject _bodyBone;
    [SerializeField] private Collider _bigCapsule;
    [SerializeField] private Collider _smallCapsule;
    [SerializeField] private Collider _cube;
    [SerializeField] private GameObject[] _enemyesSkinVariations;
    [SerializeField] private GameObject[] _enemyesMaskVariations;
    [SerializeField] private GameObject[] _enemyesWeaponVariations;
    [SerializeField] private bool _useSkinGeneration;
    [SerializeField] private bool _useWeaponGeneration;
    [SerializeField] private bool _useMaskGeneration;
    [SerializeField] private float _addForcePower;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _targetDistance = 30;
    [SerializeField] private GameObject _capsuleInAttackRange;

    private Transform _player;
    private float _distance;
    private Vector3 ForceVector;
    private PlayerMovement _playerState;
    private CapsuleCollider _collider;
    private bool _finalState = false;
    private bool _isDead;
    private bool _isActive;

    private int _skinIndex;
    private int _maskIndex;
    private int _weaponIndex;
    private EnemyWeapon _weaponObject;
    private float _destroyTime = 12.0f;
    private float _smoothValue = 0.1f;
    private Vector3 _smoothLookingVector;
    private Vector3 _movingDownVector;
    private float _movingDownSpeed = 0.005f;
    public Rigidbody _enemyModelRigidbody;

    

    void Start()
    {
        _isActive = false;
        _isDead = false;
        _smoothLookingVector = new Vector3(1, 0, 1);
        _player = GameObject.FindGameObjectWithTag(NameManager.Player).transform;
        _playerState = _player.GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();        
        _collider = GetComponent<CapsuleCollider>();
        _enemyModelRigidbody = GetComponent<Rigidbody>();
        ActivateRagdoll(false);
        ChooseMySkin();
        _animator.enabled = false;
        CheckDistance();
    }

    void Update()
    {
        if (_isActive)
        {
            if (!_isDead)
            {   
                _smoothLookingVector = _player.position;
                _smoothLookingVector.y = transform.position.y;
                transform.LookAt(_smoothLookingVector);
                _headTransform.LookAt(_player);                
            }            
            if (_isDead)
            {
                _movingDownVector = transform.position;
                _movingDownVector.y -= _movingDownSpeed;
                transform.position = _movingDownVector;
            }
        }
        else if (!_isActive)
        {
            CheckDistance();
        }
        if (!_finalState)
        {
            if (_playerState.GetState() == CharacterState.Final ||
                _playerState.GetState() == CharacterState.Hurricane)
            {
                _finalState = true;
                _bigCapsule.enabled = false;
                if (!_animator.enabled && !_isDead)
                {
                    _animator.enabled = true;
                }
            }
        }
    }

    private void CheckDistance()
    {
        _distance = (transform.position.z > _player.position.z) ? (transform.position.z - _player.position.z) : (_player.position.z - transform.position.z);
        if (_distance < _targetDistance)
        {
            _isActive = true;
            _animator.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Staff"))
        {
            Debug.LogWarning("Атака прошла");
            OnDeath(other);

        }
    }

    private void OnDeath(Collider other)
    {
        _animator.enabled = false;
        _bodyBone.GetComponent<Rigidbody>().AddForce(Vector3.left * _addForcePower * 1000, ForceMode.Impulse);
        ForceVector.x = (transform.position.x - other.transform.position.x) * 20f;
        ForceVector.z = 60f;
        ActivateRagdoll(true);

        _weaponObject.OnEnemyDeath();        
        //_enemyModelRigidbody.AddForce(Vector3.left * _addForcePower*1000, ForceMode.Impulse);
        _bodyBone.GetComponent<Rigidbody>().AddForce(Vector3.left * _addForcePower * 1000, ForceMode.Impulse);
        Invoke("AfterDeath", 2.0f);
    }

    private void AfterDeath() 
    {
        _enemyModelRigidbody.isKinematic = false;
        _enemyModelRigidbody.useGravity = false;
        _isDead = true;
        _collider.enabled = false;
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var coll in colliders)
        {
            coll.enabled = false;
        }
        //ActivateRagdoll(false);
        Destroy(gameObject, _destroyTime);
    }

    private void ChooseMySkin()
    {
        if (_enemyesSkinVariations[0] != null && _useSkinGeneration)
        {

            _skinIndex = Random.Range(0, _enemyesSkinVariations.Length);
            foreach (GameObject skin in _enemyesSkinVariations)
            {
                skin.SetActive(false);
            }
            _enemyesSkinVariations[_skinIndex].SetActive(true);
        }

        if (_enemyesMaskVariations[0] != null && _useMaskGeneration)
        {
            _maskIndex = Random.Range(0, _enemyesMaskVariations.Length);
            foreach (GameObject mask in _enemyesMaskVariations)
            {
                mask.SetActive(false);
            }
            _enemyesMaskVariations[_maskIndex].SetActive(true);
        }

        if (_enemyesWeaponVariations[0] != null && _useWeaponGeneration)
        {
            _weaponIndex = Random.Range(0, _enemyesWeaponVariations.Length);
            foreach (GameObject weapon in _enemyesWeaponVariations)
            {
                weapon.SetActive(false);
            }
            _enemyesWeaponVariations[_weaponIndex].SetActive(true);
        }

        _weaponObject = _enemyesWeaponVariations[_weaponIndex].GetComponent<EnemyWeapon>();
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

    public bool IsDead()
    {
        return _isDead;
    }
}
