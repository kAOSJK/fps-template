using UnityEngine;

namespace FPS
{
    public class PlayerSprintResponse : MonoBehaviour, ISprintResponse
    {
        [SerializeField] [Min(1)] private float _sprintSpeedMultiplicator;
        [SerializeField] private RSE_MultiplyMoveSpeed _multiplyMoveSpeed;

        private bool _enabledStamina;

        public void EnableSprint()
        {
            _multiplyMoveSpeed.Raise(_sprintSpeedMultiplicator);
            _enabledStamina = true;
        }

        public void DisableSprint()
        {
            _multiplyMoveSpeed.Raise(1 / _sprintSpeedMultiplicator);
            _enabledStamina = false;
        }

        public bool IsSprintEnabled()
        {
            return _enabledStamina;
        }

        public float GetSprintMultiplicator()
        {
            float multiplicator = 1.0f;

            if (_enabledStamina)
            {
                multiplicator = _sprintSpeedMultiplicator;
            }

            return multiplicator;
        }
    }
}
