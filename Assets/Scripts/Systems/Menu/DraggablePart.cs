using UnityEngine;

public class DraggablePart : DraggableItem
{
    [SerializeField] private GameEvent partSwap;

    [SerializeField] ClassData theClass;
    [SerializeField] int partValue;

    internal ClassData TheClass => theClass;
    internal int PartValue => partValue;

    public void SwappingPart()
    {
        partSwap.Raise(partValue, theClass);
    }
}