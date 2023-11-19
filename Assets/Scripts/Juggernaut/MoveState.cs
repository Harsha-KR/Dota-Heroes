using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes
{
    public class MoveState : BaseState
    {
        public override void EnterState(MyCharacterController controller)
        {
            controller.agent.isStopped = false;
            controller.agent.SetDestination(controller.targetPosition);
            Debug.Log("entered move state");
        }

        public override void UpdateState(MyCharacterController controller)
        {
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse pressed");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                Physics.Raycast(ray, out hitInfo);

                if (hitInfo.collider.CompareTag("Ground"))
                {
                    controller.targetPosition = hitInfo.point;
                    controller.agent.SetDestination(controller.targetPosition);
                }
                else if (hitInfo.collider.CompareTag("Enemy"))
                {
                    controller.target = hitInfo.collider.transform;
                    controller.ExitState(controller, controller.attackState);
                }
                
            }
            if (controller.agent.remainingDistance <= controller.agent.stoppingDistance)
            {
                controller.ExitState(controller, controller.idleState);
            }
        }
    }
}
