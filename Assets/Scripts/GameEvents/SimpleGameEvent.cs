using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used for GameEvents where data need not be passed
/// 
/// What it does:
/// Creates a new method (no arguments)
/// Calls Raise using Void argument
/// Automatically creates new Void data
/// </summary>
public class Void { } // Dummy class/Concrete Subclass as Unity's inspector cannot serialize or display open generic classes
[CreateAssetMenu(fileName = "SimpleGameEvent", menuName = "Scriptable Objects/SimpleGameEvent")]
public class SimpleGameEvent : GameEvent<Void>
{
    // Creates a new method (no arguments)
    // Calls Raise using Void argument
    // Automatically creates new Void data
    public void Raise() => Raise(new Void());
}
