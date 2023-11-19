
namespace DotaHeroes
{
    public abstract class BaseState
    {
        public virtual void EnterState(MyCharacterController controller)
        {
        }
        public virtual void UpdateState(MyCharacterController controller)
        {
        }
    }
}
