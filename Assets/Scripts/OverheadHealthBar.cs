using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DotaHeroes
{
    public class OverheadHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Camera mainCamera;

        private void Awake()
        {
            if(slider == null)
            {
                slider = this.GetComponent<Slider>();
            }
            if(mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            slider.value = currentHealth / maxHealth;
        }

        private void Update()
        {
            this.transform.rotation = mainCamera.transform.rotation;
        }
    }
}
