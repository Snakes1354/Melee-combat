using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    

    
}
