using UnityEngine;


namespace StaffRun
{
    [CreateAssetMenu(fileName = NameManager.Scene_1, menuName = NameManager.CreateLevel, order = 0)]
    public class SOLevel : ScriptableObject
    {
        [Header(NameManager.LevelParts)]
        [SerializeField] public LevelPart[] LevelParts;
    }
}
