using UnityEngine;

public class RythimButton : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Transform TargetSpawner;

    [SerializeField] private RythimManager rc;

    public string ActivateKeyA;

    public string ActivateKeyB;


    void Start()
    {   
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(ActivateKeyA)) PressedButton(true);

        if (Input.GetKeyDown(ActivateKeyB)) PressedButton(false);
    }

    public void PressedButton(bool A)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - new Vector3(1f, 0f, 0f), transform.TransformDirection(Vector3.right), out hit))
        {
            TargetControl Target = hit.transform.gameObject.GetComponent<TargetControl>();

            Target.HandleRemoval(A);
        }
    }
}