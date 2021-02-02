using UnityEngine;

public class StickModel : MonoBehaviour
{
    private const float _scaleMultiplierConst = 0.1f;
    [SerializeField] private CameraController _camera;
    [SerializeField] private Transform _stickSpawner1;
    [SerializeField] private Transform _stickSpawner2;
    [SerializeField] private GameObject _stickPart;
    [SerializeField] private float _stickPower;
    public Vector3 RotatingPositionOfStick;
    public Vector3 RotatingRotationOfStick;
    public float RotatingSpeed;
    public float MaxRotatingSpeed;

    private InputController _inputController;
    private Transform _stickTransform;
    private Vector3 _scaleVector;
    private Vector3 _rotatingVector;
    private Vector3 _startPositionOfStick;
    private Vector3 _startRotationOfStick;
    private Collider _collider;
    private float _rotatingSpeed;
    private bool _isRotating;
    private bool _isRotatingFaster;
    private bool _isFinalStage = false;
    private Rigidbody _rigidbody;
    private bool _wasThrowed;
    private bool _canFlyUp;
    private bool _canGetParts;

    public bool CanFlyUp
    {
        get => _canFlyUp;
    }

    private void Awake()
    {
        _camera = FindObjectOfType<CameraController>();
        _isRotating = false;
        _inputController = FindObjectOfType<InputController>();
        _stickTransform = GetComponent<Transform>();
        _scaleVector = new Vector3();
        _startPositionOfStick = _stickTransform.localPosition;
        _startRotationOfStick = _stickTransform.localEulerAngles;
        _rigidbody = GetComponent<Rigidbody>();
        _rotatingVector = new Vector3(RotatingSpeed, 0, 0);
        _rotatingSpeed = RotatingSpeed;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _wasThrowed = false;
        _stickPower = 2;
        ChangeLenghtOfStick();
        _canGetParts = true;
    }
    public void ChangeLenghtOfStick()
    {
        _scaleVector = _stickTransform.localScale;        
        _scaleVector.y = _stickPower * _scaleMultiplierConst;
        _stickTransform.localScale = _scaleVector;
    }
    public void GivePower()
    {
        _stickPower += 1;
    }
    public void ChangePositionOfStick()
    {
        Debug.Log("ChangePositionOfStick");
        if (!_isRotating)
        {
            _isRotating = true;
            _stickTransform.localPosition = RotatingPositionOfStick;
            _stickTransform.rotation = Quaternion.identity;
            _stickTransform.Rotate(RotatingRotationOfStick);
        }
        else
        {
            _isRotating = false;
            _stickTransform.localPosition = _startPositionOfStick;
            _stickTransform.rotation = Quaternion.identity;
            _stickTransform.Rotate(_startRotationOfStick);
        }
    }
    private void RotateStick()
    {
        _stickTransform.Rotate(_rotatingVector, Space.Self);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeLenghtOfStick();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangePositionOfStick();
        }
        if (_isRotating)
        {
            _canFlyUp = (_stickPower > 4) ? true : false;
            if (_canFlyUp && _inputController.DragingStarted)
            {
                if (_canGetParts)
                {
                    CreateStaffParts();
                    _canGetParts = false;
                    Invoke("CooldownStaffParts", 0.25f);
                    _stickPower -= 1f;
                    ChangeLenghtOfStick();
                }
                _rotatingSpeed = MaxRotatingSpeed;
                _rotatingVector.x = _rotatingSpeed;
                
            }
            else if (!_inputController.DragingStarted && _rotatingSpeed != RotatingSpeed)
            {
                _rotatingSpeed = RotatingSpeed;
                _rotatingVector.x = _rotatingSpeed;
            }
            RotateStick();
        }
    }
    public void CooldownStaffParts()
    {
        _canGetParts = true;
    }
    public void CreateStaffParts()
    {
        GameObject _obj1 = Instantiate(_stickPart, _stickSpawner1.position, Quaternion.identity);
        GameObject _obj2 = Instantiate(_stickPart, _stickSpawner2.position, Quaternion.identity);
        
        /*_obj1.GetComponent<Rigidbody>().AddTorque(new Vector3(0, -1, 0) * 50f, ForceMode.Impulse);
        _obj1.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * 5f, ForceMode.Impulse);
        _obj2.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);
        _obj2.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * 50f, ForceMode.Impulse);*/
        Destroy(_obj1, 3.0f);
        Destroy(_obj2, 3.0f);
    }

    public void StaffAttackStart()
    {
        _collider.enabled = true;
    }

    public void StaffAttackEnd()
    {
        _collider.enabled = false;
    }

    public void FinalMove()
    {
        if (!_wasThrowed)
        {
            transform.parent = null;
            _wasThrowed = true;
            Debug.Log("Бросок посоха");
            FinalStateStaffChange();
            _rigidbody.isKinematic = false;

            transform.rotation = Quaternion.Euler(0, 0, 90);
            _rigidbody.AddForce(new Vector3(0, 0.2f, 1) * 335f, ForceMode.Impulse);
            _rigidbody.AddTorque(new Vector3(0, -1, 0) * 505f, ForceMode.Impulse);
            _camera.SetPursuedObject(gameObject);
            _collider.isTrigger = false;
        }
    }

    public void FinalStateStaffChange()
    {
        if (!_isFinalStage)
        {
            _isFinalStage = true;
        }
        else if (_isFinalStage)
        {
            _isFinalStage = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.GetContact(0).point * (-1), ForceMode.Impulse);
            //Debug.Log("Челик оттолкнулся");

        }
    }
}
