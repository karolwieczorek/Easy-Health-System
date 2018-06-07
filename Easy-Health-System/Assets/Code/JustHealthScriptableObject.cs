using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [CreateAssetMenu(fileName = "JustHealthScriptableObject", menuName = "Custom/JustHealthScriptableObject",
        order = 1)]
    public class JustHealthScriptableObject : ScriptableObject
    {
        public float health = 50;
        public float maxHealth = 100;
    }
}