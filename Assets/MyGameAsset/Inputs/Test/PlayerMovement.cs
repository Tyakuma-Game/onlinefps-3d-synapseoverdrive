using UnityEngine;

namespace NewInputTest
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveForce = 5;
        Rigidbody _rigidbody;
        Vector2 _moveInputValue;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="rigidbody">物理コンポーネント取得</param>
        public void Initialize(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="input">移動の入力値</param>
        public void Move(Vector2 input)
        {
            _moveInputValue = input;
            Vector3 moveDirection = new Vector3(_moveInputValue.x, 0, _moveInputValue.y);
            _rigidbody.AddForce(moveDirection * moveForce * Time.deltaTime);
        }
    }
}