using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace FPS
{
    public class StaminaAmountController : MonoBehaviour, IStaminaAmountController
    {
        [SerializeField] private RSE_OnEmptyStamina _onEmptyStamina;
        [SerializeField] private RSE_OnFullStamina _onFullStamina;
        [SerializeField] [Min(0)] private float _staminaTotalDurationSeconds;
        [SerializeField] [Min(0)] private float _minStaminaToEnableSeconds;
        [SerializeField] private Slider _staminaSlider;

        private Coroutine _consumeStaminaCoroutine;
        private Coroutine _gainStaminaCoroutine;

        public void ConsumeStamina()
        {
            if (_gainStaminaCoroutine != null)
            {
                StopCoroutine(_gainStaminaCoroutine);
                _gainStaminaCoroutine = null;
            }

            float staminaToNullDuration =
                _staminaTotalDurationSeconds * _staminaSlider.value;
            _consumeStaminaCoroutine =
                StartCoroutine(ControlStamina(
                    _staminaSlider.minValue, 
                    staminaToNullDuration, 
                    _onEmptyStamina));
        }

        public void GainStamina()
        {
            if (_consumeStaminaCoroutine != null)
            {
                StopCoroutine(_consumeStaminaCoroutine);
                _consumeStaminaCoroutine = null;
            }

            float staminaToFullDuration =
                _staminaTotalDurationSeconds - (_staminaTotalDurationSeconds * _staminaSlider.value);
            _gainStaminaCoroutine = StartCoroutine(ControlStamina(
                _staminaSlider.maxValue,
                staminaToFullDuration, 
                _onFullStamina));
        }

        public bool IsConsumingStamina()
        {
            return _consumeStaminaCoroutine != null;
        }

        public bool IsGainingStamina()
        {
            return _gainStaminaCoroutine != null;
        }

        public bool HasStamina()
        {
            return _staminaSlider.value >= _minStaminaToEnableSeconds;
        }

        private IEnumerator ControlStamina(float lerpTo, float lerpDuration, EventWrapper callback)
        {
            float staminaStartValue = _staminaSlider.value;
            float startTime = Time.time;

            while (Time.time < startTime + lerpDuration)
            {
                float delta = (Time.time - startTime) / lerpDuration;
                float interpolationValue = Mathf.Lerp(staminaStartValue, lerpTo, delta);
                _staminaSlider.value = interpolationValue;
                yield return null;
            }

            _staminaSlider.value = lerpTo;
            callback.Raise();
        }
    }
}
