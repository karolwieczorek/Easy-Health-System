using EasyHealthSystem.BarsFactory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem.Example
{
    public class BarOperator : MonoBehaviour
    {
        public Bar barSource;
        public RectTransform parentRectTransformSource;
        public BarsAssetsData barsAssetsData;

        UnityAction<float> onBarValueChanged = delegate { };
        IHealthUpdate target;
        
        MovingBar bar;
        RectTransform parent;
        
        MovingBar TargetBar
        {
            get
            {
                if (bar == null)
                    bar = CreateBar();
                return bar;
            }
            set { bar = value; }
        }

        void OnValidate()
        {
            if (GetComponent<IHealthUpdate>() == null)
                Debug.LogErrorFormat(this, "Missing IHealthUpdate");
        }

        public bool IsBarPrefab()
        {
            return barSource.gameObject.IsPrefab();
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
                TargetBar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
            }

            onBarValueChanged(health);
        }

        [UsedImplicitly]
        public void InitBar()
        {
            TargetBar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
        }

        MovingBar CreateBar()
        {
            if (barSource == null)
                return null;

            return BarsFactory.BarsFactory.CreateMovingBar(parentRectTransformSource, barSource);
        }

        public void UpdateValuePerLine(int valuePerLine)
        {
            TargetBar.UpdateValuePerLine(valuePerLine);
        }
    }
}
