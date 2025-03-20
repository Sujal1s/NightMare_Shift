using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamepadDropdown : MonoBehaviour
{
    public Dropdown dropdown;

    void Update()
    {
        if (dropdown != null && EventSystem.current.currentSelectedGameObject == dropdown.gameObject)
        {
            if (Input.GetButtonDown("Submit")) // A button (Xbox)
            {
                dropdown.Show();
            }
        }
    }
}
