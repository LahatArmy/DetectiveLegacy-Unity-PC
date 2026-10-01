using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int vertical;
    int horizontal;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        float snappedVertical = 0;
        float snappedHorizontal = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement >= 0.55f)
            snappedHorizontal = 1f;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement <= -0.55f)
            snappedHorizontal = -1f;
        else
            snappedHorizontal = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
            snappedVertical = 0.5f;
        else if (verticalMovement >= 0.55f)
            snappedVertical = 1f;
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
            snappedVertical = -0.5f;
        else if (verticalMovement <= -0.55f)
            snappedVertical = -1f;
        else
            snappedVertical = 0;

        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
    }
}
