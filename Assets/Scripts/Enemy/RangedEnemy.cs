using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
   [Header("Attack Parameters")]
   [SerializeField] private float attackCooldown;
   [SerializeField] private float range;
   [SerializeField] private int damage;

   [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

   [Header("Collider Parameter")]
   [SerializeField] private float boxColliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

   [Header("Player Layer")]
   [SerializeField] private LayerMask playerLayer;
   private float cooldownTimer = Mathf.Infinity;

   [Header("Fireball Sound")]
   [SerializeField]private AudioClip fireballSound;

   // References
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator> ();
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); 
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        // Attack only Player in sight? 
        if (PlayerInSight()){
             if (cooldownTimer >= attackCooldown){
            cooldownTimer = 0;
            anim.SetTrigger("rangeAttack"); 
        }
        }
        if (enemyPatrol != null) 
            enemyPatrol.enabled = !PlayerInSight();
    } 

    private void RangeAttack () {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<EnemyProjectTile>().ActiveProjectTile();
    }

    private int FindFireBall(){
        for (int i = 0; i < fireballs.Length; i ++){
            if (!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * boxColliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z) ,
        0,Vector2.left,0, playerLayer);

        return  hit.collider != null;
    }

    private void OnDrawGizmos() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right *range * transform.localScale.x * boxColliderDistance,
      new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
       
    }



}
