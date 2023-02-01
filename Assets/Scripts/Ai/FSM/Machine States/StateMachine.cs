public class StateMachine
{
    public StateMachineState current { get; set; }

    public void Init(BaseStateMachineState init)
    {
        current = init;
        current.OnEnterState();
    }

    public void OnUpdate()
    {
        current.ExecuteState();
    }

    public void Transition<T>(params object[] objects) where T : BaseStateMachineState
    {
        var state = current.GetTransition<T>();

        if (state == null) return;

        current.OnExitState();

        current = state;

        current.OnEnterState(objects);
    }
}
