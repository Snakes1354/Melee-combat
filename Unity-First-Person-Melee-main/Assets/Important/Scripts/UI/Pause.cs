using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Container;
    public GameObject Health;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Container.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Health.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        Container.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}