using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToWinScreen : MonoBehaviour
{
    public GameObject enemy;

   void Update()
    {   
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            SceneManager.LoadScene("Winning Menu"); // Load the scene with name "Winning Menu"
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
