using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class StartGameHubPresenter
    {
        private readonly StartGameHudView _view;
        private readonly ISubject<Unit> _gameStarted;
        
        public StartGameHubPresenter(StartGameHudView view, ISubject<Unit> gameStarted)
        {
            _view = view;
            _view.StartGame = StartGame;
            _gameStarted = gameStarted;
        }

        public void Initialize()
        {
            _view.Initialize();
        }

        public void StartGame()
        {
            _gameStarted.OnNext(Unit.Default);
            _view.Hide();
        }
    }
}