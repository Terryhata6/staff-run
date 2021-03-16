using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float YDelay;
    public float MovingSpeed;
    public float MovingUpSpeed;
    public float MovingDownSpeed;
    public float RotationForce;

    [SerializeField] private InputController _inputController;
    [SerializeField] private StickModel _stickModel;
    private Transform _playerTransform;
    private Rigidbody _playerRigidbody;
    [SerializeField] public CharacterState _currentState;
    private Vector3 _delay;
    private Vector3 _startPosition;
    [SerializeField] private Vector3 _startTouchPosition;
    [SerializeField] private Vector3 _movingVector;
    [SerializeField] private Vector3 _rotationVector;
    [SerializeField] private Vector3 _screenWall;
    [SerializeField] private Vector2 _minScreenPosition, _maxScreenPosition;
    [SerializeField] private bool _delayCounted;
    [SerializeField] private bool _isRunning;
    [SerializeField] private float _sliderSensetivity = 3.0f;
    [SerializeField] private float _sideDelay;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _deathHeight = -5f;
    private Vector2 _movingVector2D;
    private Vector3 _touchDelta3D;
    private float _magnitude;
    private MainController _mainController;
    private float _attackCoolDown = 1.0f;
    private bool _attackInCoolDown = false;
    private Vector3 temp;
    private float _oldSideDelay = 0;
    private Vector3 _oldControllerPosition;
    private float _sideDelaysDifference;

    private bool _touchBegan = false;
    private bool _touchCancelled = false;
    private bool _touchMoved = false;
    private bool _touchEnded = false;
    private bool _touchStationary = false;

    private Vector2 _touchDelta2D = Vector2.zero;
    private Vector2 _touchDeltaNormalized = Vector2.zero;

    private void Start()
    {
        _mainController = FindObjectOfType<MainController>();
        _currentState = CharacterState.Run;
        _playerTransform = GetComponent<Transform>();
        _animator = GetComponentInChildren<Animator>();
        _inputController = FindObjectOfType<InputController>();
        _stickModel = FindObjectOfType<StickModel>();
        _delay = new Vector3(0, YDelay);
        _minScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) * 3f;
        _maxScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 3f;
        _movingVector = new Vector3();
        _screenWall = new Vector3(Mathf.Clamp(transform.position.x, _minScreenPosition.x, _maxScreenPosition.x), Mathf.Clamp(transform.position.y, _minScreenPosition.y, _maxScreenPosition.y), transform.position.z);
        _playerRigidbody = GetComponent<Rigidbody>();
        _isRunning = true;
        ChangePlayerState(CharacterState.Run);

        GameEvents.current.OnTouchBeganEvent += OnTouchPhaseBegan;
        GameEvents.current.OnTouchMovedEvent += OnTouchPhaseMoved;
        GameEvents.current.OnTouchEndedEvent += OnTouchPhaseEnded;
        GameEvents.current.OnTouchStationaryEvent += OnTouchPhaseStationary;
        GameEvents.current.OnTouchCancelledEvent += OnTouchPhaseCancelled;
    }
    //передвигает обьект в зависимости от положения пальца
    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case CharacterState.Run:
                {
                    _playerTransform.rotation = Quaternion.Euler(Vector3.forward);
                    _animator.SetBool(NameManager.RunState, true);
                    _animator.SetBool(NameManager.FlyState, false);
                    OnRunMovement();
                    break;
                }
            case CharacterState.Fly:
                {
                    _playerTransform.rotation = Quaternion.Euler(Vector3.forward);
                    _animator.SetBool(NameManager.RunState, false);
                    _animator.SetBool(NameManager.FlyState, true);
                    OnFlyMovement();
                    break;
                }
            case CharacterState.Final:
                {
                    _playerTransform.rotation = Quaternion.Euler(Vector3.forward);
                    _animator.SetBool(NameManager.RunState, false);
                    _animator.SetBool(NameManager.FlyState, false);
                    _animator.SetBool(NameManager.FinalState, true);
                    break;
                }
            case CharacterState.Hurricane:
                {
                    _playerTransform.rotation = Quaternion.Euler(Vector3.forward);
                    _animator.SetBool(NameManager.RunState, false);
                    _animator.SetBool(NameManager.FlyState, false);
                    _animator.SetBool(NameManager.FinalState, false);
                    OnHurricaneMovement();
                    break;
                }
            case CharacterState.Balancing:
                {
                    
                    break;
                }

            default: _animator.SetBool(NameManager.RunState, true); break;
        }

        if (_playerTransform.position.y <= _deathHeight)
        {
            _mainController.EndLevel(false);
            Debug.Log("Game Over");
        }
    }

    private void OnBalancingMovement()
    { 
    
    }

    private void OnHurricaneMovement()
    {
        _movingVector = Vector3.zero;
        if (_touchBegan)
        {
            _startPosition = _playerTransform.position;
        }
        if (_touchMoved || _touchStationary)
        {
            //_movingVector.z += MovingSpeed;
            /*
            _movingVector.x = _playerTransform.position.x +_touchDelta2D.normalized.x *Time.deltaTime;
            _movingVector.y = 0;
            _movingVector.z = _playerTransform.position.z +_touchDelta2D.normalized.y *Time.deltaTime;
            */
            _movingVector = _touchDelta3D - _startTouchPosition;
            _magnitude = _movingVector.magnitude;
            if (_magnitude > 100)
            {
                _magnitude = 100.0f;
            }
            transform.rotation = Quaternion.LookRotation(_movingVector, Vector3.up);
            transform.Translate(Vector3.forward * _magnitude * 0.01f * 5 * Time.deltaTime);

        }
        _playerTransform.position += Vector3.forward * Time.deltaTime;
    }
    private void OnFlyMovement()
    {
        _movingVector = _playerTransform.position;
        _movingVector.z += MovingSpeed;
        /*if (_inputController.DragingStarted && _stickModel.CanFlyUp)
        {
            _movingVector.y += MovingUpSpeed;
        }*/
        if ((_touchBegan || _touchMoved || _touchStationary) && _stickModel.CanFlyUp)
        {
            _movingVector.y += MovingUpSpeed;
        }
        else
        {
            _movingVector.y -= MovingDownSpeed;
        }
        _playerTransform.position = _movingVector;

    }
    private void OnRunMovement()
    {
        _movingVector = _playerTransform.position;
        _movingVector.z += MovingSpeed;
        if (_rotationVector == null)
        {
            _rotationVector = _playerTransform.rotation.eulerAngles;
        }
        if (_touchBegan)
        {
            _startPosition = _playerTransform.position;
        }
        if (_touchMoved)
        {
            //_movingVector.x = _playerTransform.position.x + _touchDelta.x;
            if (_touchDelta2D.x - _startTouchPosition.x > 0)
            {
                _movingVector.x += _sliderSensetivity;
            }
            else if (_touchDelta2D.x - _startTouchPosition.x < 0)
            {
                _movingVector.x -= _sliderSensetivity;
            }
            //_movingVector.x = _startPosition.x + (_touchDeltaNormalized.x - _startTouchPosition.x);// * _sliderSensetivity;
            _rotationVector.y = _touchDelta2D.x - _startTouchPosition.x;
            if (_rotationVector.y > 30f)
            {
                _rotationVector.y = 30f;
            }
            else
            {
                _rotationVector.y += 1.0f;
            }
            if (_rotationVector.y < -30f)
            {
                _rotationVector.y = -30f;
            }
            else
            {
                _rotationVector.y -= 1.0f;
            }
        }
        if (_touchStationary)
        {
            _startTouchPosition = _touchDelta2D;
        }
        if (_touchEnded || _touchStationary /*|| (!_touchBegan && !_touchCancelled && !_touchEnded && !_touchMoved && !_touchStationary )*/)
        {
            if (_rotationVector.y >= -30f && _rotationVector.y < 0f)
            {
                _rotationVector.y += 2.0f;
            }
            else if (_rotationVector.y <= 30f && _rotationVector.y > 0f)
            {
                _rotationVector.y -= 2.0f;
            }
        }
        _playerTransform.rotation = Quaternion.Euler(_rotationVector);
        _playerTransform.position = _movingVector;
    }


    public void ChangePlayerState(CharacterState state)
    {
        Debug.Log("ChangePalyerState");

        if (state == CharacterState.Fly && _currentState != state)
        {
            _animator.applyRootMotion = false;
            _currentState = state;
            _isRunning = false;
            _playerRigidbody.isKinematic = true;
            _stickModel.ChangePositionOfStick();
        }
        else if (state == CharacterState.Run && _currentState != state)
        {
            _animator.applyRootMotion = false;
            _currentState = state;
            _isRunning = true;
            _playerRigidbody.isKinematic = false;
            _stickModel.ChangePositionOfStick();
        }
        else if (state == CharacterState.Final && _currentState != state)
        {

            _currentState = state;
            _isRunning = false;
        }

    }

    private void OnTriggerEnter(Collider _entryCollider)
    {
        if (!_attackInCoolDown)
        {
            if (_entryCollider.gameObject.CompareTag("EnemyInAttackRange"))
            {
                if (!_attackInCoolDown)
                {
                    _animator.SetTrigger("Attack 1");
                    _attackInCoolDown = true;
                    Invoke("AttackCooldownReset", _attackCoolDown);
                }
            }
        }
    }

    private void AttackCooldownReset()
    {
        _attackInCoolDown = false;
    }

    public void SetAnimatorApplyMotion(bool value)
    {
        _animator.applyRootMotion = value;
    }

    public void OnTouchPhaseBegan(Vector2 position)
    {
        _startTouchPosition.x = position.x;
        _startTouchPosition.y = 0;
        _startTouchPosition.z = position.y;
        _touchDelta2D = position;
        _touchDelta3D.x = _touchDelta2D.x;
        _touchDelta3D.y = 0;
        _touchDelta3D.z = _touchDelta2D.y;

        _touchDeltaNormalized = position.normalized;
        _touchBegan = true;
        _touchCancelled = false;
        _touchMoved = false;
        _touchEnded = false;
        _touchStationary = false;
    }
    public void OnTouchPhaseCancelled()
    {
        _touchBegan = false;
        _touchCancelled = true;
        _touchMoved = false;
        _touchEnded = false;
        _touchStationary = false;
    }
    public void OnTouchPhaseMoved(Vector2 delta)
    {
        _touchDelta2D = delta;
        _touchDelta3D.x = delta.x;
        _touchDelta3D.y = 0;
        _touchDelta3D.z = delta.y;
        _touchDeltaNormalized = delta.normalized;
        _touchBegan = false;
        _touchCancelled = false;
        _touchMoved = true;
        _touchEnded = false;
        _touchStationary = false;
    }
    public void OnTouchPhaseEnded()
    {
        _touchDelta2D = Vector2.zero;
        _touchBegan = false;
        _touchCancelled = false;
        _touchMoved = false;
        _touchEnded = true;
        _touchStationary = false;
    }
    public void OnTouchPhaseStationary(Vector2 position)
    {
        //_startTouchPosition = position;
        _touchBegan = false;
        _touchCancelled = false;
        _touchMoved = false;
        _touchEnded = false;
        _touchStationary = true;
    }

    public CharacterState GetState()
    {
        return _currentState;
    }
}