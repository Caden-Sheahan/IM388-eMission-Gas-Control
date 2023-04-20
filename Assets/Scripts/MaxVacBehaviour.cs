using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxVacBehaviour : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private Transform _playerTsfm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 distance = new(Mathf.Abs(_playerTsfm.position.x - transform.position.x), Mathf.Abs(_playerTsfm.position.z - transform.position.z));

        if (distance.magnitude <= _radius)
        {

        }
    }
}
