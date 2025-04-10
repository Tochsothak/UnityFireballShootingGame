using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
  [Header ("Health")]
  [SerializeField] private float startingHealth;
  public float currentHealth {get; private set;}
  private Animator anim;

  private SpriteRenderer spriteRend;
  private bool dead;

  [Header ("Iframes")]
  [SerializeField] private float iframeDuration;
  [SerializeField] private float numberOfFlashes;
    private void Awake()
    {
     currentHealth = startingHealth;   
     anim = GetComponent<Animator>();
     spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage){
        currentHealth =  Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0) {
          //Player get hurt
          anim.SetTrigger("hurt");
          StartCoroutine(Invunerability());
            
        }
        else {
          // Player Dead
          if (!dead) {
             anim.SetTrigger("die");
             GetComponent<PlayerMovement>().enabled = false;
             dead = true;
          } 
        }
    }

    public void AddHealth (float _value) {
      currentHealth =  Mathf.Clamp(currentHealth + _value, 0, startingHealth);


    }
    
    private IEnumerator Invunerability (){
      Physics2D.IgnoreLayerCollision(10, 11, true);
      for (int i = 0;  i < numberOfFlashes; i ++){
        spriteRend.color = new Color(1,0,0,0.5f);
        yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        spriteRend.color = Color.white;
        yield return new WaitForSeconds(iframeDuration / (numberOfFlashes *2));

      }

      Physics2D.IgnoreLayerCollision(10, 11, false);

    }
}
