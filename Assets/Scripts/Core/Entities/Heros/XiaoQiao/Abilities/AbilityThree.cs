using Core.Game.Combat;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    public class AbilityThree : AbilityBase
    {
        public override string Name => "星华缭乱";

        public override Character.Ability Type => Character.Ability.Three;

        public override int ManaCost => throw new System.NotImplementedException();

        protected override Awaitable PerformAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
