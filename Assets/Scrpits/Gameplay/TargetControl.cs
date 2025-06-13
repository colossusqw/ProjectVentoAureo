using UnityEngine;

public class TargetControl : MonoBehaviour
{
    public bool needsA = false;
    public bool needsB = false;

    public bool Destroyed = false;

    private int index = 0;
    private int subIndex = 0;

    private Rigidbody rb;
    private RythimManager rc;
    private Material mat;
    private PerformanceFeedbackController Client;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        mat= this.gameObject.GetComponent<MeshRenderer>().material;
        rc = this.gameObject.GetComponentInParent<RythimManager>();
        Client = FindFirstObjectByType<PerformanceFeedbackController>();

        index = rc.currentIndex;
        subIndex = rc.currentIndex;

        if (index > rc.LevelLength) 
        {
            if (Destroyed) return;
            Destroyed = true;
            DestroyImmediate(this.gameObject);

            rc.playing = false;
            return;
        }

        SetupNeeds();

        rb.linearVelocity = transform.right * -rc.targetvel;
    }

    void Update()
    {
        if(Destroyed) return;
        rb.linearVelocity = transform.right * -rc.targetvel;
        if (transform.position.x < -12f) HandleRemoval(false, true);
    }

    public void SetupNeeds()
    {
        if(Destroyed) return;

        if(rc.LevelList[index].type == 1)
        {
            mat.color = new Color(0f, 0f, 1f);
            needsA = true;
        }
        if(rc.LevelList[index].type == 2)
        {
            mat.color = new Color(1f, 0f, 0f);
            needsB = true;
        }
        if(rc.LevelList[index].type == 3)
        {
            mat.color = new Color(0.8f,0f,0.8f);
            needsA = true;
            needsB = true;
        }

        if (!needsA && !needsB)
        {
            if (Destroyed) return;
            Destroyed = true;
            Destroy(this.gameObject);
        }
    }

    public void HandleRemoval(bool A = false, bool full = false)
    {
        float accuracy = Mathf.Abs(rb.transform.position.x + 6f);

        if(Destroyed) return;

        if((A && !needsA) || (!A && !needsB))
        {
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points -= 3;
        }
        else if (accuracy < rc.GreatDist) 
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

        if (A && needsA) 
        {
            needsA = false;
            mat.color = new Color(1f, 0f, 0f);
        }
        
        if (!A && needsB)
        {
            needsB = false;
            mat.color = new Color(0f, 0f, 1f);
        }

        rc.UpdatePointText();

        if (full)
        {
            if (Destroyed) return;
            Destroyed = true;
            DestroyImmediate(this.gameObject);
        }
        
        if(!needsA && !needsB)
        {
            if (Destroyed) return;
            Destroyed = true;
            DestroyImmediate(this.gameObject);
        }
    }
}
