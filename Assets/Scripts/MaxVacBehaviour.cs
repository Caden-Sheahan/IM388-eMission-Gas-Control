using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxVacBehaviour : MonoBehaviour
{
    [SerializeField] private float _radius;
    private PlayerMovement _player;
    [SerializeField] private bool _playerInRange;

    public bool PlayerInVacRange => _playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<PlayerMovement>();
        }

        Vector2 distance = new(Mathf.Abs(_player.transform.position.x - transform.position.x), Mathf.Abs(_player.transform.position.z - transform.position.z));

        _player.overcharged = distance.magnitude <= _radius && Mathf.Abs(_player.transform.position.y - transform.position.y) < 3;
    }
}
