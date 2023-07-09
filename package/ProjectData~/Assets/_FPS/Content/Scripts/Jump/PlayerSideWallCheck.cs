using UnityEngine;

namespace FPS
{
    public class PlayerSideWallCheck : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _wall;
        [SerializeField] private Transform _orientation;

        public bool CheckLeft(out Vector3 normal)
        {
            return Check(-_orientation.right, out normal);
        }

        public bool CheckRight(out Vector3 normal)
        {
            return Check(_orientation.right, out normal);
        }

        private bool Check(Vector3 direction, out Vector3 normal)
        {
            bool result = Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, _maxDistance, _wall);
            normal = result ? hitInfo.normal : Vector3.zero;
            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -_orientation.right * _maxDistance);
            Gizmos.DrawLine(transform.position, transform.position + _orientation.right * _maxDistance);
        }
    }
}
