using Hypnagogia.Bar.Code;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hypnagogia.Example {
    public class OperateBar : MonoBehaviour {

        UnityAction<float> onBarValueChanged;
        float barValue = 100;
        float maxBarValue = 100;
        float BarValue{
            get {return barValue;}
            set {
                barValue = Mathf.Clamp(value, 0, maxBarValue);
                if (onBarValueChanged != null)
                    onBarValueChanged(barValue);
            }
        }

        [SerializeField]
        MovingBar bar;

        [SerializeField]
        Transform target;

        [SerializeField]InputField maxValueInput; 
        [SerializeField]InputField valuePerLineInput;

        [SerializeField]InputField damageInput;
        [SerializeField]InputField healInput;

        void Start() {
            InitUI();
            InitBar();
        }

        [UsedImplicitly]
        public void InitBar()
        {
            InitValues();
            bar.Init(target, ref onBarValueChanged, maxBarValue, Color.green);
        }

        void InitValues()
        {
            float maxValue = 200f;
            float.TryParse(maxValueInput.text, out maxValue);
            Debug.LogFormat("text: {0}, value: {1}", maxValueInput.text, maxValue);
            maxBarValue = maxValue;
        }

        void InitUI()
        {
            damageInput.text = 10.ToString();
            healInput.text = 10.ToString();
            maxValueInput.text = maxBarValue.ToString();
            valuePerLineInput.text = 50f.ToString();
        }

        [UsedImplicitly]
        public void Damage() {
            int damage;
            if (int.TryParse(damageInput.text, out damage))
                BarValue -= damage;
        }

        [UsedImplicitly]
        public void Heal() {
            int heal;
            if (int.TryParse(healInput.text, out heal))
                BarValue += heal;
        }
    }
}
