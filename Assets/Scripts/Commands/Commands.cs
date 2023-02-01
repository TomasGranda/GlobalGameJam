using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Commands
{
    private HashSet<Command> commands = new HashSet<Command>();

    public Commands() { }

    public void AddCommand(Command command)
    {
        commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (Command command in commands)
        {
            if (command.ShouldExecute())
            {
                command.Execute();
            } else {
                command.Reset();
            }
        }
    }
}
