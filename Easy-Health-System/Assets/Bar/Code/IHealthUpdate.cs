using UnityEngine.Events;

namespace Hypnagogia.Bar.Code {
    public interface IHealthUpdate {
        event UnityAction<float, float> HealthUpdated;
    }
}