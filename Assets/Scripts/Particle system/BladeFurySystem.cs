using UnityEngine;
using System.Collections.Generic;

namespace DotaHeroes
{
    [System.Serializable]
    public class BladeFurySystem : MonoBehaviour
    {
        public List<EnemyController> list;

        void Update()
        {
            this.transform.Rotate(Vector3.forward * 5);            
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherEC = other.GetComponent<EnemyController>();
            if (!list.Contains(otherEC) && otherEC != null)
            {
                list.Add(otherEC);
            }
                               
        }

        private void OnTriggerExit(Collider other)
        {
            var otherEC = other.GetComponent<EnemyController>();
            if (list.Contains(otherEC))
            {
                list.Remove(otherEC);
            }
        
        }
        private void OnDisable()
        {
            list.Clear();
        }
    }
}

