using Unity.VisualScripting;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{

    [SerializeField]private  float attackCoolDown;
    [SerializeField]private Transform firePoint;
    [SerializeField]private GameObject[] arrows;
    [Header("SFX ")]
    [SerializeField]private AudioClip arrowSound;

    private float  cooldownTimer;

    private  void Attack(){
        SoundManager.instance.PlaySound(arrowSound);
        cooldownTimer  = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectTile>().ActiveProjectTile();

    }
    private int FindArrow() {
        for (int i = 0; i < arrows.Length; i ++) {
            if (!arrows[i].activeInHierarchy)
            return i;
        }
        return 0;
    }

    private void Update () {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCoolDown) 
        Attack();
    }
    
}
