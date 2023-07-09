namespace FPS
{
    public interface ISprintResponse
    {
        void EnableSprint();
        void DisableSprint();
        bool IsSprintEnabled();
        float GetSprintMultiplicator();
    }
}
