using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lastHope.combat {
    public class Punch : MonoBehaviour
    {
        [SerializeField] AimScheduler aimScheduler;

        private void Start()
        {
            aimScheduler.Punching(false);
        }
        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0) return;
            if (!aimScheduler.ToAim())
            {
               
            }

        }
        void StopPunching()
        {
            aimScheduler.Punching(false);
        }
    }

}

