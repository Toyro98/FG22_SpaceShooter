using UnityEngine;

namespace SpaceShooter
{
    public sealed class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping = 0.75f;

        private Vector2 _velocity = Vector2.zero;

        private void LateUpdate()
        {
            transform.position = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _damping);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
