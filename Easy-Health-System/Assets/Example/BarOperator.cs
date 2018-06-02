using Hypnagogia.Bar.Code;
using Hypnagogia.Bar.Code.BarsFactory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Hypnagogia.Example
{
    public class BarOperator : MonoBehaviour
    {
        [SerializeField] MovingBar bar;
        [SerializeField] BarsAssetsData barsAssetsData;

        UnityAction<float> onBarValueChanged;
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
                bar = CreateMovingBar();
            bar.Init(transform, ref onBarValueChanged, maxHealth, Color.green);
        }

        Canvas parentCanvas;
        MovingBar CreateMovingBar()
        {
            if (parentCanvas == null)
                parentCanvas = Instantiate(barsAssetsData.screenSpaceCanvasPrefab);

            return Instantiate(barsAssetsData.barPrefab, parentCanvas.transform) as MovingBar;
        }

        public void UpdateValuePerLine(int valuePerLine)
        {
            bar.UpdateValuePerLine(valuePerLine);
        }
    }
}
