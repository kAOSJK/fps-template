using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _forceJumpDrag;
        [SerializeField] private float _jumpDrag;
        [SerializeField] private float _jumpBuffer;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private Transform _orientation;
        [SerializeField] private RSO_GroundHit _groundHit;
        [SerializeField] private RSO_CanJump _canJump;

        private Rigidbody _rb;
        private bool _jumpInput;
        private bool _isJumping;
        private float _lastJumpPressed;
        private float _timeLeftGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _groundHit.OnValueChanged += SetTimeLeftGrounded;
        }

        private void OnDisable()
        {
            _groundHit.OnValueChanged -= SetTimeLeftGrounded;
        }

        private void Update()
        {
            GetJumpInput();
        }

        private void FixedUpdate()
        {
            _canJump.Value = CanJump();

            if (_jumpInput && _canJump.Value)
            {
                Jump();
                _jumpInput = false;
                _timeLeftGrounded = 0f;
            }

            if (_jumpInput && _lastJumpPressed + _jumpBuffer < Time.time)
            {
                _jumpInput = false;
            }
        }

        private void GetJumpInput()
        {
            if (!_jumpInput)
            {
                _jumpInput = Input.GetKeyDown(KeyCode.Space);
                _lastJumpPressed = Time.time;
            }
        }

        private void Jump()
        {
            if (_forceJumpDrag)
            {
                _rb.drag = _jumpDrag;
            }

            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(_orientation.up * _jumpForce, ForceMode.Impulse);
            _isJumping = true;
        }

        private bool CanJump()
        {
            return _groundHit.Value.transform ||
                (!_isJumping && _timeLeftGrounded + _coyoteTime > Time.time);
        }

        private void SetTimeLeftGrounded(RaycastHit hitInfo)
        {
            if (hitInfo.transform)
            {
                _isJumping = false;
            }

            _timeLeftGrounded = Time.time;
        }
    }
}
