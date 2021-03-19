using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesObstacle : MonoBehaviour
{
    [SerializeField] private GameObject _spikesObject;
    private PlayerMovement _player;
    private bool _isEnable = true;
    private bool _trapActive = true;
    [SerializeField] private float _changingStateTime = 2f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        ChangeState();
    }
       
    private void SetNewChanginState()
    {
        Invoke("ChangeState", _changingStateTime);
    }

    public void ChangeState()
    {
        if (_isEnable)
        {
            
        }
        SetNewChanginState();
        _trapActive = !_trapActive;
        _spikesObject.transform.Rotate(0, 0, 180);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            if(_trapActive)
            {
                _player.GetDamageFromObstacle();
            }
        }
    }
}
