using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProvider
{
     struct InputBase
     {
          public Vector2 BaseVector;
     }
     InputBase GetState();
}
