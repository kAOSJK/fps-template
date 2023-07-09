using UnityEngine;

namespace FPS
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _sensitivity;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            GetLookInput(out float mouseX, out float mouseY);
            CalculateAngle(mouseX, mouseY);
            Rotate();
        }

        private void Rotate()
        {
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }

        private void CalculateAngle(float mouseX, float mouseY)
        {
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            _yRotation += mouseX;
        }

        private void GetLookInput(out float mouseX, out float mouseY)
        {
            mouseX = _sensitivity * Time.deltaTime * Input.GetAxisRaw("Mouse X");
            mouseY = _sensitivity * Time.deltaTime * Input.GetAxisRaw("Mouse Y");
        }
    }
}
