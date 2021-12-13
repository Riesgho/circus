using System;
using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class GamePlay
    {
        private readonly GamePlayView _view;
        private readonly ISubject<Unit> _gameStarted;

        public GamePlay(GamePlayView view, ISubject<Unit> gameStarted)
        {
            _view = view;
            _gameStarted = gameStarted;
            _gameStarted.Subscribe(_ => _view.Initialize());
            _view.GamePlayStart = GamePlayStart;
        }
        
        public void GamePlayStart(PlayerInput playerInput, PlayerView playerView)
        {
            var actionActivated = new Subject<Unit>();
            var playerInputPresenter = new PlayerInputPresenter(playerInput, actionActivated);
            var playerPresenter = new PlayerPresenter(playerView, actionActivated);
        }
    }
}