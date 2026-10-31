using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// RuntimeSet for storing data at runtime
/// Example Usecase: Adding all enemies to this runtime set, so that it may be referenced when a GameEvent such as 'GameRestart' is raised, to re-enable all dead enemies
/// Modelled from Ryan Hipple's Unite2017 Sets
/// </summary>
/// <typeparam name="T"></typeparam>
public class RuntimeSet<T> : ScriptableObject
{
    public List<T> activeItems = new List<T>(); // Storing data of currently active items
    public List<T> allItems = new List<T>(); // Stores data of all items

    // For scene changes to reinitialize the lists
    public void Initialize()
    {
        activeItems.Clear();
        allItems.Clear();
    }

    // Add item to items list
    public void AddToRuntimeSet(T item)
    {
        if (!activeItems.Contains(item)) activeItems.Add(item);
        if (!allItems.Contains(item)) allItems.Add(item);
    }

    // Remove item from Active Item list
    public void RemoveFromRuntimeSet(T item)
    {
        if (activeItems.Contains(item)) activeItems.Remove(item);
    }
}
