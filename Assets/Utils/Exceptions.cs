using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PropertyHasAlreadyBeenInitializedException : Exception
{
    public PropertyHasAlreadyBeenInitializedException(string methodName) : 
        base($"{methodName} has already been initialized"){ } 
}

public class PropertyHasNotBeenInitializedException : Exception
{
    public PropertyHasNotBeenInitializedException(string methodName) : 
        base($"{methodName} has not been initialized"){ }
        
}
