using EasyHealthSystem.BarsFactory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem.Example
{
    public class BarOperator : MonoBehaviour
    {
        [SerializeField] MovingBar bar;
        [SerializeField] BarsAssetsData barsAssetsData;

        UnityAction<float> onBarValueChanged = delegate { };
        IHealthUpdate target;

        void OnValidate()
        {
            if (GetComponent<IHealthUpdate>() == null)
                Debug.LogErrorFormat(this, "Missing IHealthUpdate");
        }

        void Start()
        {
            target = GetComponent<IHealthUpdate>();
            target.HealthUpdated += HealthUpdated;

            InitBar();
        }

        float maxHealth = 0;
        void HealthUpdated(float health, float maxHealth)
        {
            if (Mathf.Approximately(this.maxHealth, maxHealth) == false)
            {
                this.maxHealth = maxHealth;
                bar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
            }

            onBarValueChanged(health);
        }

        [UsedImplicitly]
        public void InitBar()
        {
            if (bar == null)
                bar = BarsFactory.BarsFactory.CreateMovingBar(barsAssetsData);
            bar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
        }

        public void UpdateValuePerLine(int valuePerLine)
        {
            bar.UpdateValuePerLine(valuePerLine);
        }
    }
}
