using UnityEngine;

namespace FPS
{
    public class PlayerDrag : MonoBehaviour
    {
        [SerializeField] private float _groudDrag;
        [SerializeField] private float _airDrag;
        [SerializeField] private RSO_GroundHit _groundHit;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _groundHit.OnValueChanged += SetDrag;
        }

        private void OnDisable()
        {
            _groundHit.OnValueChanged -= SetDrag;
        }

        private void SetDrag(RaycastHit hitInfo)
        {
            if (hitInfo.transform)
            {
                _rb.drag = _groudDrag;
            }
            else
            {
                _rb.drag = _airDrag;
            }
        }
    }
}
