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

        public override void EnterState(MyCharacterController controller)
        {
            controller.agent.isStopped = false;
            //Debug.Log("Entered Attack State");
        }
        public override void UpdateState(MyCharacterController controller)
        {
            if(controller.target != null)
            {
                if (Vector3.Distance(controller.target.position, controller.transform.position) >= controller.attackRange)
                {
                    controller.agent.isStopped = false;
                    controller.agent.SetDestination(controller.target.position);
                }
                else
                {
                    
                    controller.agent.isStopped = true;
                    
                    attackRate = baseAttackRate / (1 + (attackRateModifierAbility + attackRateModifierItem) / 100);
                    Debug.Log(attackRate);
                    if ((Time.time - lastAttackTime) >= attackRate)
                    {
                        lastAttackTime = Time.time;
                        Debug.Log("attack performed");
                    }

                }
            }
            else
            {
                //Debug.Log("Target dead?, Returning to idle state");
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
    }
}
