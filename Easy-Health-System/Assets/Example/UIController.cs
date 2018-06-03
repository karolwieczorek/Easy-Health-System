using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace EasyHealthSystem.Example
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] BarOperator barOperator;
        
        [SerializeField] InputField maxHealthValueInput;
        [SerializeField] InputField valuePerLineInput;

        [SerializeField] InputField damageInput;
        [SerializeField] InputField healInput;

        void Start()
        {
            InitUI();
        }

        [UsedImplicitly]
        public void InitValues()
        {
            float maxHealthValue = 200f;
            if (float.TryParse(maxHealthValueInput.text, out maxHealthValue))
            {
                Debug.LogFormat("text: {0}, value: {1}", maxHealthValueInput.text, maxHealthValue);
                health.SetMaxHealth(maxHealthValue);
            }
        }
        
        void InitUI()
        {
            damageInput.text = 10.ToString();
            healInput.text = 10.ToString();
            maxHealthValueInput.text = 50.ToString();
            valuePerLineInput.text = 50f.ToString();
        }

        [UsedImplicitly]
        public void Damage()
        {
            int damage;
            if (int.TryParse(damageInput.text, out damage))
                health.UpdateHealth(-damage);
        }

        [UsedImplicitly]
        public void Heal()
        {
            int heal;
            if (int.TryParse(healInput.text, out heal))
                health.UpdateHealth(heal);
        }

        [UsedImplicitly]
        public void UpdateLines()
        {
            int valuePerLine;
            if (int.TryParse(valuePerLineInput.text, out valuePerLine))
                barOperator.UpdateValuePerLine(valuePerLine);
        }
    }
}