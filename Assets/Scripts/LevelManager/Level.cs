using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            SceneManager.LoadScene(2);
        }   
    }
}
