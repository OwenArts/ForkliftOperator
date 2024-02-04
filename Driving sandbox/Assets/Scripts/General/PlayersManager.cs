using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.General
{
    public class PlayersManager : MonoBehaviour
    {
        public static List<GameObject> Players { get; private set; }

        private void Start()
        {
            Players = GameObject.FindGameObjectsWithTag("Player").ToList();
        }

        private void FixedUpdate()
        {
            if (!Players.Equals(GameObject.FindGameObjectsWithTag("Player").ToList()))
                Players = GameObject.FindGameObjectsWithTag("Player").ToList();
        }
    }
}