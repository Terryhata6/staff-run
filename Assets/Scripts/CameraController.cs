using UnityEngine;

/// <summary>
/// Объявляем и меняем объект+
/// Управляем расстоянием от камеры до объекта+
/// Плавно перемещаем Камеру к новому объекту слежения - в fixedupdate
/// *Изменяем угол обзора камеры - меняем ротейт оператора -
/// *Сохранять ротейт в пресет по енамам и переключать мгновенно/плавно
/// </summary>


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _pursuedObject;
    [SerializeField] private float _cameraDistance;    
    [SerializeField] private float _smooth = 0.05f;
    [SerializeField] private float _stopChangingDistance = 0.05f;
    private bool _objectChanging;

    private Camera _mainCamera;

    private void Start()
    {        
        _mainCamera = GetComponentInChildren<Camera>();
        _objectChanging = false;
    }

    
    private void Update()
    {
        if(_objectChanging)
        {
            if (Vector3.Distance(_pursuedObject.transform.position, transform.position) < _stopChangingDistance)
            {
                _objectChanging = false;
            }
        }            
    }

    private void LateUpdate()
    {
        if (!_objectChanging)
        {
            transform.position = _pursuedObject.transform.position;
        }
        else 
        {
            Vector3 pos = new Vector3(_pursuedObject.transform.position.x, _pursuedObject.transform.position.y, _pursuedObject.transform.position.z); // сохраняем Z координату камеры
            transform.position = Vector3.Lerp(transform.position, pos, _smooth);
        }
    }

    public void SetPursuedObject(GameObject obj) 
    {
        _pursuedObject = obj;
        _objectChanging = true;
    }

    public void SetCameraDistance(float value) { }

    public GameObject GetPursuedObject() 
    {
        return _pursuedObject;
    }

    
}
