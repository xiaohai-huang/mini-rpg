using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Xiaohai.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "My Scriptable Objects/Input Reader")]
    public class InputReader : ScriptableObject
    {
        public MyInputActions InputActions;
        public Vector2 Move { get => InputActions.GamePlay.Move.ReadValue<Vector2>(); }
        public event Action OnAttack;
        public event Action OnAttackCanceled;
        public event Action OnSpawnEnemy;


        private void OnEnable()
        {
            if (InputActions == null)
            {
                InputActions = new MyInputActions();
                InputActions.GamePlay.Attack.performed += Attack_performed;
                InputActions.GamePlay.Attack.canceled += Attack_canceled;
                InputActions.GamePlay.SpawnEnemy.performed += SpawnEnemy_performed;
            }
        }


        private void OnDisable()
        {
            InputActions.Disable();
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
