using System.Linq;
using EasyHealthSystem.BarsFactory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem.Example
{
    public class BarOperator : MonoBehaviour
    {
        public Bar barPrefab;
        public BarsAssetsData barsAssetsData;

        UnityAction<float> onBarValueChanged = delegate { };
        IHealthUpdate target;
        MovingBar bar;

        void OnValidate()
        {
            if (GetComponent<IHealthUpdate>() == null)
                Debug.LogErrorFormat(this, "Missing IHealthUpdate");
        }

        void Start()
        {
            InitBar();
            target = GetComponent<IHealthUpdate>();
            target.HealthUpdated += HealthUpdated;
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
                bar = BarsFactory.BarsFactory.CreateMovingBar(barsAssetsData, GetBar());
            bar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
        }

        Bar GetBar()
        {
            return barsAssetsData.barsPrefabs.First();
        }

        public void UpdateValuePerLine(int valuePerLine)
        {
            bar.UpdateValuePerLine(valuePerLine);
        }
    }
}
