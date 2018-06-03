using UnityEngine;

namespace EasyHealthSystem.Example
{
    public class MouseHitter : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hitTarget = GetHitTarget();
                if (hitTarget != null)
                    hitTarget.OnHit(-20);
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                var hitTarget = GetHitTarget();
                if (hitTarget != null)
                    hitTarget.OnHit(20);
            }
        }

        static IHitTarget GetHitTarget()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                IHitTarget hitTarget = hit.collider.GetComponent<IHitTarget>();
                return hitTarget;
            }

            return null;
        }
    }
}