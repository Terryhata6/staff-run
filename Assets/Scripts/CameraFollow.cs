using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Player;
	private Vector3 PositionVector;
	private Vector3 StartPositionVector;
	private Transform CameraTransform;

	private void Start()
	{
		CameraTransform = GetComponent<Transform>();
		StartPositionVector = Player.position;
		PositionVector = StartPositionVector - CameraTransform.position;
	}
	private void Update()
	{
		CameraTransform.position = Player.position - PositionVector;
	}
}
