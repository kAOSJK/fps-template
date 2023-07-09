using UnityEngine;

namespace Utility
{
    public static class VectorUtil
    {
        public static float Distance(Vector3 posA, Vector3 posB) => (posA - posB).sqrMagnitude;
        
        public static float Distance(Transform trA, Transform trB) => (trA.position - trB.position).sqrMagnitude;
        
        public static T GetClosest<T>(Transform from, T[] others) where T : Component
        {
            T closest = others[0];
            float distance = Distance(from, closest.transform);

            for (int i = 1; i < others.Length; i++)
            {
                float distanceTemp = Distance(from, others[i].transform);

                if (distanceTemp < distance)
                {
                    closest = others[i];
                    distance = distanceTemp;
                }
            }

            return closest;
        }
    }
}
