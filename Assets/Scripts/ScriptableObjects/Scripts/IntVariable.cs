using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Objects/IntVariable")]
public class IntVariable : Variable<int>
{
    public int previousHighestValue;
    public override void SetValue(int value)
    {
        // Updates the value if higher than currently stored value
        if (value > previousHighestValue) previousHighestValue = value;

        // Updates protected value
        _value = value;
    }

    // Overrides 
    public void SetValue(IntVariable integer)
    {
        SetValue(integer.Value);
    }

    public void ApplyChange(int amount)
    {
        this.Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        ApplyChange(amount.Value);
    }

    // Resets back to 0
    public void ResetHighestValue()
    {
        previousHighestValue = 0;
    }
}
