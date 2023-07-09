using UnityEngine;

namespace FPS
{
    [RequireComponent(
        typeof(Rigidbody),
        typeof(PlayerSideWallCheck))]
    public class PlayerWallJump : MonoBehaviour
    {
        [SerializeField] private float _wallJumpForce;
        [SerializeField] private Transform _orientation;
        [SerializeField] private RSO_GroundHit _groundHit;
        [SerializeField] private RSO_CanJump _canJump;

        private Rigidbody _rb;
        private PlayerSideWallCheck _wallCheck;

        private bool _wallJumpInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _wallCheck = GetComponent<PlayerSideWallCheck>();
        }

        private void Update()
        {
            if (_groundHit.Value.transform == null)
            {
                GetJumpInput();
            }
        }

        private void FixedUpdate()
        {
            if (_groundHit.Value.transform != null)
            {
                _wallJumpInput = false;
                return;
            }

            if (_canJump.Value)
            {
                _wallJumpInput = false;
                return;
            }

            if (!CheckWalls(out Vector3 normal))
            {
                _wallJumpInput = false;
                return;
            }

            if (_wallJumpInput)
            {
                Jump(normal);
            }

            _wallJumpInput = false;
        }

        private bool CheckWalls(out Vector3 normal)
        {
            bool isCloseLeftWall = _wallCheck.CheckLeft(out Vector3 leftNormal);
            bool isCloseRightWall = _wallCheck.CheckRight(out Vector3 rightNormal);

            normal = Vector3.zero;

            if (isCloseLeftWall && isCloseRightWall)
            {
                return false;
            }

            if (!isCloseLeftWall && !isCloseRightWall)
            {
                return false;
            }

            normal = isCloseLeftWall ? leftNormal : rightNormal;
            return true;
        }

        private void Jump(Vector3 normal)
        {
            Vector3 wallJumpDirection = _orientation.up + normal;
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(wallJumpDirection * _wallJumpForce, ForceMode.Impulse);
        }

        private void GetJumpInput()
        {
            if (!_wallJumpInput)
            {
                _wallJumpInput = Input.GetKeyDown(KeyCode.Space);
            }
        }
    }
}
