using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPostX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update(){

        // Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPostX,
        //  transform.position.y, transform.position.z),
        // ref velocity, speed);

        // Camera follow the player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x,  Time.deltaTime * cameraSpeed);
    } 

    public void MoveToRoom (Transform _newRoom){
        currentPostX = _newRoom.position.x;
    }
}
