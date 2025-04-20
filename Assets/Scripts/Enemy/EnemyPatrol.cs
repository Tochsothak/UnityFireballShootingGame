using Unity.Android.Gradle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [Header("Idle Behaviour")]
    [SerializeField]private float idleDuration;
    private float idleTimer;
    

    [Header("Enemy Animator")]
   [SerializeField] private Animator anim;

   private void Awake() {
        initScale = enemy.localScale;
   }

    void OnDisable()
    {
        anim.SetBool("moving", false) ;    
    }
    private void Update() {
        if (movingLeft){
            if (enemy.position.x >= leftEdge.position.x)
              MovingDirection(-1);
            else {
                // Change Direction
                DirectionChange();
            }
        }
        else {
            if (enemy.position.x <= rightEdge.position.x)
            MovingDirection(1);
            else {
                DirectionChange();
            }
        }
    }
    private void DirectionChange(){
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        movingLeft = !movingLeft;
    }

    private void MovingDirection(int _direction){
        idleTimer = 0;
          anim.SetBool("moving" , true);
        // Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x )* _direction, initScale.y, initScale.z);
        // Move  in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
      

    }
}
