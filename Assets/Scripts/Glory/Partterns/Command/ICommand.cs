

public interface ICommand
{
    void Execute();
    void UpdateLogic();
    void Cancel();

    bool IsFinished();
}
