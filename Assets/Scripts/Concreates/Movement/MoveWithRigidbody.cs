using System.Collections.Generic;
using Cat.Abstracts.Movements;
using UnityEngine;

namespace Cat.Concreates.Movements
{
    public class MoveWithRigidbody : IPlayerMovement
    {
        private Rigidbody2D rb;
        private float speed;
        private float collisionOffset;

        private ContactFilter2D contactFilter;
        List<RaycastHit2D> results;

        public MoveWithRigidbody(Rigidbody2D rb, float speed, ContactFilter2D contactFilter, float collisionOffset)
        {
            this.rb = rb;
            this.speed = speed;
            this.contactFilter = contactFilter;
            this.collisionOffset = collisionOffset;

            results = new List<RaycastHit2D>();
        }

        public void Move(float horizontal, float vertical)
        {
            Vector2 direction = new Vector2(horizontal, vertical);

            if (direction != Vector2.zero)
            {
                bool isMoving = TryMove(direction);
                if(!isMoving)
                {
                    isMoving = TryMove(new Vector2(direction.x, 0));
                    if(!isMoving)
                    {
                        TryMove(new Vector2(0, direction.y));
                    }
                }
            }
        }

        private bool TryMove(Vector2 direction)
        {
            int collisionCount = rb.Cast(direction, contactFilter, results, speed * Time.fixedDeltaTime + collisionOffset);
            if (collisionCount == 0)
            {
                rb.MovePosition(direction * speed * Time.fixedDeltaTime + rb.position);
                return true;
            }
            return false;
        }
    }

}