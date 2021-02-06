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
    [SerializeField] private GameObject[] _enemyesSkinVariations;
    [SerializeField] private GameObject[] _enemyesMaskVariations;
    [SerializeField] private GameObject[] _enemyesWeaponVariations;
    [SerializeField] private float _addForcePower;
    [SerializeField] private Animator _animator;

    private Transform _player;
    private float _distance;
    public Rigidbody _enemyModelRigidbody;
    private Vector3 ForceVector;
    private PlayerMovement _playerState;
    private CapsuleCollider _collider;
    private bool _finalState = false;
    private bool _isDead;

    private int _skinIndex;
    private int _maskIndex;
    private int _weaponIndex;
    private EnemyWeapon _weaponObject;
    private float _destroyTime = 12.0f;
    private float _smoothValue = 0.1f;
    private Vector3 _smoothLookingVector;
    private Vector3 _movingDownVector;
    private float _movingDownSpeed = 0.005f;

    void Start()
    {
        _isDead = false;
        _smoothLookingVector = new Vector3(1, 0, 1);
        _player = GameObject.FindGameObjectWithTag(NameManager.Player).transform;
        _playerState = _player.GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        _enemyModelRigidbody = GetComponent<Rigidbody>();
        ActivateRagdoll(false);
        ChooseMySkin();
    }

    void Update()
    {
        if (_isLooking)
        {
            _distance = Vector3.Distance(_player.position, transform.position);
            _smoothLookingVector = _player.position;
            _smoothLookingVector.y = transform.position.y;
            transform.LookAt(_smoothLookingVector);
            _headTransform.LookAt(_player);
            /*
            if ((_distance <= _visibleDistance) &&( _distance > 2.0f ) )
            {
                vector = (_player.position-transform.position)/_distance;
                transform.rotation.SetLookRotation(vector);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion(vector), _smoothValue);
                //Quaternion.Lerp(transform.rotation, Qu, _smoothValue)
                //transform.LookAt(_player); 
            }
            */
        }
        if (!_finalState) 
        {
            if (_playerState.GetState() == CharacterState.Final)
            {
                _isLooking = false;
                _finalState = true;
                //_collider.isTrigger = false;
                //_box.SetActive(false);
                //_rigsNormal.SetActive(false);
                //_rigsRagdoll.SetActive(true);
                //_animator.enabled = false;
            }
        }
        if (_isDead)
        {
            _movingDownVector = transform.position;
            _movingDownVector.y -= _movingDownSpeed;
            transform.position = _movingDownVector;
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
        if (_enemyesSkinVariations[0] != null)
        {

            _skinIndex = Random.Range(0, _enemyesSkinVariations.Length);
            foreach (GameObject skin in _enemyesSkinVariations)
            {
                skin.SetActive(false);
            }
            _enemyesSkinVariations[_skinIndex].SetActive(true);
        }

        if (_enemyesMaskVariations[0] != null)
        {
            _maskIndex = Random.Range(0, _enemyesMaskVariations.Length);
            foreach (GameObject mask in _enemyesMaskVariations)
            {
                mask.SetActive(false);
            }
            _enemyesMaskVariations[_maskIndex].SetActive(true);
        }

        if (_enemyesWeaponVariations[0] != null)
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
}
