using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicTrigger : MonoBehaviour
{
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
        {
            AudioManager.instance.PlayMainMenuMusic();
        }
        else if (sceneName.StartsWith("level"))
        {
            AudioManager.instance.PlayLevelMusic();
        }
    }
}
