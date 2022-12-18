using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackMan_Enemy
{
    public class EnemyControllerManager : MonoBehaviour
    {
        [SerializeField]
        private List<EnemyController> controllers;
        void Update()
        {
            for(int i = 0; i < controllers.Count; i++)
            {
                controllers[i].UpdateLoop();
            }
        }
    }
}
