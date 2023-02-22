using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void HandleMovementAnimation(float speed, bool isSprinting, bool isCrouching)
    {
        float moveSpeed = isSprinting && speed > .1 ? 1.5f : speed;
        _animator.SetBool("Crouch", isCrouching);
        _animator.SetFloat("Speed", moveSpeed);
    }
}
