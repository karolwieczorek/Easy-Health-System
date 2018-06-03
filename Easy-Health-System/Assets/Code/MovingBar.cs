using UnityEngine;
using UnityEngine.Events;

namespace EasyHealthSystem 
{
    public class MovingBar : Bar 
    {
        Transform target;

        [SerializeField] Vector3 offset = new Vector3(0, 1.5f, 0);
 
        /// int
        public void Init(Transform target, ref UnityAction<int> onValueChange, int maxHealth, Color? color = null)
        {
            base.Init(maxHealth, ref onValueChange, color);
            this.target = target;
        }

        
        /// float
        public void Init(Transform target, ref UnityAction<float> onValueChange, float maxHealth, Color? color = null)
        {
            base.Init(maxHealth, ref onValueChange, color);
            this.target = target;
        }

        void Update()
        {
            if (target == null)
                return;
            
            Vector3 barPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = barPos;
        }
    }
}
