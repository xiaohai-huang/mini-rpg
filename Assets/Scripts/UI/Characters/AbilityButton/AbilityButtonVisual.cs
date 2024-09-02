using Core.Game.Combat;
using Core.Game.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityHFSM;

[RequireComponent(typeof(AbilityButton))]
public class AbilityButtonVisual : IconButtonVisual
{
    [SerializeField]
    private Image _abilityLevel;

    [SerializeField]
    private AbilityCDVisual _cdVisual;

    [SerializeField]
    private Image _abilityReadyEffect;

    [SerializeField]
    private AbilityBase _ability;
    private Material _abilityLevelMat;
    private Material _abilityReadyEffectMat;
    private static readonly int ABILITY_LEVEL = Shader.PropertyToID("_Level");
    private static readonly int ABILITY_LEVEL_NUM_LEVELS = Shader.PropertyToID("_NumLevels");
    private static readonly int ABILITY_READY_EFFECT_PROGRESS = Shader.PropertyToID("_Progress");

    private StateMachine<States> _fsm;

    enum States
    {
        Lv0,
        Ready,
        Performing,
        Disabled,
        Ghost
    }

    public override void Awake()
    {
        base.Awake();
        _ability.OnLevelChange.AddListener(HandleLevelChange);

        _fsm = new StateMachine<States>(this);

        _fsm.AddState(
            States.Lv0,
            onEnter: (_) =>
            {
                _darkCover.SetActive(true);
            },
            onExit: (_) =>
            {
                _darkCover.SetActive(false);
            }
        );
        _fsm.AddState(
            States.Ready,
            onEnter: (_) =>
            {
                // Play ready effect
                if (_abilityReadyEffectMat == null)
                {
                    _abilityReadyEffect.material = Instantiate(_abilityReadyEffect.material);
                    _abilityReadyEffectMat = _abilityReadyEffect.material;
                }
                _abilityReadyEffectMat.SetFloat(ABILITY_READY_EFFECT_PROGRESS, 0);
                DOTween.To(
                    () => _abilityReadyEffectMat.GetFloat(ABILITY_READY_EFFECT_PROGRESS),
                    (newValue) =>
                    {
                        _abilityReadyEffectMat.SetFloat(ABILITY_READY_EFFECT_PROGRESS, newValue);
                    },
                    1f,
                    1
                );
            }
        );
        _fsm.AddState(
            States.Performing,
            onEnter: (_) => {
                // play 'click' visual effect
            }
        );
        _fsm.AddState(
            States.Disabled,
            onEnter: (_) =>
            {
                _darkCover.SetActive(true);
                // TODO: disable interactions
            },
            onLogic: (_) =>
            {
                // show cd progress
                if (_ability.CD_Timer > 0)
                {
                    _cdVisual.HandleCoolDownChange(_ability.CD_Timer, _ability.Total_CD_Timer);
                }
            },
            onExit: (_) =>
            {
                _darkCover.SetActive(false);
                _cdVisual.Hide();
            }
        );

        _fsm.AddState(States.Ghost, new State<States>(isGhostState: true));

        _fsm.AddTransition(States.Lv0, States.Ghost, (_) => _ability.CurrentLevel >= 1);

        // Intermediate ghost state
        _fsm.AddTransition(States.Ghost, States.Ready, (_) => _ability.CanPerform);
        _fsm.AddTransition(States.Ghost, States.Disabled, (_) => !_ability.CanPerform);

        _fsm.AddTransition(States.Ready, States.Performing, (_) => _ability.Performing);
        _fsm.AddTransition(States.Performing, States.Disabled, (_) => !_ability.Performing);
        _fsm.AddTransition(States.Disabled, States.Ghost);

        _fsm.SetStartState(States.Lv0);
        _fsm.Init();
    }

    public override void Update()
    {
        base.Update();
        _fsm.OnLogic();
    }

    protected override Color GetBackgroundColor()
    {
        if (_fsm.ActiveState.name == States.Disabled)
        {
            return _cancelColor;
        }
        else
        {
            return base.GetBackgroundColor();
        }
    }

    public void HandleLevelChange(int current, int max)
    {
        if (_abilityLevelMat == null)
        {
            _abilityLevel.material = Instantiate(_abilityLevel.material);
            _abilityLevelMat = _abilityLevel.material;
        }
        _abilityLevelMat.SetFloat(ABILITY_LEVEL, current);
        _abilityLevelMat.SetFloat(ABILITY_LEVEL_NUM_LEVELS, max);
    }
}
