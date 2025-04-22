using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckPoint;
    private Health playerHealth;
    private UIManger uIManger;

    private void Awake()
    {
      playerHealth = GetComponent<Health>();  
      uIManger = FindFirstObjectByType<UIManger>();
    }

    public void CheckRespawn(){
        // Check if check point available
        if (currentCheckPoint == null){
            // Show game over screen
            uIManger.GameOver();
            
            return; // Don't execute the res of this function
        }

       
        // Reset player health and reset Get
        playerHealth.Respawn();
        transform.position = currentCheckPoint.position; // Move the player to the checkpoint position

        // Move camera back to the checkPoint
        Camera.main.GetComponent<CameraController>().MoveToRoom(currentCheckPoint.parent);
    }

    // Activate checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint"){
            currentCheckPoint = collision.transform;
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
