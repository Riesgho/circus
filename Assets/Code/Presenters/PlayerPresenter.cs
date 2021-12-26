using System.Collections.Generic;
using Code.Views;
using UniRx;

namespace Code.Presenters
{
    public class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly ISubject<float> _actionActivated;
        private readonly GamePlayView _gamePlayView;
        private bool _isGrounded;

        public PlayerPresenter(PlayerView view, ISubject<float> actionActivated, GamePlayView gamePlayView)
        {
            _view = view;
            _view.IsGrounded = SetGrounded;
            _actionActivated = actionActivated;
            _gamePlayView = gamePlayView;
            _gamePlayView.MovePlayer = Move;
            _actionActivated.Subscribe(Jump);
        }

        private void Move(float amount)
        {
            _view.Move(amount);
        }

        private  void Jump(float power)
        {
            if(_isGrounded) _view.Jump(power);
        }

        private void SetGrounded(bool isGrounded)
        {
            _isGrounded = isGrounded;
        }

        public void Dismiss()
        {
            _view.Reset();
        }
    }
}