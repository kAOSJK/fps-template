using UnityEngine;

namespace FPS
{
    [RequireComponent(
        typeof(IStaminaAmountController),
        typeof(ISprintResponse))]
    public class PlayerSprintManager : MonoBehaviour
    {
        [SerializeField] private RSE_OnEmptyStamina _onEmptyStamina;

        private IStaminaAmountController _staminaAmountController;
        private ISprintResponse _staminaResponse;

        private void Awake()
        {
            _staminaAmountController = GetComponent<IStaminaAmountController>();
            _staminaResponse = GetComponent<ISprintResponse>();
        }

        private void OnEnable()
        {
            _onEmptyStamina.Event += DispatchDisableSprint;
        }

        private void OnDisable()
        {
            _onEmptyStamina.Event -= DispatchDisableSprint;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _staminaAmountController.HasStamina())
            {
                DisptachEnableSprint();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && _staminaResponse.IsSprintEnabled())
            {
                DispatchDisableSprint();
            }
        }

        private void DisptachEnableSprint()
        {
            _staminaResponse.EnableSprint();

            if (!_staminaAmountController.IsConsumingStamina())
            {
                _staminaAmountController.ConsumeStamina();
            }
        }

        private void DispatchDisableSprint()
        {
            _staminaResponse.DisableSprint();

            if (!_staminaAmountController.IsGainingStamina())
            {
                _staminaAmountController.GainStamina();
            }
        }
    }
}
