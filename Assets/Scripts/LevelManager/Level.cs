using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
          // Get the current scence index
          int currenrScenceIndex = SceneManager.GetActiveScene().buildIndex;
          // Load the next scence
          int nextScenseIndex =  currenrScenceIndex + 1;
          if (nextScenseIndex >= SceneManager.sceneCountInBuildSettings){
            nextScenseIndex = 0;
          }
          SceneManager.LoadScene(nextScenseIndex);
        }
    }
}