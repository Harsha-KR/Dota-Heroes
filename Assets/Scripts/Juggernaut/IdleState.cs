using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes
{
    public class IdleState : BaseState
    {
        public override void EnterState(MyCharacterController controller)
        {
            controller.agent.isStopped = true;
            //Debug.Log("entered Idle State");
        }

        public override void UpdateState(MyCharacterController controller)
        {
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse pressed");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                Physics.Raycast(ray, out hitInfo);

                if(hitInfo.collider.CompareTag("Ground"))
                {
                    controller.targetPosition = hitInfo.point;
                    controller.ExitState(controller, controller.moveState);
                }
                else if (hitInfo.collider.CompareTag("Enemy"))
                {
                    controller.target = hitInfo.collider.transform;
                    controller.ExitState(controller, controller.attackState);
                }
            }

        }

        /*public void AutoAttack(MyCharacterController controller, Transform target)
        {
            controller.target = target;
            controller.ExitState(controller, controller.attackState);
        }*/
    }
}
