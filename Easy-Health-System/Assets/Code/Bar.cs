using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EasyHealthSystem
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] Image barImage;
        [SerializeField] BarLines barLines;
        [SerializeField] float valuePerLine = 100;

        float currentValue;
        float maxValue;


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

        public void Destroy()
        {
            Destroy(gameObject);
        }

        void Init(float maxValue, Color? color)
        {
            currentValue = maxValue;
            this.maxValue = maxValue;

            barLines.UpdateBar(valuePerLine / maxValue);

            if (color.HasValue)
                barImage.color = color.Value;

            ResizeBar();
        }

        public void UpdateValuePerLine(float newValuePerLine)
        {
            valuePerLine = newValuePerLine;
            barLines.UpdateBar(valuePerLine / maxValue);
        }

        void ResizeBar()
        {
            barImage.fillAmount = currentValue / maxValue;
        }

        void UpdateValue(int value)
        {
            UpdateValue((float) value);
        }

        void UpdateValue(float value)
        {
            Debug.LogFormat("update value: {0}", value);

            this.currentValue = value;
            ResizeBar();
        }
        
        public void SetupData(BarRectTransformData positionData)
        {
            positionData.SetTransform(GetComponent<RectTransform>());
            if (positionData.Color.HasValue)
            barImage.color = positionData.Color.Value;
        }
    }
}