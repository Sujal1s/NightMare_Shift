using UnityEngine;
using System.Collections.Generic;

public class RealmShiftToggle : MonoBehaviour
{
    public List<GameObject> puzzle;
    [SerializeField] RealmShift realmShift;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5) && realmShift.isRealmShifted)
        {
            puzzlesobject();
        }
        else if (realmShift.isRealmShifted == false && puzzle[0].activeSelf == true)
        {
            foreach (GameObject obj in puzzle)
            {
                obj.SetActive(false);
            }
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