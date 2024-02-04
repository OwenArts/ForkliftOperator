using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class DistanceCalculator
    {
        public static float DistanceBetween(GameObject parent, GameObject other)
        {
            return Mathf.Sqrt((other.transform.position - parent.transform.position).sqrMagnitude);
        }
        public static float DistanceBetween(GameObject parent, Transform otherTransform)
        {
            return Mathf.Sqrt((otherTransform.transform.position - parent.transform.position).sqrMagnitude);
        }
        public static float DistanceBetween(Transform parentTransform, Transform otherTransform)
        {
            return Mathf.Sqrt((otherTransform.transform.position - parentTransform.transform.position).sqrMagnitude);
        }

        public static float ClosestDistance(List<GameObject> objects, Vector3 parentPosition)
        {
            if (objects.Count == 0)
            {
                Debug.LogError("Object list is empty");
                return -1;
            }

            if (objects.Count == 1)
            {
                return Mathf.Sqrt((objects[0].transform.position - parentPosition).sqrMagnitude);
            }

            if (objects.Count >= 2)
            {
                float closestDistance = -1;

                foreach (GameObject gameObject in objects)
                {
                    Vector3 offset = gameObject.transform.position - parentPosition;
                    if (closestDistance.Equals(-1))
                    {
                        closestDistance = offset.sqrMagnitude;
                    }
                    else
                    {
                        if (offset.sqrMagnitude < (closestDistance * closestDistance) || closestDistance == 0)
                        {
                            closestDistance = Mathf.Sqrt(offset.sqrMagnitude);
                        }
                    }
                }
                return closestDistance;
            }

            return -1f;
        }

        public static GameObject ClosestObject(List<GameObject> objects, Vector3 parentPosition)
        {
            if (objects.Count == 0)
            {
                Debug.LogError("Object list is empty");
                return null;
            }

            if (objects.Count == 1)
            {
                return objects[0];
            }

            if (objects.Count >= 2)
            {
                float closestDistance = 0;
                GameObject closestObject = null;

                foreach (GameObject gameObject in objects)
                {
                    Vector3 offset = gameObject.transform.position - parentPosition;
                    if (closestObject == null)
                    {
                        closestDistance = offset.sqrMagnitude;
                        closestObject = gameObject;
                    }
                    else
                    {
                        if (offset.sqrMagnitude < (closestDistance * closestDistance) || closestDistance == 0)
                        {
                            closestDistance = Mathf.Sqrt(offset.sqrMagnitude);
                            closestObject = gameObject;
                        }
                    }
                }
                return closestObject;
            }

            return null;
        }
    }
}