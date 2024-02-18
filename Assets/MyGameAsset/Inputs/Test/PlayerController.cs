using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewInputTest
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerMovement), typeof(PlayerJump))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            GetComponent<PlayerMovement>().Initialize(_rigidbody);
            GetComponent<PlayerJump>().Initialize(_rigidbody);
        }
    }
}