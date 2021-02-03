using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapController : MonoBehaviour
{
    [Header("Level parts")]
    [SerializeField] private List<Image> _levels;

    private int _levelsCount;


    public void PaintLevels(int levelsCount)
    {
        if (levelsCount > _levels.Count)
        {
            _levelsCount = levelsCount % _levels.Count;
        }
        else
        {
            _levelsCount = levelsCount;
        }

        for (int i = 0; i <= (_levelsCount - 1); i++)
        {
            _levels[i].color = Color.green;
        }
    }
}