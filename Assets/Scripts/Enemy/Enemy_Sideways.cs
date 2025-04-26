using System.Runtime.Serialization;
using TreeEditor;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
   [SerializeField] private  float movementDistance;
   [SerializeField]private float speed;
   [SerializeField] private float damage;

   private bool movingLeft;
   private float leftEdge;
   private float rightEdge;

   private void  Awake () {
    leftEdge = transform.position.y - movementDistance;
    rightEdge = transform.position.y + movementDistance;
   }

   private void Update () {
    if (movingLeft) {
        if (transform.position.y > leftEdge){
            transform.position = new Vector3(transform.position.x,
             transform.position.y - speed * Time.deltaTime, transform.position.z);

        } 
        else {
            movingLeft = false;
        }
    }
    else {
        if ( transform.position.y < rightEdge) {
               transform.position = new Vector3(transform.position.x,
             transform.position.y + speed * Time.deltaTime, transform.position.z);


        }
        else 
        movingLeft = true;
    }
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            collision.GetComponent<Health>().TakeDamage(damage);
        }    
    }
}
