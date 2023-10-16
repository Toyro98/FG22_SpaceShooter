using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _throttlePower = 5.0f;
        [SerializeField] private float _rotationPower = 5.0f;

        [Header("Shooting")]
        [SerializeField] private float _fireRate = 0.1f;
        [SerializeField] private Projectile _projectilePrefab;

        private Rigidbody2D _rigidbody2D;
        private float _lastTimeFired = 0.0f;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Movement();
            Shooting();
        }

        private void Movement()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody2D.AddForce(transform.up * _throttlePower, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rigidbody2D.AddTorque(_rotationPower, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody2D.AddTorque(-_rotationPower, ForceMode2D.Force);
            }
        }

        private void Shooting()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }

            if (Time.timeSinceLevelLoad - _lastTimeFired < _fireRate)
            {
                return;
            }

            _lastTimeFired = Time.timeSinceLevelLoad;
            Instantiate(_projectilePrefab, transform.position, transform.localRotation);
        }
    }
}