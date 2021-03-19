using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawObstacle : MonoBehaviour
{
    [SerializeField] private GameObject _sawObject;
    private PlayerMovement _player;
    private bool _isEnable = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        _sawObject?.transform.Rotate(0,0,-5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isEnable)
        {
            if (other.CompareTag("Player"))
            {
                _player.GetDamageFromObstacle();
                
            }
        }
        
    }
}
