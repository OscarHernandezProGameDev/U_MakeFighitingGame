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
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        if (ScreenBounds.x > Screen.width - 200)
        {
            CanWalkRight = false;
        }
        else if (ScreenBounds.x < 200)
        {
            CanWalkLeft = false;
        }
        else
        {
            CanWalkLeft = true;
            CanWalkRight = true;
        }

        // El tag nos indicará si nos podemos mover o no
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (CanWalkRight)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (CanWalkLeft)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(-WalkSpeed, 0, 0);
                }
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
