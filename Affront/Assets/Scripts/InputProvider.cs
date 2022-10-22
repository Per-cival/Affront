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
        //state list: base state -> dash chain -> event chain -> null
        return DashChain(inputBase);
    }

    private IInputProvider.InputBase DashChain(IInputProvider.InputBase inputBase)
    {
        return inputBase;
    }

    public void Release() //call to release BaseVector back to constructed original. Used to mark end of chain.
    {
        inputBase.BaseVector = Vector2.right;
    }

    public void Zero()
    {
        inputBase.BaseVector = Vector2.zero;
    }
    
}
