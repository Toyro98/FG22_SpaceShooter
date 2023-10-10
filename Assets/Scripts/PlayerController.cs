using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _throttlePower = 6.0f;
        [SerializeField] float _rotationPower = 4.0f;

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