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
        private float _newPlayerPositionXAxis;

        public void Start()
        {
            Reset();
        }

        public Action<bool> IsGrounded { get; set; }

        public void Jump(float powerX, float powerY)
        {
            body.AddForce(new Vector2(jumpForce.x * powerX, jumpForce.y * (1 + powerY)),
                ForceMode2D.Impulse);
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
            var isSteppedOnSomething = hits.Any(hit => hit.collider && hit.distance < 0.52f);
            IsGrounded(isSteppedOnSomething);
            if (!isSteppedOnSomething) return;
            if (ColliderHasActionable(hits))
                hits.First(hit =>hit.collider && hit.collider.GetComponent<Actionable>() != null).collider
                    .GetComponent<Actionable>()
                    .Execute();

        }

        private  bool ColliderHasActionable(IEnumerable<RaycastHit2D> hits) => 
            hits.Any(hit => hit.collider && hit.collider.GetComponent<Actionable>() != null);

        private void Update()
        {
            CheckGrounded(transform.position);
        }

        private void FixedUpdate()
        {
             transform.position = new Vector2(_newPlayerPositionXAxis, transform.position.y);
        }

        public void Reset()
        {
            transform.position = startPosition;
        }

        public void Move(float amount)
        {
            _newPlayerPositionXAxis = transform.position.x + amount;
        }
    }
}