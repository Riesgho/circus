using System;
using UnityEngine;

namespace Code.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private Vector2 jumpForce;
        public Action<bool> IsGrounded { get; set; }
        public void Jump()
        {
            body.AddForce(jumpForce, ForceMode2D.Impulse);
        }
        
        private void CheckGrounded(Vector3 position)
        {
            var hit = Physics2D.Raycast(position, Vector2.down, 1, 1 << 9);
            if (hit.collider && hit.distance < 0.52f)
            {
                IsGrounded(true);
            }
            else
                IsGrounded(false);
        }

        private void Update()
        {
            CheckGrounded(transform.position);
        }
    }
}