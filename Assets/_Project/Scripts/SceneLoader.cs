using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNext()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextIndex >= SceneManager.sceneCountInBuildSettings) nextIndex = 0;
        SceneManager.LoadScene(nextIndex);
    }
}
