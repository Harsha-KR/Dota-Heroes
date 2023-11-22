using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


namespace DotaHeroes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MyCharacterController : MonoBehaviour
    {
        public BaseState currentState;
        //public BaseState previousState;
        public IdleState idleState = new IdleState();
        public AttackState attackState = new AttackState();
        public MoveState moveState = new MoveState();

        public AbilityBaseState currentAbilityState;
        public BladeFuryState bladeFuryState = new BladeFuryState();

        public float autoAttackRange;
        //public SphereCollider autoAttackRangeTrigger;
        public List<GameObject> autoAttackEnemiesInRange = new List<GameObject>();

        public Vector3 targetPosition;
        public EnemyController target;

        public float attackRange;

        public NavMeshAgent agent;

        private float currentHealth;
        [SerializeField]
        private float maxHealth;

        private OverheadHealthBar healthBar;

        public Transform playerAvatar;
        [SerializeField]
        private TMP_Text floatingDamage;

        private void Awake()
        {
            if(healthBar == null)
            {
                healthBar = GetComponentInChildren<OverheadHealthBar>();
            }
            agent = this.GetComponent<NavMeshAgent>();
            /*autoAttackRangeTrigger = this.gameObject.AddComponent<SphereCollider>();
            autoAttackRangeTrigger.isTrigger = true;
            autoAttackRangeTrigger.radius = autoAttackRange;*/
        }

        void Start()
        {
            currentHealth = maxHealth;
            currentState = idleState;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            currentState.EnterState(this);            
        }

        void Update()
        {
            currentState.UpdateState(this);

            if (currentAbilityState != null)
            {
                currentAbilityState.UpdateState(this);
            }
            

            if (Input.GetKeyDown(KeyCode.A))
            {
                currentAbilityState = bladeFuryState;
                currentAbilityState.EnterState(this);
            }
        }
        public void ExitState(MyCharacterController controller, BaseState nextState)
        {
            controller.currentState = nextState;
            controller.currentState.EnterState(controller);
        }
        private void OnTriggerStay(Collider other)
        {

        }

        public void Heal(float heal)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += heal;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }

            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                //implement dead state;
            }
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
