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

        private void Awake()
        {
            overHeadHealthBar = GetComponentInChildren<OverheadHealthBar>();
        }

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            overHeadHealthBar.UpdateHealthBar(currentHealth, maxHealth);
            //Destroy(this.gameObject, 5);

        }

        // Update is called once per frame
        void Update()
        {
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
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
