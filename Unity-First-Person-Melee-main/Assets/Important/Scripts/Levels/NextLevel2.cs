using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel2 : MonoBehaviour
{
    public GameObject enemy;

   void Update()
    {   
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            SceneManager.LoadScene("Level 2"); // Load the scene with name "OtherSceneName"
        }
    }
}
