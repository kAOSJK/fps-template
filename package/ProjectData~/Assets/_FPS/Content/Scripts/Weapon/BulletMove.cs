using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _moveSpeed * Time.deltaTime * transform.forward;
        }
    }
}
