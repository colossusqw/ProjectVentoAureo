using UnityEngine;

public class TargetControl : MonoBehaviour
{
    public bool needsA = false;
    public bool needsB = false;

    private Rigidbody rb;
    private RythimManager rc;
    private PerformanceFeedbackController Client;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rc = this.gameObject.GetComponentInParent<RythimManager>();
        Client = FindObjectOfType<PerformanceFeedbackController>();

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
            Client.ShowFeedback(JudgementType.Eccellente);
            rc.points += 5;
        }
        else if (accuracy < rc.GoodDist)
        {
            Client.ShowFeedback(JudgementType.Grande);
            rc.points += 3;
        }
        else if (accuracy < rc.OkDist)
        {
            Client.ShowFeedback(JudgementType.Bene);
            rc.points += 1;
        }
        else if (accuracy < rc.BadDist)
        {
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points += 0;
        }
        else 
        {
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points -= 3;
        }

        rc.UpdatePointText();

        DestroyImmediate(this.gameObject);
    }
}
