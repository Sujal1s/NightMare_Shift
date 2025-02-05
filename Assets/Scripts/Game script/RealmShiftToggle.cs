using UnityEngine;
using System.Collections.Generic;

public class RealmShiftToggle : MonoBehaviour
{
    public List<GameObject> puzzle;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            puzzlesobject();
        }
    }

    void puzzlesobject()
    {
        foreach (GameObject obj in puzzle)
        {
            if (obj.activeSelf != true)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}