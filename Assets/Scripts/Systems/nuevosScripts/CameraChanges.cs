using UnityEngine;

public class CameraChanges : MonoBehaviour
{
    public CameraFollow Camera;
    public float xValueIn;
    public float yValueIn;
    public float zValueIn;
    public float xValueOut;
    public float yValueOut;
    public float zValueOut;

    public void Start()
    {
        xValueOut = Camera.positionOffset.x;
        yValueOut = Camera.positionOffset.y;
        zValueOut = Camera.positionOffset.z;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.positionOffset.x = xValueIn;
            Camera.positionOffset.y = yValueIn;
            Camera.positionOffset.z = zValueIn;
            Camera.combatAarea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.positionOffset.x = xValueOut;
            Camera.positionOffset.y = yValueOut;
            Camera.positionOffset.z = zValueOut;
            Camera.combatAarea = false;
        }
    }
}
