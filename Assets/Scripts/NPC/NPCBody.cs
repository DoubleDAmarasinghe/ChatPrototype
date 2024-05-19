using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBody : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Moving(bool isMoving)
    {
        animator.SetBool("Move", isMoving);
    }
    public void Responding()
    {
        animator.SetTrigger("Response");
    }
    public void Combat()
    {
        animator.SetTrigger("Combat");
    }
}
