namespace FPS
{
    public interface IStaminaAmountController
    {
        void ConsumeStamina();
        void GainStamina();
        bool IsConsumingStamina();
        bool IsGainingStamina();
        bool HasStamina();
    }
}
