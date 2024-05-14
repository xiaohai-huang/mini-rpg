using UnityEngine;
using UnityHFSM;

namespace Xiaohai.Character.XiaoQiao
{
    public class Fan : MonoBehaviour
    {
        public float Distance = 10f;
        public float DamageAmount = 200f;
        /// <summary>
        /// Fan flying speed
        /// </summary>
        public float Speed = 2f;

        private Transform _receiver;
        private Vector3 _destination;
        StateMachine sm;
        const float TOLERANCE = 0.5F;

        public void SetReceiver(Transform receiver)
        {
            _receiver = receiver;
        }
        public void Throw()
        {
            // calculate the destination
            _destination = transform.position + transform.forward * Distance;
            sm.RequestStateChange("FlyingForwards");
        }

        void Awake()
        {
            sm = new StateMachine();
            sm.AddState("Idle", onEnter: (_) => { });
            sm.AddState("FlyingForwards", onLogic: (_) =>
            {

                transform.position = Vector3.Lerp(transform.position, _destination, Speed * Time.deltaTime);


                if (Vector3.Distance(transform.position, _destination) < TOLERANCE)
                {
                    sm.RequestStateChange("FlyingBack");
                }
            });

            sm.AddState("FlyingBack", onLogic: (_) =>
            {

                transform.position = Vector3.Lerp(transform.position, _receiver.position, 1.5f * Speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _receiver.position) < TOLERANCE * 3)
                {
                    sm.RequestStateChange("Destroyed");
                }
            });

            sm.AddState("Destroyed", onEnter: (_) =>
            {
                Destroy(gameObject);
            });

            sm.SetStartState("Idle");

            sm.Init();
        }

        // Update is called once per frame
        void Update()
        {
            sm.OnLogic();
        }
    }
}
