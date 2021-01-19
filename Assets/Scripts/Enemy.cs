using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _visibleDistance;
    [SerializeField] private Transform _headTransform;
    private Transform _player;
    private float _distance;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(NameManager.Player).transform;
    }

    void Update()
    {
        _distance = Vector3.Distance(_player.position, transform.position);

        if (_distance <= _visibleDistance)
        {
            //transform.LookAt(_player);
            _headTransform.LookAt(_player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }
}
