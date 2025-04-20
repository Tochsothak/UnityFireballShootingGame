using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
   [SerializeField] private float healthValue;
   [Header("SFX")]
   [SerializeField] private AudioClip collectableSound;

   private void OnTriggerEnter2D(Collider2D collision){
    if (collision.tag == "Player") {
        SoundManager.instance.PlaySound(collectableSound);
        collision.GetComponent<Health>().AddHealth(healthValue);
        gameObject.SetActive(false);
    }
   }
}
