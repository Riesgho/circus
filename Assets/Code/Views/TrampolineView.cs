using System;
using UnityEngine;

namespace Code.Views
{
    public class TrampolineView : MonoBehaviour, Actionable
    {
        public void Execute()
        {
           
        }
    }

    public interface Actionable
    {
        void Execute();
    }
}