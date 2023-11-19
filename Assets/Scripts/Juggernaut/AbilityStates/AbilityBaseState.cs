using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes
{
    public abstract class AbilityBaseState
    {
        public virtual void EnterState(MyCharacterController controller)
        {

        }
        public virtual void UpdateState(MyCharacterController controller)
        {

        }
        public virtual void ExitState(MyCharacterController controller)
        {

        }
    }
}
