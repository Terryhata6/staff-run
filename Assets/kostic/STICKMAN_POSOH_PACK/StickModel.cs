using UnityEngine;

public class StickModel : MonoBehaviour
{
	[SerializeField] CameraController _camera;
	public Vector3 RotatingPositionOfStick;
	public Vector3 RotatingRotationOfStick;
	public float AmountOfGettingBigger;
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
	}
	public void IncreaseLenghtOfStick()
	{
		_scaleVector = _stickTransform.localScale;
		_scaleVector.y += AmountOfGettingBigger;
		_stickTransform.localScale = _scaleVector;
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
	private void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			IncreaseLenghtOfStick();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			ChangePositionOfStick();
		}
		if (_isRotating)
		{
			if(_inputController.InputStarted && _rotatingSpeed != MaxRotatingSpeed)
			{
				_rotatingSpeed = MaxRotatingSpeed;
				_rotatingVector.x = _rotatingSpeed;
			}
			else if (!_inputController.InputStarted && _rotatingSpeed != RotatingSpeed)
			{
				_rotatingSpeed = RotatingSpeed;
				_rotatingVector.x = _rotatingSpeed;
			}
			RotateStick();
		}
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

			transform.rotation = Quaternion.Euler(0,0 ,90);
			_rigidbody.AddForce(new Vector3(0, 0.2f, 1)*335f, ForceMode.Impulse);
			_rigidbody.AddTorque(new Vector3(0, -1, 0)*505f, ForceMode.Impulse);
			_camera.SetPursuedObject(gameObject);
			_collider.isTrigger = false;
			



		}
		///Активировать RigidBody
		///Дать импульс

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
			collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.GetContact(0).point * (-1),ForceMode.Impulse);
			Debug.Log("Челик оттолкнулся");
			
		}
    }
}
