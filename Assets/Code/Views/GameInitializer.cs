using System;
using Code.Presenters;
using UniRx;
using UnityEngine;

namespace Code.Views
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private StartGameHudView _startGameHudView;
        [SerializeField] private GamePlayView gamePlayView;
        private void Start()
        {
            var gameStarted = new Subject<Unit>();
            var startGameHudPresenter = new StartGameHubPresenter(_startGameHudView, gameStarted);
            startGameHudPresenter.Initialize();
            var gamePlayPresenter = new GamePlay(gamePlayView, gameStarted);
        }
    }
}