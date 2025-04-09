using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField]private UnityEngine.UI.Image totalHealthBar;
    [SerializeField]private UnityEngine.UI.Image currentHealthBar;

    private void Start() {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        
    }

    private void Update() {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

}
