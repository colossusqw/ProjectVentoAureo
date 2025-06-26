using System.Collections;
using UnityEngine;

public interface IFeedbackReactive
{
    void ReactToFeedback(float scaleMultiplier, float duration);
}

public class TargetControl : MonoBehaviour, IFeedbackReactive
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

    public Texture blueNote;
    public Texture redNote;
    public Texture purpleNote;

    private Vector3 originalScale;
    private Coroutine feedbackRoutine;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        mat = this.gameObject.GetComponent<MeshRenderer>().material;
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

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Destroyed) return;
        rb.linearVelocity = transform.right * -rc.targetvel;
        if (transform.position.x < -12f) HandleRemoval(false, true);
    }

    public void SetupNeeds()
    {
        if (Destroyed) return;

        if (rc.LevelList[index].type == 1)
        {
            mat.mainTexture = blueNote;
            needsA = true;
        }
        if (rc.LevelList[index].type == 2)
        {
            mat.mainTexture = redNote;
            needsB = true;
        }
        if (rc.LevelList[index].type == 3)
        {
            mat.mainTexture = purpleNote;
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

        if (Destroyed) return;

        if ((A && !needsA) || (!A && !needsB))
        {
            SFXManager.Instance.PlaySFX(SFX.HitSchifoso);
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points -= 3;
        }
        else if (accuracy < rc.GreatDist)
        {
            SFXManager.Instance.PlaySFX(SFX.HitEccellente);
            Client.ShowFeedback(JudgementType.Eccellente);
            rc.points += 5;
        }
        else if (accuracy < rc.GoodDist)
        {
            SFXManager.Instance.PlaySFX(SFX.HitGrande);
            Client.ShowFeedback(JudgementType.Grande);
            rc.points += 3;
        }
        else if (accuracy < rc.OkDist)
        {
            SFXManager.Instance.PlaySFX(SFX.HitBene);
            Client.ShowFeedback(JudgementType.Bene);
            rc.points += 1;
        }
        else if (accuracy < rc.BadDist)
        {
            SFXManager.Instance.PlaySFX(SFX.HitSchifoso);
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points += 0;
        }
        else
        {
            SFXManager.Instance.PlaySFX(SFX.HitSchifoso);
            Client.ShowFeedback(JudgementType.Schifoso);
            rc.points -= 3;
        }

        if (A && needsA)
        {
            needsA = false;
            mat.mainTexture = redNote;
        }

        if (!A && needsB)
        {
            needsB = false;
            mat.mainTexture = blueNote;
        }

        rc.UpdatePointText();

        if (full)
        {
            if (Destroyed) return;
            Destroyed = true;
            DestroyImmediate(this.gameObject);
        }

        if (!needsA && !needsB)
        {
            if (Destroyed) return;
            Destroyed = true;
            DestroyImmediate(this.gameObject);
        }
    }

    public void ReactToFeedback(float scaleMultiplier, float duration)
    {
        if (feedbackRoutine != null) StopCoroutine(feedbackRoutine);
        feedbackRoutine = StartCoroutine(AnimateScale(scaleMultiplier, duration));
    }
    
    private IEnumerator AnimateScale(float scaleMultiplier, float duration)
    {
        Vector3 targetScale = originalScale * scaleMultiplier;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);

            yield return null;
        }

        transform.localScale = originalScale;
    }
}

