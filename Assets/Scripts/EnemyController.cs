using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _rangeToPlayerToFlee = 5f;
    private bool _touched = false;

    private bool _playerIsInRange = false;

    private PlayerController _player;

    private void Start()
    {
        _touched = false;
        _player = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_touched)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        
        if (_rangeToPlayerToFlee >= distanceToPlayer)
        {
            Vector3 _directionToPlayer = _player.transform.position - transform.position;
            Vector3 _oppositeDirection = (transform.position - _directionToPlayer).normalized;
            transform.position += _oppositeDirection * _movementSpeed * Time.deltaTime;
        }
        ClampPositionWithinScreenBounds();
    }

    public void OnTouch()
    {
        _touched = true;
    }

    private void ClampPositionWithinScreenBounds()
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

}
