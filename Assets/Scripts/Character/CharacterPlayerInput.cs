using UnityEngine;
using Xiaohai.Input;

[RequireComponent(typeof(Character))]
public class CharacterPlayerInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private OnScreenInputEventChannel _onScreenInputEventChannel;

    private Character _character;
    private EffectSystem _effectSystem;

    void Awake()
    {
        _character = GetComponent<Character>();
        _effectSystem = GetComponent<EffectSystem>();
    }
    void OnEnable()
    {
        _inputReader.OnAttack += OnAttack;
        _inputReader.OnAttackCanceled += OnAttackCanceled;

        _onScreenInputEventChannel.OnClickEventRaised += OnScreenButtonClickEventRaised;
    }

    void OnDisable()
    {
        _inputReader.OnAttack -= OnAttack;
        _inputReader.OnAttackCanceled -= OnAttackCanceled;

        _onScreenInputEventChannel.OnClickEventRaised -= OnScreenButtonClickEventRaised;
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

    private void OnAbilityThreeClicked(Vector2 position)
    {
        _character.AbilityThreeInput = true;
        _character.AbilityThreeDirection = position.normalized;
    }

    private void OnHealClicked()
    {
        _effectSystem.AddEffect(new HealEffect("Heal Once", 520));
    }

    private void OnScreenButtonClickEventRaised(OnScreenInputEventChannel.Input inputType, Vector2 position)
    {
        switch (inputType)
        {
            case OnScreenInputEventChannel.Input.ABILITY_ONE:
                {
                    OnAbilityOneClicked(position);
                    break;
                }
            case OnScreenInputEventChannel.Input.ABILITY_TWO:
                {
                    OnAbilityTwoClicked(position);
                    break;
                }
            case OnScreenInputEventChannel.Input.ABILITY_THREE:
                {
                    OnAbilityThreeClicked(position);
                    break;
                }
            case OnScreenInputEventChannel.Input.HEAL:
                {
                    OnHealClicked();
                    break;
                }
        }
    }
}