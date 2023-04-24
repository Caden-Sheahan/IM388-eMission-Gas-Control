using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxVacBehaviour : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private PlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 distance = new(Mathf.Abs(_player.transform.position.x - transform.position.x), Mathf.Abs(_player.transform.position.z - transform.position.z));

        if (distance.magnitude <= _radius && Mathf.Abs(_player.transform.position.y - transform.position.y) < 3)
        {

        }
    }
}
