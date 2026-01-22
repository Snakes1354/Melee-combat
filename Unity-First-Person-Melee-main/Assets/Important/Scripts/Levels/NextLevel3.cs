using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel3 : MonoBehaviour
{
    public GameObject enemy;

   void Update()
    {   
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            SceneManager.LoadScene("Level 3"); // Load the scene with name "level 3"
        }
    }
}
