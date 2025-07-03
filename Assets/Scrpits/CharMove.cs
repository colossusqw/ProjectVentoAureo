using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float desiredSpeed = 0f;
    public float xBorder = 0f;
    public bool toRight = false;

    private Vector3 initialpos;

    void Awake()
    {
        initialpos = transform.position;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * desiredSpeed * 2f * Time.deltaTime;

        if (toRight)
        {
            if(transform.position.x > xBorder) transform.position = initialpos;
        }
        else 
        {
            if(transform.position.x < xBorder) transform.position = initialpos;
        }

    }
}
