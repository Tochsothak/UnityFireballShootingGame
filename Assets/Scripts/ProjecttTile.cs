using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjecttTile : MonoBehaviour
{
   [SerializeField] private float speed;
   private bool hit;
   private float direction;

    private BoxCollider2D boxColider2D;
    private Animator anim;


    private void Awake (){
        boxColider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        boxColider2D.enabled = false;
        anim.SetTrigger("explode");
        
    }
    public void setDirection(float _direction){
        direction  = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxColider2D.enabled = true;

        // Flip the fireball 
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

        

    }
    private void Deactivate(){
        gameObject.SetActive(false);

    }
}
