using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem
{
    public class ReflectionHealthUpdate : MonoBehaviour, IHealthUpdate
    {
        public event UnityAction<float, float> HealthUpdated = delegate {};

        [SerializeField] Object healthObject;
        [SerializeField] string healthFieldName;
        [SerializeField] Object maxHealthObject;
        [SerializeField] string maxHealthFieldName;

        FieldInfo healthMemberInfo;
        FieldInfo maxHealthMemberInfo;

        void Awake()
        {
            healthMemberInfo = healthObject.GetType().GetField(healthFieldName);
            maxHealthMemberInfo = maxHealthObject.GetType().GetField(maxHealthFieldName);
        }

        void Update()
        {
            var healthValue = healthMemberInfo.GetValue(healthObject);
            var maxHealthValue = maxHealthMemberInfo.GetValue(maxHealthObject);
            
            float health = healthValue as float? ?? 0;
            float maxHealth = maxHealthValue as float? ?? 0;
            
            HealthUpdated(health, maxHealth);
        }
    }
}