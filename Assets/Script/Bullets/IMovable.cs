public interface IMovable
{
    public void SlowDown(float slowDownFactor);
    public void ResumeSpeed(float slowDownFactor);
    public MoveState GetState();
}

public enum MoveState
{
    Normal, Slow
}
