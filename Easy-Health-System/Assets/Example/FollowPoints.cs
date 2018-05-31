using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hypnagogia.Example {
    public class FollowPoints : MonoBehaviour {

        [SerializeField] Transform target;
        [SerializeField] float speed = 1f;

        List<Transform> points;

        Transform nextPoint;

        void Awake() {
            points = GetComponentsInChildren<Transform>().ToList();
            nextPoint = points[0];
        }

        void Update() {
            if (points.Count < 2)
                return; 

            Move();
            if (target.position == nextPoint.position)
                nextPoint = GetNextPoint();
        } 

        private void Move()
        {
            target.position = Vector3.MoveTowards(target.position, nextPoint.position, speed * Time.deltaTime);
        }

        private Transform GetNextPoint()
        {
            var index = points.IndexOf(nextPoint);
            var nextIndex = index+1;
            if (nextIndex == points.Count)
                return points[0];
            return points[nextIndex];
        }
    }
}
