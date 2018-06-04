using EasyHealthSystem.BarsFactory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem.Example
{
    public class BarOperator : MonoBehaviour
    {
        public Bar barSource;
        public bool followTarget; 
        public RectTransform parentRectTransformSource;
        public BarsAssetsData barsAssetsData;

        [SerializeField] BarRectTransformData positionData;
        public bool updateRectData;

        UnityAction<float> onBarValueChanged = delegate { };
        IHealthUpdate target;

        Bar bar;
        Bar parent;

        Bar TargetBar
        {
            get
            {
                if (bar == null)
                    bar = CreateBar();
                return bar;
            }
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

        float maxHealth;
        void HealthUpdated(float health, float maxHealth)
        {
            if (Mathf.Approximately(this.maxHealth, maxHealth) == false)
            {
                this.maxHealth = maxHealth;
                InitBar();
            }

            onBarValueChanged(health);
        }

        void LateUpdate()
        {
            if (updateRectData && positionData != null)
                TargetBar.SetupData(positionData);
            
            if (followTarget)
                FollowTarget();
        }

        void FollowTarget()
        {
            if (bar == null)
                return;

            var offset = Vector3.zero;
            if (positionData != null)
                offset = positionData.anchoredPosition;

            var position = transform.position + offset;

            if (bar.GetComponentInParent<Canvas>().renderMode == RenderMode.WorldSpace)
                bar.transform.position = position;
            else
            {
                Vector3 barPos = Camera.main.WorldToScreenPoint(position);
                bar.transform.position = barPos;
            }
        }

        [UsedImplicitly]
        public void InitBar()
        {
            TargetBar.Init(maxHealth, ref onBarValueChanged, Color.green);
            if (positionData != null)
                TargetBar.SetupData(positionData);
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
