using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraTurner : MonoBehaviour
{
    [SerializeField] CameraController _camera;
    [SerializeField] GameObject _cameraEndPosition;

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
            Invoke("CompleteLevel", 3f);
        }
    }

    private void CompleteLevel()
    {
        _mainController.EndLevel(true);
    }
}
