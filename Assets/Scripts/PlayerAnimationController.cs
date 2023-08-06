using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator playerAnim;

    private string gameStart = "GameStart";
    private string leftStrafe = "LeftStrafe";
    private string rightStrafe = "RightStrafe";
    private string jump = "isJumping";
    private string slide = "isSliding";
    private string die = "GameOver";

    private void OnEnable()
    {
        SwipeController.firstTap += GameStarts;
        SwipeController.moveLeft += LeftStrafe;
        SwipeController.moveRight += RightStrafe;
        SwipeController.moveUp += Jump;
        SwipeController.moveDown += Slide;
         
    }

    private void OnDisable()
    {
        SwipeController.firstTap -= GameStarts;
        SwipeController.moveLeft -= LeftStrafe;
        SwipeController.moveRight -= RightStrafe;
        SwipeController.moveUp -= Jump;
        SwipeController.moveDown -= Slide;
    }

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void GameStarts()
    {
        playerAnim.SetBool(gameStart, true);
    }

    private void LeftStrafe()
    {
        playerAnim.SetBool(leftStrafe, true);
    }

    private void RightStrafe()
    {
        playerAnim.SetBool(rightStrafe, true);
    }

    private void Jump()
    {
        playerAnim.SetBool(jump, true);
    }

    private void Slide()
    {
        playerAnim.SetBool(slide, true);
    }

    private void Die()
    {
        playerAnim.SetBool(die, true);
    }

    public void LeftStrafeExit()
    {
        playerAnim.SetBool(leftStrafe, false);
    }

    public void RightStrafeExit()
    {
        playerAnim.SetBool(rightStrafe, false);
    }

    private void JumpExit()
    {
        playerAnim.SetBool(jump, false);
    }

    private void SlideExit()
    {
        playerAnim.SetBool(slide, false);
    }
}

