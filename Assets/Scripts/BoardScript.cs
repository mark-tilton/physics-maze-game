using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public float Sensitivity = 0.1f;
    public float MaxSpeed = 10;

    private Vector3 _previousMousePosition = new Vector3(0, 0, 0);
    private Vector3 _rotation = new Vector3(0, 0, 0);
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _previousMousePosition = Input.mousePosition;
        _rigidBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaMousePosition = Input.mousePosition - _previousMousePosition;
        _previousMousePosition = Input.mousePosition;

        _rotation += new Vector3(deltaMousePosition.y, 0, -deltaMousePosition.x);
        _rigidBody.MoveRotation(Quaternion.Euler(_rotation));
    }
}