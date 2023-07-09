using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _airMoveSpeedMultiplier;
        [SerializeField] private float _moveMaxMagnitude;
        [SerializeField] private float _verticalMaxVelocity;
        [SerializeField] private RSO_GroundHit _groundHit;
        [SerializeField] private RSE_MultiplyMoveSpeed _multiplyMoveSpeed;

        private Rigidbody _rb;

        private Vector3 _moveDirection;
        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
        }

        private void OnEnable()
        {
            _multiplyMoveSpeed.Event += MultiplyMoveSpeed;
        }

        private void OnDisable()
        {
            _multiplyMoveSpeed.Event -= MultiplyMoveSpeed;
        }

        private void Update()
        {
            GetMoveInput();
            CalculateMoveDirection();
            ClampVelocity();
        }

        private void FixedUpdate()
        {
            if (_groundHit.Value.transform)
            {
                float angle = Vector3.Angle(Vector3.up, _groundHit.Value.normal);

                if (angle != 0)
                {
                    _moveDirection = Vector3.ProjectOnPlane(_moveDirection, _groundHit.Value.normal);
                    _rb.useGravity = false;
                }
                else
                {
                    _rb.useGravity = true;
                }
            }
            else
            {
                _rb.useGravity = true;
            }

            Vector3 force = _moveDirection.normalized * _moveSpeed * 10;

            if (!_groundHit.Value.transform)
            {
                force *= _airMoveSpeedMultiplier;
            }

            _rb.AddForce(force, ForceMode.Force);
        }

        private void ClampVelocity()
        {
            Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            flatVelocity = Vector3.ClampMagnitude(flatVelocity, _moveMaxMagnitude);

            if (_rb.velocity.y > _verticalMaxVelocity)
            {
                flatVelocity.y = _verticalMaxVelocity;
            }
            else
            {
                flatVelocity.y = _rb.velocity.y;
            }

            _rb.velocity = flatVelocity;
        }

        private void CalculateMoveDirection()
        {
            _moveDirection = _orientation.forward * _vertical + _orientation.right * _horizontal;
        }

        private void GetMoveInput()
        {
            _horizontal = Time.deltaTime * Input.GetAxisRaw("Horizontal");
            _vertical = Time.deltaTime * Input.GetAxisRaw("Vertical");
        }

        private void MultiplyMoveSpeed(float moveSpeedMultiplier)
        {
            _moveSpeed *= moveSpeedMultiplier;
            _moveMaxMagnitude *= moveSpeedMultiplier;
        }
    }
}
