using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class PlayerInputPresenter
    {
        private readonly PlayerInput _view;
        private readonly ISubject<Unit> _actionActivated;

        public PlayerInputPresenter(PlayerInput view, ISubject<Unit> actionActivated)
        {
            _view = view;
            _actionActivated = actionActivated;
            _view.Action = () => { _actionActivated.OnNext(Unit.Default); };
        }
    }
}