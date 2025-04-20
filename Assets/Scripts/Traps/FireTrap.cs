using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("FireTrap Timer")]
    [SerializeField]private float activationDelay;
    [SerializeField]private float  activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    [Header("SFX")]
    [SerializeField]private AudioClip fireTrapSound;

    private bool triggered; // When the trap gets triggered
    private bool active; // When the trap is active and can hurt the player
    private Health playerHealth; 

    private void Awake() {
        
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update (){
        if (playerHealth != null && active ){
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered){
                //trigger the firetrap
                StartCoroutine(ActivateFiretrap());
                
            }
            if (active)
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player"){
            playerHealth = null;
        }
        
    }
    private IEnumerator ActivateFiretrap (){
        triggered = true;
        spriteRend.color = Color.red; // Turn the sprite to red to notify the player
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireTrapSound);
        spriteRend.color = Color.white; // Turn the sprite back to initial color
        active = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }
    
}
