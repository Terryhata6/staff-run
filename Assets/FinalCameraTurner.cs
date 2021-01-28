using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraTurner : MonoBehaviour
{
    [SerializeField] CameraController _camera;
    [SerializeField] GameObject _cameraEndPosition;
    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinalTrigger"))
        {
            _camera.SetPursuedObject(_cameraEndPosition);
        }
    }
}
