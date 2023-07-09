using DG.Tweening;
using UnityEngine;

namespace FPS
{
    public class PlayerCrouchResponse : MonoBehaviour, ICrouchResponse
    {
        [SerializeField] private float _crouchYScale;
        [SerializeField] private float _scaleDuration;
        [SerializeField][Min(0)] private float _crouchSpeedMultiplicator;
        [SerializeField] private RSE_MultiplyMoveSpeed _multiplyMoveSpeed;

        private float _defaultYScale;

        private void Start()
        {
            _defaultYScale = transform.localScale.x;
        }

        public void Crouch()
        {
            transform.DOScaleY(_crouchYScale, _scaleDuration);
            _multiplyMoveSpeed.Raise(_crouchSpeedMultiplicator);
        }

        public void Stand()
        {
            transform.DOScaleY(_defaultYScale, _scaleDuration);
            _multiplyMoveSpeed.Raise(1 / _crouchSpeedMultiplicator);
        }
    }
}
