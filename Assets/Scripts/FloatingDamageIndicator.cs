using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DotaHeroes
{
    public class FloatingDamageIndicator : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text damageText;
        [SerializeField]
        private Camera mainCamera;

        private void Awake()
        {
            if(damageText == null)
            {
                damageText = this.GetComponent<TMP_Text>();
            }
            if(mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
        }

        private void Start()
        {
            Destroy(this.gameObject, 1f);
        }

        public void ShowDamageIndicator(float damage, DamageType type)
        {
            switch (type)
            {
                case DamageType.Normal:
                    damageText.text = damage.ToString("F0");
                    return;
                case DamageType.Critical:
                    damageText.fontSize += 5;
                    damageText.color = Color.red;
                    damageText.text = damage.ToString("F0");
                    return;
                case DamageType.Heal:
                    damageText.fontSize += 2;
                    damageText.color = Color.green;
                    damageText.text = damage.ToString("F0");
                    return;                    
            }                      
        }

        private void Update()
        {
            this.transform.rotation = mainCamera.transform.rotation;
            this.transform.Translate(Vector3.up * 1f * Time.deltaTime);
            damageText.CrossFadeAlpha(0, 1f, false);
        }
    }
}
