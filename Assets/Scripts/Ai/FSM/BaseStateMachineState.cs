using System.Collections.Generic;

public abstract class BaseStateMachineState : StateMachineState
{
    List<BaseStateMachineState> statesList = new List<BaseStateMachineState>();

    public abstract void OnEnterState(params object[] objects);
    public abstract void ExecuteState();
    public abstract void OnExitState();

    public void AddTransition(BaseStateMachineState state)
    {
        if (!statesList.Exists((s) => s.GetType() == state.GetType()))
            statesList.Add(state);
    }

    public BaseStateMachineState GetTransition<T>() where T : BaseStateMachineState
    {
        if (statesList.Exists((s) => s is T)) return statesList.Find((s) => s is T);

        return null;
    }
}
