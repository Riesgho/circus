using System;
using UnityEngine;

namespace Code.Views
{
    public class PlayerInput : MonoBehaviour
    {
        public Action Action { get; set; }
        private void Update()
        {
            if (Input.GetButtonDown("Action"))
            {
                Action();
            }
        }
    }
}