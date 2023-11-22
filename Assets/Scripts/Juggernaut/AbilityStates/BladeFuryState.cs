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
        [SerializeField]
        private float bladeFuryDamage;
        [SerializeField]
        private BladeFurySystem bladefurySystem;

        public override void EnterState(MyCharacterController ctx)
        {
            startTime = Time.time;
            bladefurySystem.gameObject.SetActive(true);
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
            bladefurySystem.gameObject.SetActive(false);
            controller.currentAbilityState = null;
        }

        private void BladeFuryDamage()
        {
            float damage = bladeFuryDamage * Time.deltaTime;
            foreach(EnemyController enemyController in bladefurySystem.list)
            {
                enemyController.TakeDamage(damage, DamageType.Normal);
            }          
        }

        private void RotateAvatar(Transform avatar)
        {
            avatar.transform.Rotate(Vector3.up, 10f);

            
        }

    }
}
