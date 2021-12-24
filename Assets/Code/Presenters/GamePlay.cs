using System;
using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class GamePlay
    {
        private readonly GamePlayView _view;
        private readonly ISubject<Unit> _gameStarted;
        private readonly ISubject<Unit> _gameFinished;
        private PlayerPresenter _playerPresenter;
        private PlayerInputPresenter _playerInputPresenter;

        public GamePlay(GamePlayView view, ISubject<Unit> gameStarted, ISubject<Unit> gameFinished)
        {
            _view = view;
            _gameStarted = gameStarted;
            _gameStarted.Subscribe(_ => _view.Initialize());
            _gameFinished = gameFinished;
            _view.GamePlayStart = GamePlayStart;
            _view.GamePlayFinish = GamePlayFinish;
        }

        private void GamePlayFinish()
        {
            _playerPresenter.Dismiss();
            _playerInputPresenter.Dismiss();
            _gameFinished.OnNext(Unit.Default);
        }

        public void GamePlayStart(PlayerInput playerInput, PlayerView playerView)
        {
            var actionActivated = new Subject<Unit>();
            _playerInputPresenter = new PlayerInputPresenter(playerInput, actionActivated);
            _playerPresenter = new PlayerPresenter(playerView, actionActivated);
        }
    }
}