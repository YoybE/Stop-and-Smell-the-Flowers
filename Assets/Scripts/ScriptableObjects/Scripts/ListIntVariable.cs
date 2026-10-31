using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ListIntVariable", menuName = "Scriptable Objects/ListIntVariable")]
public class ListIntVariable : Variable<List<int>>
{
    public List<int> previousList;

    public void Initialize()
    {
        for (int idx = 0; idx < Value.Count; idx++)
        {
            ApplyChange(0, idx);
        }
    }

    public override void SetValue(List<int> value)
    {
        if (value != previousList) previousList = value;

        // Updates protected value
        _value = value;
    }

    // Overrides 
    public void SetValue(ListIntVariable list)
    {
        SetValue(list.Value);
    }

    public void ApplyChange(int value, int idx)
    {
        this.Value[idx] = value;
    }
}
