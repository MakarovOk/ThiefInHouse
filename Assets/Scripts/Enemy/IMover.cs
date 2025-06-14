namespace Enemy
{
    public interface IMover
    {
        void HandleStateMoving(bool value);
        void ResetPos();
    }
}