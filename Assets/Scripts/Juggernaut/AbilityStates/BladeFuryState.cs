using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes
{
    [System.Serializable]
    public class BladeFuryState : AbilityBaseState
    {
        float startTime;
        [SerializeField]
        private float bladeFuryDuration;
        private MyCharacterController controller;
        [SerializeField]
        private float bladeFuryDamage;

        public override void EnterState(MyCharacterController ctx)
        {
            startTime = Time.time;
            controller = ctx;
        }
        public override void UpdateState(MyCharacterController controller)
        {
            RotateAvatar(controller.playerAvatar);
            if (Time.time >= (startTime + bladeFuryDuration) && controller.playerAvatar.rotation == controller.transform.rotation)
            {
                ExitState(controller);
            }
            BladeFuryDamage();
        }

        public override void ExitState(MyCharacterController controller)
        {
            controller.currentAbilityState = null;
        }

        private void BladeFuryDamage()
        {
            if (controller.target != null)
            {
                controller.target.TakeDamage(bladeFuryDamage * Time.deltaTime);
            }
        }

        private void RotateAvatar(Transform avatar)
        {
            avatar.transform.Rotate(Vector3.up, 10f);
        }
    }
}
