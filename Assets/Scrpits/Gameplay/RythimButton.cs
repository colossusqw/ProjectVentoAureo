using UnityEngine;

public class RythimButton : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Rigidbody Target;

    [SerializeField] private RythimManager rc;

    private Vector3 targetStartPos;

    private MeshRenderer mr;

    public float duration = 0.2f;
    public float cooldown = 1f;

    public string ActivateKey;

    public bool OnCooldown = false;

    public bool targetActive = false;

    void Start()
    {   
        mr = this.gameObject.GetComponent<MeshRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        targetStartPos = Target.transform.position;

        ReleaseButton();
    }

    void Update()
    {
        if (!OnCooldown && Input.GetKey(ActivateKey)) PressedButton();

        HandleTargetControl();
    }

    void HandleTargetControl()
    {
        if (rc.playing && targetActive)
        {
            Target.linearVelocity = transform.right * -rc.targetvel;
        }
        else
        {
            Target.linearVelocity = transform.right * 0f;
            Target.transform.position = targetStartPos;
            targetActive = false;
        }
        if (Target.transform.position.x < -12f)
        {
            Target.transform.position = targetStartPos;
            targetActive = false;
        }
    }

    public void PressedButton()
    {
        mr.enabled = true;
        OnCooldown = true;

        float accuracy = Mathf.Abs(rb.transform.position.x - Target.transform.position.x);

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

        targetActive = false;
        rc.UpdatePointText();


        Invoke("ReleaseButton", duration);
        Invoke("FinishCooldown", cooldown);
    }

    public void ReleaseButton()
    {
        mr.enabled = false;
    }

    public void FinishCooldown()
    {
        OnCooldown = false;
    }
}