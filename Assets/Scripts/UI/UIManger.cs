using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField]private GameObject pauseScreen;

    private void Awake()
    {
       gameOverScreen.SetActive(false);
       pauseScreen.SetActive(false);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape)) 
        	 if (pauseScreen.activeInHierarchy)
                 PauseGame(false);
            else 
                 PauseGame(true);
    }
    #region Game Over
    public void GameOver(){
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Game Over function
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(){
       gameOverScreen.SetActive(false);
       pauseScreen.SetActive(true);
    }

    public void Quit(){
        Application.Quit(); // Quit the game (only in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only work in edit)
        # endif

    }
    #endregion

#region  Pause
    public void PauseGame(bool status){
        
        pauseScreen.SetActive(status);

        // When pause status is true change timeScale to 0 (time stop)
        //When it's pause change it back to 1 (time goes by normally)
        if (status)
        Time.timeScale = 0;
        else
        Time.timeScale = 1;


    }
    public void SoundVolume(){
        SoundManager.instance.ChangeSoundvolume(0.2f);
    }
    public void MusicVolume(){
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
#endregion

}


