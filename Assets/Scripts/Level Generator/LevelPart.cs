using UnityEngine;


namespace StaffRun
{
    public class LevelPart : MonoBehaviour
    {
        [SerializeField] private GameObject _endPoint;

        public Vector3 GetPointNextPart()
        {
            return _endPoint.transform.position;
        }
    }
}