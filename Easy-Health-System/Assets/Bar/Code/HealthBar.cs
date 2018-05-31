using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hypnagogia.Bar.Code {

    // DOIT StaticBar / DynamicBar
    public class HealthBar : MonoBehaviour {
        [SerializeField] Image barImage;
        [SerializeField] BarLines barLines;

        float currentValue;
        float maxValue;

        [SerializeField]
        Vector3 offset = new Vector3(0, 1.5f, 0);

        [SerializeField]
        float valuePerLine = 100;

        Transform target;

        public void Init(Transform target, ref UnityAction<int> onValueChange, int maxHealth, Color? color = null)
        {
            Init(target, maxHealth, color);
            onValueChange += UpdateHealth;
        }

        public void Init(Transform target, ref UnityAction<float> onValueChange, float maxHealth, Color? color = null)
        {
            Init(target, maxHealth, color);
            onValueChange += UpdateHealth;
        }

        void Init(Transform target, float maxHealth, Color? color) 
        {
            this.currentValue = maxHealth;
            this.maxValue = maxHealth;
            this.target = target;

            barLines.UpdateBar(valuePerLine / (float) maxHealth);
            
            if (color.HasValue)
                barImage.color = color.Value;

            ResizeBar();
        }

        void ResizeBar()
        {
            barImage.fillAmount = currentValue / (float)maxValue;
        }

        void UpdateHealth(int health)
        {
            Debug.Log("update health");
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            this.currentValue = health;
            ResizeBar();
        }

        void UpdateHealth(float health) 
        {
            this.currentValue = health;
            ResizeBar();
        }

        void Update()
        {
            Vector3 barPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = barPos;
        }
    }
}
