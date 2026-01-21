using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

}
