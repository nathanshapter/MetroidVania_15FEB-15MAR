using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    // References to effect prefabs. These are set in the inspector
    [Header("Effects")]
    public GameObject RunStopDust;
    public GameObject JumpDust;
    public GameObject LandingDust;
    public GameObject DodgeDust;
    public GameObject WallSlideDust;
    public GameObject WallJumpDust;
    public GameObject AirSlamDust;
    public GameObject ParryEffect;

    private PlayerMovement player;
  //  private AudioManager_PrototypeHero m_audioManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
     //   m_audioManager = AudioManager_PrototypeHero.instance;
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_resetDodge()
    {
        player.ResetDodging();
        float dustXOffset = 0.6f;
        float dustYOffset = 0.078125f;
        player.SpawnDustEffect(RunStopDust, dustXOffset, dustYOffset);
    }

    void AE_setPositionToClimbPosition()
    {
      //  player.SetPositionToClimbPosition();
    }

    void AE_runStop()
    {
     //   m_audioManager.PlaySound("RunStop");
        float dustXOffset = 0.6f;
        float dustYOffset = 0.078125f;
        player.SpawnDustEffect(RunStopDust, dustXOffset, dustYOffset);
    }

    void AE_footstep()
    {
     //   m_audioManager.PlaySound("Footstep");
    }

    void Jump()
    {
      //  m_audioManager.PlaySound("Jump");

        if (!player.IsWallSliding())
        {
            float dustYOffset = 0.078125f;
            player.SpawnDustEffect(JumpDust, 0.0f, dustYOffset);
        }
        else
        {
            player.SpawnDustEffect(WallJumpDust);
        }
    }

    void Landing()
    {
     //   m_audioManager.PlaySound("Landing");
        float dustYOffset = 0.078125f;
        player.SpawnDustEffect(LandingDust, 0.0f, dustYOffset);
    }

    void AE_Throw()
    {
     //   m_audioManager.PlaySound("Jump");
    }

    void AE_Parry()
    {
     //   m_audioManager.PlaySound("Parry");
        float xOffset = 0.1875f;
        float yOffset = 0.25f;
        player.SpawnDustEffect(ParryEffect, xOffset, yOffset);
       // player.DisableMovement(0.5f);
    }

    void AE_ParryStance()
    {
      //  m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirSlam()
    {
      //  m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirLanding()
    {
      //  m_audioManager.PlaySound("AirSlamLanding");
        float dustYOffset = 0.078125f;
        player.SpawnDustEffect(AirSlamDust, 0.0f, dustYOffset);
      //  player.DisableMovement(0.5f);
    }

    void AE_Hurt()
    {
     //   m_audioManager.PlaySound("Hurt");
    }

    void AE_Death()
    {
    //    m_audioManager.PlaySound("Death");
    }

    void AE_SwordAttack()
    {
     //   m_audioManager.PlaySound("SwordAttack");
    }

    void AE_SheathSword()
    {
     //   m_audioManager.PlaySound("SheathSword");
    }

    void AE_Dodge()
    {
     //   m_audioManager.PlaySound("Dodge");
        float dustYOffset = 0.078125f;
        player.SpawnDustEffect(DodgeDust, 0.0f, dustYOffset);
    }

    void AE_WallSlide()
    {
        //m_audioManager.GetComponent<AudioSource>().loop = true;
     //   if (!m_audioManager.IsPlaying("WallSlide"))
      //      m_audioManager.PlaySound("WallSlide");
        float dustXOffset = 0.25f;
        float dustYOffset = 0.25f;
        player.SpawnDustEffect(WallSlideDust, dustXOffset, dustYOffset);
    }

    void AE_LedgeGrab()
    {
      //  m_audioManager.PlaySound("LedgeGrab");
    }

    void AE_LedgeClimb()
    {
       // m_audioManager.PlaySound("RunStop");
    }

}
