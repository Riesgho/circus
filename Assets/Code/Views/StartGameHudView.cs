using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class StartGameHudView : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject startGameHud;       
        public Action StartGame { get; set; }

        public void Initialize()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame.Invoke);
        }

        public void Hide()
        {
            startGameHud.SetActive(false);
        }
    }
}
