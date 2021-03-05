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

	private MainController _mainController;
	private float _attackCoolDown = 1.0f;
	private bool _attackInCoolDown = false;
	private Vector3 temp;
	private Quaternion _rotationForward;
	private float _oldSideDelay = 0;
	private Vector3 _oldControllerPosition;
	private float _sideDelaysDifference;

	private void Start()
	{
		_rotationForward = Quaternion.Euler(Vector3.forward);

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
		_rotationVector = new Vector3();
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
					_playerTransform.rotation = _rotationForward;
					_animator.SetBool(NameManager.RunState, true);
					_animator.SetBool(NameManager.FlyState, false);
					OnRunMovement();
					break;
				}
			case CharacterState.Fly:
				{
					_playerTransform.rotation = _rotationForward;
					_animator.SetBool(NameManager.RunState, false);
					_animator.SetBool(NameManager.FlyState, true);
					OnFlyMovement();
					break;
				}
			case CharacterState.Final:
				{
					_playerTransform.rotation = _rotationForward;
					_animator.SetBool(NameManager.RunState, false);
					_animator.SetBool(NameManager.FlyState, false);
					_animator.SetBool(NameManager.FinalState, true);
					break;
				}
			default: _animator.SetBool(NameManager.RunState, true); break;
		}

		if (_playerTransform.position.y <= _deathHeight)
		{
			_mainController.EndLevel(false);
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
		if (_inputController.DragingStarted && _stickModel.CanFlyUp)
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
				_startTouchPosition = _inputController.TouchPosition;
				_oldControllerPosition = _inputController.TouchPosition;
				_startPosition = _playerTransform.position;
				_delayCounted = true;
			}
			_movingVector.x = _startPosition.x + (_inputController.TouchPosition.x - _delay.x) * _sliderSensetivity;
			_playerTransform.position = _movingVector;
			_sideDelay = _startTouchPosition.x - _inputController.TouchPosition.x;
			//_sideDelay = _oldControllerPosition.x - _inputController.TouchPosition.x;
			_oldControllerPosition.x = _inputController.TouchPosition.x;
		}
		else
		{
			_delayCounted = false;
		}


		if (_sideDelay > _oldSideDelay)
		{
			_sideDelaysDifference = _sideDelay - _oldSideDelay;
		}
		else
		{
			_sideDelaysDifference =_oldSideDelay - _sideDelay;
		}	


		if (_sideDelaysDifference < 0.005)
		{
			if (_sideDelay > 0)
			{
				_sideDelay -= 0.01f;
				_startTouchPosition.x -= 0.01f;
			}
			else if (_sideDelay < 0)
			{
				_sideDelay += 0.01f;	
				_startTouchPosition.x += 0.01f;
			}

		}
		_oldSideDelay = _sideDelay;
		


		_rotationVector = _playerTransform.rotation.eulerAngles;
		_rotationVector.y = _sideDelay * RotationForce * -1f;

		if (_rotationVector.y > 30)
		{
			_rotationVector.y = 30;
			_sideDelay = -0.07f;
		}
		if (_rotationVector.y < -30)
		{
			_rotationVector.y = -30;
			_sideDelay = 0.07f;
		}
		/*
		if (_sideDelay >= 0.03)
		{
			
		}
		else if (_sideDelay <= -0.03)
		{
			
		}*/
		if (_sideDelay < 0.03f && _sideDelay > -0.03f && !_inputController.DragingStarted)
		{
			_playerTransform.rotation = _rotationForward;
			_sideDelay = 0;
		}
		else
		{ 
			_playerTransform.rotation = Quaternion.Euler(_rotationVector); 
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
	public CharacterState GetState()
	{
		return _currentState;
	}
}