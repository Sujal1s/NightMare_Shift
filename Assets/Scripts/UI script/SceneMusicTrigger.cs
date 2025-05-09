using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicTrigger : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.instance == null) return;

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu" || sceneName == "SettingMenu")
        {
            AudioManager.instance.PlayMainMenuMusic();
        }
        else if (sceneName.StartsWith("level"))
        {
            AudioManager.instance.PlayLevelMusic();
        }
    }
}
