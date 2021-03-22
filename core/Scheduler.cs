using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace lastHope.core {
    public class Scheduler : MonoBehaviour
    {
        ChangeAction action;

        public void StartAction(ChangeAction sentAction)
        {
            if (action == sentAction) return;
            if (action != null)
                action.CancelAction();
            action = sentAction;
        }
        public void CancelAction()
        {
            StartAction(null);
        }
    }
}

