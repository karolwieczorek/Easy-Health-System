using UnityEngine;

namespace EasyHealthSystem.Example
{
    public class HitTarget : MonoBehaviour, IHitTarget
    {
        [SerializeField] Health health;

        public void OnHit(float value)
        {
            health.UpdateHealth(value);
        }
    }

    public interface IHitTarget
    {
        void OnHit(float value);
    }
}