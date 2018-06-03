using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem
{
    public class Health : MonoBehaviour, IHealthUpdate
    {
        public event UnityAction<float, float> healthUpdated = delegate { };
        public event UnityAction<float, float> HealthUpdated
        {
            add
            {
                value(healthValue, maxHealthValue);
                healthUpdated += value;
            }
            remove { healthUpdated -= value; }
        }
        
        float healthValue = 100;
        float maxHealthValue = 100;
        
        float HealthValue{
            get {return healthValue;}
            set {
                healthValue = Mathf.Clamp(value, 0, maxHealthValue);
                healthUpdated(healthValue, maxHealthValue);
            }
        }

        [UsedImplicitly]
        public void UpdateHealth(float value) 
        {
            HealthValue += value;
        }

        [UsedImplicitly]
        public void UpdateMaxHealth(float value) 
        {
            maxHealthValue += value;
            healthUpdated(healthValue, maxHealthValue);
        }
        
        [UsedImplicitly]
        public void SetMaxHealth(float maxHealth) 
        {
            maxHealthValue = maxHealth;
            healthUpdated(healthValue, maxHealthValue);
        }

        [ContextMenu("Heal 10 health")]
        public void Heal() 
        {
            UpdateHealth(10);
        }
        
        [ContextMenu("Damage 10 health")]
        public void DoDamage() 
        {
            UpdateHealth(-10);
        }

        [ContextMenu("Increase health")]
        public void AddMaxHealth() {
            UpdateMaxHealth(10);
        }
    }
}