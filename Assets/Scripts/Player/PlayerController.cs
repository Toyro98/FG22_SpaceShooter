using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        const float SLOWDOWNFACTOR = 1.005f;

        [SerializeField] float _throttlePower = 5.0f;
        [SerializeField] float _rotationPower = 5.0f;

        Rigidbody2D _rigidbody2D;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody2D.AddForce(transform.up * _throttlePower, ForceMode2D.Force);
            }
            else
            {
                // Gradually slow down if no force is applied
                _rigidbody2D.velocity /= SLOWDOWNFACTOR;
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
    }
}