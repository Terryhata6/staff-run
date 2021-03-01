using UnityEngine;
using System.Collections.Generic;


public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private SOLevel[] _levels;

    public void BuildLevel(int levelNumber)
    {
        List<LevelPart> levelParts = new List<LevelPart>();

        if (levelNumber < 0 || levelNumber >= _levels.Length)
        {
            levelNumber = levelNumber % _levels.Length;
        }

        Debug.Log($"Level Number:{levelNumber} ");
        for (int i = 0; i < _levels[levelNumber].LevelParts.Length; i++)
        {
            levelParts.Add(Instantiate(_levels[levelNumber].LevelParts[i]));
        }

        for (int i = 0; i < levelParts.Count; i++)
        {
            if (i > 0)
            {
                levelParts[i].transform.position = levelParts[i - 1].GetPointNextPart();
            }
        }
    }
}