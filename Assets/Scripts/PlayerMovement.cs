using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float YDelay;
	public float MovingSpeed;
	public float MovingUpSpeed;
	public float MovingDownSpeed;

	[SerializeField] private InputController _inputController;
	[SerializeField] private StickModel _stickModel;
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private Rigidbody _playerRigidbody;
	[SerializeField] public CharacterState _currentState;
	[SerializeField] private Vector3 _delay;
	[SerializeField] private Vector3 _startPosition;
	[SerializeField] private Vector3 _movingVector;
	[SerializeField] private Vector3 _screenWall;
	[SerializeField] private Vector2 _minScreenPosition, _maxScreenPosition;
	[SerializeField] private bool _delayCounted;
	[SerializeField] private bool _isRunning;
	[SerializeField] private float _sliderSensetivity = 3.0f;
	[SerializeField] private Animator _animator;
	[SerializeField] private StickModel _staff;

	private float _attackCoolDown = 1.0f;
	private bool _attackInCoolDown = false;

	private void Start()
	{
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
	}
	//передвигает обьект в зависимости от положения пальца
	private void FixedUpdate()
	{
		switch (_currentState)
		{
			case CharacterState.Run:
                {
					
					_animator.SetBool(NameManager.RunState, true);
					_animator.SetBool(NameManager.FlyState, false);
					OnRunMovement();
                    break;
                }
            case CharacterState.Fly:
                {					
					_animator.SetBool(NameManager.RunState, false);
					_animator.SetBool(NameManager.FlyState, true);
					OnFlyMovement();
                    break;
                }
			case CharacterState.Final:
				{
					
					_animator.SetBool(NameManager.RunState, false);
					_animator.SetBool(NameManager.FlyState, false);
					_animator.SetBool(NameManager.FinalState, true);
					break;
				}
            default: _animator.SetBool(NameManager.RunState, true); break;
		}	
		
		if(_playerTransform.position.y <= -5)
		{
			Debug.Log("Game Over");
		}
		//ограничение экрана
		/*_screenWall.x = Mathf.Clamp(transform.position.x, _minScreenPosition.x, _maxScreenPosition.x);
		_screenWall.y = Mathf.Clamp(transform.position.y, _minScreenPosition.y, _maxScreenPosition.y);
		transform.position = _screenWall;*/		
	}

    

    private void OnFlyMovement()
    {
        _movingVector = _playerTransform.position;
        _movingVector.z += MovingSpeed;
        if (_inputController.DragingStarted)
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
        if (_inputController.DragingStarted)
        {
            if (!_delayCounted)
            {
                _delay = _inputController.TouchPosition;
                _startPosition = _playerTransform.position;
                _delayCounted = true;
            }
            _movingVector.x = _startPosition.x + (_inputController.TouchPosition.x - _delay.x) * _sliderSensetivity;			
            _playerTransform.position = _movingVector;
        }
        else
        {
            _delayCounted = false;
        }
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
			_animator.applyRootMotion = true;
			_currentState = state;
			_isRunning = false;
		}

	}
	private void MoveForward()
	{
		/*_movingVector = _playerTransform.position;
		_movingVector.z += MovingSpeed;
		_playerTransform.position = _movingVector;*/
		_playerTransform.Translate(Vector3.forward * 10f);
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

	public CharacterState GetState()
	{
		return _currentState;
	}
}
