using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewInputTest
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] float jumpForce = 5;
        Rigidbody _rigidbody;

        public void Initialize(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}