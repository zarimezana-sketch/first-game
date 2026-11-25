using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator bodyAnimator;

    private void Awake()
    {
        if (bodyAnimator == null)
        {
            Transform body = transform.Find("Body");
            if (body != null)
                bodyAnimator = body.GetComponent<Animator>();
        }
    }

    public void UpdateMovement(float horizontalSpeed, bool isGrounded)
    {
        if (bodyAnimator == null) return;
        bodyAnimator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));
        bodyAnimator.SetBool("IsGrounded", isGrounded);
    }

    public void TriggerJump()
    {
        if (bodyAnimator != null)
            bodyAnimator.SetTrigger("Jump");
    }

    public void TriggerShoot()
    {
        if (bodyAnimator != null)
            bodyAnimator.SetTrigger("Shoot");
    }
}