using UnityEngine;
using System.Collections.Generic;

namespace StaffRun
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private SOLevel[] _levels;
        [SerializeField] private int _levelNumber;


        private void Awake()
        {
            BuildLevel(_levelNumber);
        }

        public void BuildLevel(int levelNumber)
        {
            List<LevelPart> levelParts = new List<LevelPart>();

            if (_levelNumber < 0 || _levelNumber >= _levels.Length)
            {
                _levelNumber = 0;
            }
            
            for (int i = 0; i < _levels[levelNumber].LevelParts.Length; i++)
            {
                levelParts.Add(Instantiate(_levels[levelNumber].LevelParts[i]));
            }

            for (int i = 0; i < levelParts.Count; i ++)
            {
                if (i > 0)
                {
                    levelParts[i].transform.position = levelParts[i - 1].GetPointNextPart();
                }
            }
        }
    }
}
