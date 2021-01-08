using UnityEngine;


namespace StaffRun
{
    [CreateAssetMenu(fileName = "Scene_1", menuName = "Create level", order = 0)]
    public class SOLevel : ScriptableObject
    {
        [Header("Level Parts")]
        [SerializeField] public LevelPart[] LevelParts;
    }
}
