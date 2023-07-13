using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Xiaohai.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "My Scriptable Objects/Input Reader")]
    public class InputReader : ScriptableObject
    {
        private MyInputActions _inputActions;
        public Vector2 Move { get => _inputActions.GamePlay.Move.ReadValue<Vector2>(); }
        public event Action OnAttack;
        public event Action OnAttackCanceled;
        public event Action OnSpawnEnemy;

        public void Enable()
        {
            _inputActions.Enable();
        }

        public void Disable()
        {
            _inputActions.Disable();
        }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new MyInputActions();
                _inputActions.GamePlay.Attack.performed += Attack_performed;
                _inputActions.GamePlay.Attack.canceled += Attack_canceled;
                _inputActions.GamePlay.SpawnEnemy.performed += SpawnEnemy_performed;
            }
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions = null;
        }

        private void SpawnEnemy_performed(InputAction.CallbackContext obj)
        {
            OnSpawnEnemy?.Invoke();
        }

        private void Attack_performed(InputAction.CallbackContext obj)
        {
            OnAttack?.Invoke();
        }

        private void Attack_canceled(InputAction.CallbackContext context)
        {
            OnAttackCanceled?.Invoke();
        }

    }
}
