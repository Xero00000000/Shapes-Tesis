using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    //public InputReader input;
    public Camera cam;

    TargetingStrategy currentStrategy;

    private void Update()
    {
        if (currentStrategy != null && currentStrategy.IsTargetting)
        {
            currentStrategy.Update();
        }
    }
}
