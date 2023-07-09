 using UnityEngine;

namespace FPS
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPosition;

        private void Update()
        {
            transform.position = _cameraPosition.position;
        }
    }
}
