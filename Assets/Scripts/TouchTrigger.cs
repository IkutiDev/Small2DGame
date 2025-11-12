using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _enemyController.OnTouch();
        }
    }
}
