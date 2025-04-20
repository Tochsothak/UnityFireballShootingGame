using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private  Vector3[] initialPositions;
    private void Awake()
    {
        // Save the initial position of the enemies

        initialPositions = new Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i ++ ) {
            if (enemies[i] != null)
            initialPositions[i] = enemies[i].transform.position;
        }
    }

    public void ActivateRoom(bool _status) {
        // Activate/deactivate enemies
        for (int i = 0; i < enemies.Length; i ++) {
            if (enemies[i] != null){
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPositions[i];
            }
        }

    }

}
