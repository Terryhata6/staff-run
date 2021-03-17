using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraTurner : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private GameObject _cameraEndPosition;
    [SerializeField] private float _timeToEndLevel = 3.0f;

    private MainController _mainController;

    void Start()
    {
        _camera = FindObjectOfType<CameraController>();
        _mainController = FindObjectOfType<MainController>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinalTrigger"))
        {
            _camera.SetPursuedObject(_cameraEndPosition);
            Invoke("CompleteLevel", _timeToEndLevel);
        }
    }

    private void CompleteLevel()
    {
        _mainController.EndLevel(true);
    }
}
