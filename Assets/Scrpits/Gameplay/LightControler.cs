using UnityEngine;

public class LightControler : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos = new Vector3();

    private Light objLight;

    void Start()
    {
        startPos = transform.position;
        rb = this.gameObject.GetComponent<Rigidbody>();
        objLight = this.gameObject.GetComponent<Light>();
    }


    void FixedUpdate()
    {
        if (transform.position.z < -30f) 
        {
            rb.linearVelocity = new Vector3(0f,0f,0f);
            transform.position = startPos;
        }
    }

    public void StartFeedback(int level)
    {
        if (level < 1) return;

        if (level < 2) objLight.color = new Color(1f, 1f, 1f, 1f);
        else if (level < 3) objLight.color = new Color(.5f, 1.5f, .6f, 1f);
        else objLight.color = new Color(2f, 1.3f, 0f, 1f);

        transform.position = startPos;
        rb.linearVelocity = new Vector3(0f,0f, -60f);
    }
}
