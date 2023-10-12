using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float _throttlePower = 5.0f;
        [SerializeField] float _rotationPower = 5.0f;

        [Header("Shooting")]
        [SerializeField] float _fireRate = 0.2f;
        [SerializeField] Projectile _projectilePrefab;

        Rigidbody2D _rigidbody2D;
        float _lastTimeFired = 0.0f;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Movement();
            Shooting();
        }

        void Movement()
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

        void Shooting()
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