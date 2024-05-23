using System.Diagnostics;

namespace Core.Game
{
    public abstract class StatModifier
    {
        public bool MarkedForRemove;

        protected float effect;
        protected StatMediator _mediator;

        public void OnAdd(StatMediator mediator)
        {
            _mediator = mediator;
            OnAdd();
        }

        public virtual void OnAdd() { }

        public virtual void OnUpdate(float deltaTime) { }

        public virtual void OnRemove() { }

        public virtual float GetEffect() => effect;
    }

    public class AddTenPercentModifier : StatModifier
    {
        public override void OnUpdate(float deltaTime)
        {
            effect = _mediator.Stat * 0.1f;
        }
    }

    public class AddFiveHundredModifier : StatModifier
    {
        public override void OnAdd()
        {
            effect = 500f;
        }
    }
}
