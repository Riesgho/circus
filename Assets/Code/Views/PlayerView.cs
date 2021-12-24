using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private Vector2 jumpForce;
        [SerializeField] private Vector3 startPosition;
        public Action<bool> IsGrounded { get; set; }
        public void Jump()
        {
            body.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private void CheckGrounded(Vector3 position)
        {
            var hits = new List<RaycastHit2D>
            {
                Physics2D.Raycast(new Vector2(position.x - 0.5f, position.y), Vector2.down, 1, 1 << 9),
                Physics2D.Raycast(new Vector2(position.x, position.y), Vector2.down, 1, 1 << 9),
                Physics2D.Raycast(new Vector2(position.x + 0.5f, position.y), Vector2.down, 1, 1 << 9)
            };
            hits.ForEach(hit => Debug.DrawRay(hit.centroid, Vector3.down));
            IsGrounded(hits.Any(hit => hit.collider && hit.distance < 0.52f));
        }

        private void Update()
        {
            CheckGrounded(transform.position);
        }

        public void Reset()
        {
            transform.position = startPosition;
        }
    }
}