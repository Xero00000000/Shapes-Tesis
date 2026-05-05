using UnityEngine;

public class DraggablePart : DraggableItem
{
    [SerializeField] private GameEvent partSwap;

    [SerializeField] ClassData theClass;
    [SerializeField] int partValue;

    /*
    public void SwappingPart()
    {
        partSwap.Raise(this, (partValue, theClass));
    }*/

    public void SwappingPart()
    {
        partSwap.Raise(partValue, theClass);
    }
}