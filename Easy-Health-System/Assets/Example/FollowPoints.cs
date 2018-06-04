﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyHealthSystem.Example 
{
    public class FollowPoints : MonoBehaviour {

        [SerializeField] Transform target;
        [SerializeField] float speed = 1f;
        [SerializeField] HealthBar speed2;

        List<Transform> points;

        Transform nextPoint;

        void Awake() {
            points = GetComponentsInChildren<Transform>().ToList();
            nextPoint = points[0];
        }

        void Update() {
            if (speed2.name == "wow")
                Debug.Log("wow");
            if (points.Count < 2)
                return; 

            Move();
            if (target.position == nextPoint.position)
                nextPoint = GetNextPoint();
        }

        void Move()
        {
            target.position = Vector3.MoveTowards(target.position, nextPoint.position, speed * Time.deltaTime);
        }

        Transform GetNextPoint()
        {
            var index = points.IndexOf(nextPoint);
            var nextIndex = index+1;
            if (nextIndex == points.Count)
                return points[0];
            return points[nextIndex];
        }
    }
}
