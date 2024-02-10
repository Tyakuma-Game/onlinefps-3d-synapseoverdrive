using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData1 : MonoBehaviour
{
    [SerializeField] float _moveForce = 5;
    [SerializeField] float _jumpForce = 5;

    public float MoveForce { get; private set; }
    public float JumpForce { get; private set; }

    void Start()
    {
        MoveForce = _moveForce;
        JumpForce = _jumpForce;
    }
}