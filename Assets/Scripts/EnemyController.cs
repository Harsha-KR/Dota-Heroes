using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DotaHeroes
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float currentHealth;
        [SerializeField]
        private float maxHealth;
        private OverheadHealthBar overHeadHealthBar;
        private FloatingUIManager damageIndicator;

        private void Awake()
        {
            overHeadHealthBar = GetComponentInChildren<OverheadHealthBar>();
            damageIndicator = GetComponentInChildren<FloatingUIManager>();
        }
        void Start()
        {
            currentHealth = maxHealth;
            overHeadHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        public void TakeDamage(float damage, DamageType type)
        {
            currentHealth -= damage;
            damageIndicator.ShowFloatingIndicator(damage, type);
            if(currentHealth <= 0)
            {
                Debug.Log("enemy died");
            }
            overHeadHealthBar.UpdateHealthBar(currentHealth, maxHealth);
            
        }

        public void Heal(float heal)
        {
            if(currentHealth < maxHealth)
            {
                currentHealth += heal;
                if(currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }

            overHeadHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
