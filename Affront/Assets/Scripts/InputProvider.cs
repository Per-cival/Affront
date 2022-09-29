using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : IInputProvider
{
    private IInputProvider.InputBase inputBase;
    public InputProvider()
    {
        inputBase = new IInputProvider.InputBase
        {
            BaseVector = Vector2.right
        };
    }

    public IInputProvider.InputBase GetState()
    {
        return inputBase;
    }
    
}
