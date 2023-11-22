using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DotaHeroes
{
    public class FloatingUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject indicatorPrefab;
        private FloatingDamageIndicator indicator;

        public void ShowFloatingIndicator(float damage, DamageType type)
        {
            if (damage < 10f)
                return;
            var go =Instantiate(indicatorPrefab, this.transform);
            indicator = go.GetComponent<FloatingDamageIndicator>();
            indicator.ShowDamageIndicator(damage, type);
        }
    }
}
