using UnityEngine;
using System.Collections;

public class PlayAnimationScript : MonoBehaviour {

    Animator animator;
    public float timeThingy = 0.14f;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }
    public void PlayAnimation()
    {
        animator.speed = 2f;
    }

    void Update()
    {
        if (animator.GetCurrentAnimationClipState(0)[0].weight < timeThingy)
        {
            animator.speed = 0;
        }
        
    }
}
