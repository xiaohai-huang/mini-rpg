using System;
using UnityEngine;
using Xiaohai.Input;

[RequireComponent(typeof(Character))]
public class CharacterPlayerInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private AbilityButton _abilityOneButton;
    [SerializeField] private AbilityButton _abilityTwoButton;
    private Character _character;

    void Awake()
    {
        _character = GetComponent<Character>();
    }
    void OnEnable()
    {
        _inputReader.OnAttack += OnAttack;
        _inputReader.OnAttackCanceled += OnAttackCanceled;

        _abilityOneButton.OnClick += OnAbilityOneClicked;
        _abilityTwoButton.OnClick += OnAbilityTwoClicked;
    }



    void OnDisable()
    {
        _inputReader.OnAttack -= OnAttack;
        _inputReader.OnAttackCanceled -= OnAttackCanceled;

        _abilityOneButton.OnClick -= OnAbilityOneClicked;
        _abilityTwoButton.OnClick -= OnAbilityTwoClicked;
    }

    void Update()
    {
        _character.HorizontalInput = _inputReader.Move;
    }

    void OnAttack()
    {
        _character.AttackInput = true;
    }
    private void OnAttackCanceled()
    {
        _character.AttackInput = false;
    }

    private void OnAbilityOneClicked(Vector2 position)
    {
        _character.AbilityOneInput = true;
        _character.AbilityOneDirection = position.normalized;
    }

    private void OnAbilityTwoClicked(Vector2 position)
    {
        _character.AbilityTwoInput = true;
        _character.AbilityTwoDirection = position.normalized;
    }
}