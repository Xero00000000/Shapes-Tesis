using UnityEngine;
using UnityEngine.InputSystem;

public class PadreMove : MonoBehaviour
{
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Input.GetAxis("Horizontal") * Vector3.right) * speed * Time.deltaTime;

        transform.position += Input.GetAxis("Vertical") * Vector3.forward * speed * Time.deltaTime;


    }

    public void OnMove()
    {

    }

}
