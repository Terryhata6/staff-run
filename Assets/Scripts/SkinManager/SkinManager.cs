using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _staffSkinsGameObjects;
    private Dictionary<string,GameObject> _staffLists = new Dictionary<string, GameObject>();
    private GameObject _enabledSkin;
    
    public void Awake()
    {
        foreach (GameObject staff in _staffSkinsGameObjects)
        {
            if (staff != null)
            {
                if (!_staffLists.ContainsKey(staff.name))
                {
                    _staffLists.Add(staff.name, staff);
                }
                if (true != true)
                { 
                    
                }
                else if (staff.activeSelf)
                {
                    _enabledSkin = staff;
                }
            }
        }
    }



    
}
