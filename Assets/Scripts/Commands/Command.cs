using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    bool ShouldExecute();

    void Execute();

    void Reset();
}
