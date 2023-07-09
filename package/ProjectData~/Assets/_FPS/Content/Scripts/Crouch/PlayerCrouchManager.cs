using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(ICrouchResponse))]
    public class PlayerCrouchManager : MonoBehaviour
    {
        private ICrouchResponse _crouchResponse;

        private void Awake()
        {
            _crouchResponse = GetComponent<ICrouchResponse>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _crouchResponse.Crouch();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _crouchResponse.Stand();
            }
        }
    }
}
