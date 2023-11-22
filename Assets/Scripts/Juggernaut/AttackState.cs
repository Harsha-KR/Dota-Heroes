using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes
{
    [System.Serializable]
    public class AttackState : BaseState
    {   
        [SerializeField]
        private float baseAttackRate;
        private float attackRate;
        private float lastAttackTime;
        [SerializeField]
        private float attackRateModifierAbility;
        [SerializeField]
        private float attackRateModifierItem;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private int criticalHitChance;
        [SerializeField]
        float baseDamage;
        [SerializeField]
        private float criticalDamagePercent;

        private MyCharacterController controller;

        public override void EnterState(MyCharacterController ctr)
        {            
            if (!controller)
            {
                controller = ctr;
            }
            controller.agent.isStopped = false;
            //Debug.Log("Entered Attack State");
        }

        public override void UpdateState(MyCharacterController controller)
        {
            if(controller.target != null)
            {
                if (Vector3.Distance(controller.target.transform.position, controller.transform.position) >= controller.attackRange)
                {
                    controller.agent.isStopped = false;
                    controller.agent.SetDestination(controller.target.transform.position);
                }
                else
                {
                    RotateTowards();
                    controller.agent.isStopped = true;
                    Attack();
                }
            }
            else
            {
                controller.ExitState(controller, controller.idleState);
            }
            

            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse pressed");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                Physics.Raycast(ray, out hitInfo);

                if (hitInfo.collider.CompareTag("Ground"))
                {
                    controller.targetPosition = hitInfo.point;
                    controller.ExitState(controller, controller.moveState);
                }
            }
        }

        private void Attack()
        {
            attackRate = baseAttackRate / (1 + (attackRateModifierAbility + attackRateModifierItem) / 100);
            if ((Time.time - lastAttackTime) >= attackRate)
            {
                lastAttackTime = Time.time;
                int chance = Random.Range(0, 100);
                if(chance <= criticalHitChance)
                {
                    float damage = baseDamage * (criticalDamagePercent / 100);
                    controller.target.TakeDamage(damage, DamageType.Critical);
                }
                else
                {
                    controller.target.TakeDamage(baseDamage, DamageType.Normal);
                }
            }
        }

        private void RotateTowards()
        {
            Vector3 enemyVector = new Vector3(controller.target.transform.position.x, 0, controller.target.transform.position.z);
            Vector3 characterVector = new Vector3(controller.transform.position.x, 0, controller.transform.position.z);
            Vector3 direction =  enemyVector - characterVector;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, toRotation,Time.deltaTime *rotationSpeed);
            
        }
    }
}
