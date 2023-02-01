public interface StateMachineState
{
    void OnEnterState(params object[] objects);
    void ExecuteState();
    void OnExitState();

    void AddTransition(BaseStateMachineState state);

    BaseStateMachineState GetTransition<T>() where T : BaseStateMachineState;
}
