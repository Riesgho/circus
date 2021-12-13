using System;
using UnityEngine;

namespace Code.Views
{
    public class GamePlayView : MonoBehaviour
    {
        public Action<PlayerInput,PlayerView> GamePlayStart { get; set; }

        
        [SerializeField] private Transform player;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerView playerView;
        [Range(0.0f, 10f), SerializeField] private float _moveHorizontalValue;
        [Range(0.0f, 1f), SerializeField] private float offset;
        private float newPlayerPositionXAxis ;

        private void Update()
        {
            var position = player.position;
            newPlayerPositionXAxis = position.x + _moveHorizontalValue * Time.deltaTime;
        }

    

        private void FixedUpdate()
        {
            player.position = new Vector2(newPlayerPositionXAxis, player.position.y);
        }

        public void Initialize()
        {
            gameObject.SetActive(true);
            newPlayerPositionXAxis = player.position.x;
            GamePlayStart(playerInput, playerView);
        }
    }
}