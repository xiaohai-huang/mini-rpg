using UnityEngine;

namespace Xiaohai.Character.MarcoPolo
{
    public class AbilityOneEffect : Effect
    {
        private MarcoPolo _polo;
        private readonly int _numberOfBullets;
        private readonly float _bulletSpawnSpeed;
        private int _numBulletsFired;
        private float _lastFireTime;

        public AbilityOneEffect(int numberOfBullets, float bulletSpawnSpeed)
        {
            Name = "Marco Polo Ability One";
            _numberOfBullets = numberOfBullets;
            _bulletSpawnSpeed = bulletSpawnSpeed;
        }

        public override void OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _polo = system.GetComponent<MarcoPolo>();
        }

        public override void OnUpdate(EffectSystem system)
        {
            base.OnUpdate(system);
            if (_numBulletsFired > _numberOfBullets)
            {
                Finished = true;
                return;
            }

            if (Time.time > _lastFireTime + 1 / _bulletSpawnSpeed)
            {
                _polo.FireBullet();
                _lastFireTime = Time.time;
                _numBulletsFired++;
            }
        }
    }
}
