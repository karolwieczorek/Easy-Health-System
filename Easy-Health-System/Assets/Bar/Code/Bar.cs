using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hypnagogia.Bar.Code {
    public class Bar : MonoBehaviour 
    {
            [SerializeField] Image barImage;
            [SerializeField] BarLines barLines;

            float currentValue;
            float maxValue;

            [SerializeField]
            float valuePerLine = 100;

            public void Init(int maxValue, ref UnityAction<int> onValueChange, Color? color = null)
            {
                Init(maxValue, color);
                onValueChange += UpdateValue;
            }

            public void Init(float maxValue, ref UnityAction<float> onValueChange, Color? color = null)
            {
                Init(maxValue, color);
                onValueChange += UpdateValue;
            }

            void Init(float maxValue, Color? color) 
            {
                this.currentValue = maxValue;
                this.maxValue = maxValue;

                barLines.UpdateBar(valuePerLine / (float) maxValue);
                
                if (color.HasValue)
                    barImage.color = color.Value;

                ResizeBar();
            }

            void ResizeBar()
            {
                barImage.fillAmount = currentValue / (float)maxValue;
            }

            void UpdateValue(int value)
            {
                UpdateValue((float)value);
            }

            void UpdateValue(float value) 
            {
                Debug.LogFormat("update value: {0}", value);
                if (value <= 0)
                {
                    Destroy(gameObject);
                }
                this.currentValue = value;
                ResizeBar();
            }
    }
}