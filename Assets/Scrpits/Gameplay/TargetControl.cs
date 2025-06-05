using UnityEngine;

public class TargetControl : MonoBehaviour
{
    public bool needsA = false;
    public bool needsB = false;

    private Rigidbody rb;
    private RythimManager rc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rc = this.gameObject.GetComponentInParent<RythimManager>();

        rb.linearVelocity = transform.right * -rc.targetvel;
    }

    void Update()
    {
        rb.linearVelocity = transform.right * -rc.targetvel;
        if (transform.position.x < -12f) HandleRemoval();
    }

    public void HandleRemoval()
    {
        float accuracy = Mathf.Abs(rb.transform.position.x + 6f);

        if (accuracy < rc.GreatDist) 
        {
            rc.points += 5;
        }
        else if (accuracy < rc.GoodDist)
        {
            rc.points += 3;
        }
        else if (accuracy < rc.OkDist)
        {
            rc.points += 1;
        }
        else if (accuracy < rc.BadDist)
        {
            rc.points += 0;
        }
        else 
        {
            rc.points -= 3;
        }

        rc.UpdatePointText();

        DestroyImmediate(this.gameObject);
    }
}
