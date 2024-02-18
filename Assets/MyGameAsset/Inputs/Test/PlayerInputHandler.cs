using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputTest
{
    public class PlayerInputHandler : MonoBehaviour
    {
        GameInputs _gameInputs;
        PlayerMovement _playerMovement;
        PlayerJump _playerJump;

        void Awake()
        {
            _gameInputs = new GameInputs();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerJump = GetComponent<PlayerJump>();

            _gameInputs.Player.Move.performed += ctx => _playerMovement?.Move(ctx.ReadValue<Vector2>());
            _gameInputs.Player.Jump.performed += ctx => _playerJump?.Jump();

            _gameInputs.Enable();
        }

        void OnDestroy()
        {
            _gameInputs?.Dispose();
        }
    }
}