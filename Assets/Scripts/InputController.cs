using UnityEngine;

public class InputController : MonoBehaviour
{
	//следит за пальцем на экране
	public Vector3 TouchPosition;
	public bool DragingStarted = false;
	public bool OnTouch = false;
	public bool _oneTouchRota = false;
	public bool UseMouse = false;
	public Camera CameraForInput;
	private Touch _touch;

	private void Start()
	{
		//CameraForInput = FindObjectOfType<Camera>();
		TouchPosition = new Vector3(0, 0);
	}

	private void Update()
	{
		if (!UseMouse)
		{
			if (Input.touchCount > 0)
			{
				_touch = Input.GetTouch(0);
				
				if (_touch.phase == TouchPhase.Began)
				{
					
					DragingStarted = true;
					if (!_oneTouchRota)
					{
						OnTouch = true;
						_oneTouchRota = true;
					}
					else
					{
						OnTouch = false;
					}
					TouchPosition = CameraForInput.ScreenToWorldPoint(_touch.position);
				}
				else if (_touch.phase == TouchPhase.Moved)
				{
					TouchPosition = CameraForInput.ScreenToWorldPoint(_touch.position);
				}
				
				switch (_touch.phase)
				{
					case TouchPhase.Began:
						{
							GameEvents.current.TouchBeganEvent(_touch.position);							
							break;
						}
					case TouchPhase.Canceled:
						{
							GameEvents.current.TouchCancelledEvent();
							break;
						}
					case TouchPhase.Moved:
						{
							GameEvents.current.TouchMovedEvent(_touch.position);
							break;
						}
					case TouchPhase.Ended:
						{
							GameEvents.current.TouchEndedEvent();
							break;
						}
					case TouchPhase.Stationary:
						{
							GameEvents.current.TouchStationaryEvent(_touch.position);
							break;
						}
					default:break;
				}


			}
			else
			{
				DragingStarted = false;
				_oneTouchRota = false;
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				DragingStarted = true;
				TouchPosition = Input.mousePosition / 100;
				if (!_oneTouchRota)
				{
					OnTouch = true;
					_oneTouchRota = true;
				}
				else
				{
					OnTouch = false;
				}
			}
			else
			{
				DragingStarted = false;
				_oneTouchRota = false;
			}
		}
	}
}