using System;
using UnityEngine;

namespace DefaultNamespace.General
{
    //Does nothing but changing a color on an object when a player is near. If-statement does not work for some reason
    public class CheckDistanceToPlayers : MonoBehaviour
    {
        private readonly Timeout m_distanceTimeout = new Timeout(1f);
        private Renderer m_ObjectRenderer;

        public float TriggerDistance;

        private void Start()
        {
            m_ObjectRenderer = GetComponent<Renderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                m_ObjectRenderer.material.color = Color.green;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                m_ObjectRenderer.material.color = Color.red;
        }
    }
}