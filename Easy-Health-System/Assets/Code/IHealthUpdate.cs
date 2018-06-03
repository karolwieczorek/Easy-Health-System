using UnityEngine.Events;

namespace EasyHealthSystem {
    public interface IHealthUpdate {
        event UnityAction<float, float> HealthUpdated;
    }
}