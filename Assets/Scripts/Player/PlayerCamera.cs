using UnityEngine;

namespace SpaceShooter
{
    public sealed class PlayerCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _damping = 0.75f;

        Vector2 _velocity = Vector2.zero;

        void LateUpdate()
        {
            transform.position = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _damping, 5f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
