using UnityEngine;

namespace FPS
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        [SerializeField] private Vector3 _boxSize;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _orientation;
        [SerializeField] private RSO_GroundHit _groundHit;

        private void FixedUpdate()
        {
            Physics.BoxCast(transform.position, _boxSize, -_orientation.up, out RaycastHit hitInfo, _orientation.rotation, _maxDistance, _ground);

            if (!_groundHit.Value.Equals(hitInfo))
            {
                _groundHit.Value = hitInfo;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position - _orientation.up * _maxDistance, _boxSize);
        }
    }
}
