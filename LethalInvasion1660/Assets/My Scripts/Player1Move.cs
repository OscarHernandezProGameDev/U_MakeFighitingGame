using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.001f;
    private bool isJumping = false;
    private AnimatorStateInfo Player1Layer0;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);
        // El tag nos indicará si nos podemos mover o no
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                Anim.SetBool("Forward", true);
                transform.Translate(WalkSpeed, 0, 0);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Anim.SetBool("Backward", true);
                transform.Translate(-WalkSpeed, 0, 0);
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            if (!isJumping)
            {
                isJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }

        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }
    }

    private IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        isJumping = false;
    }
}
