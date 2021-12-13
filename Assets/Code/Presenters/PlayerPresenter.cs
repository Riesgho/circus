using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly ISubject<Unit> _actionActivated;
        private bool _isGrounded;

        public PlayerPresenter(PlayerView view, ISubject<Unit> actionActivated)
        {
            _view = view;
            _view.IsGrounded = SetGrounded;
            _actionActivated = actionActivated;
            _actionActivated.Subscribe(_=>Jump());
        }

        private  void Jump()
        {
            if(_isGrounded) _view.Jump();
        }

        private void SetGrounded(bool isGrounded)
        {
            _isGrounded = isGrounded;
        }
    }
}