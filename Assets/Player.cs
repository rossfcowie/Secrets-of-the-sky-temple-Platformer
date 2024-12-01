using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
  public static GameObject gameObject { get { return player.go; } set { player.go = value; } }
  public static Collider2D collider { get { return player.c != null ? player.c : player.c = player.go.GetComponent<BoxCollider2D>(); } set { player.c = value; } }
  public static Animator animator { get { return player.a != null ? player.a : player.a = player.go.GetComponent<Animator>(); } set { player.a = value; } }
  public static Player player { get { return singleton != null ? singleton : singleton = new Player(); } set { singleton = value; } }
  public static Vector3 lastsafe;
  public static void Safesave()
  {
    lastsafe = gameObject.transform.position;
  }
  public static void Safeload()
  {
    gameObject.transform.position = lastsafe;
  }
  public static float movespeed { get { return 0.5f + Math.Max(0, Momentum) * 2; } }
  public static float Momentum = 0f;
  public static float Verticalsmovespeed { get { return 0f + Math.Max(0, Verticals); } }
  public static float Verticals = 0f;
  private static Player singleton;
  public GameObject go;
  public Collider2D c;
  public Animator a;
  public enum Interact
  {
    Interact, Cast, CastAriel
  }
  public Interact interact;
  public Dictionary<type, spell> spellbook = new() { { type.fire, spell.none }, { type.water, spell.none }, { type.earth, spell.none }, { type.air, spell.none }, { type.time, spell.none } };
  public Dictionary<(type, spell), bool> learned = new();
  public static Dictionary<Inputbutton.button, bool> buttonDown = new(){
        { Inputbutton.button.Action,false},
        { Inputbutton.button.Left,false},
        { Inputbutton.button.Right,false},
        { Inputbutton.button.Up,false},
        { Inputbutton.button.Down,false}
      };
  internal static bool interactDown()
  {
    return Input.GetKey(KeyCode.Space) || buttonDown[Inputbutton.button.Action];
  }
  internal static bool LeftDown()
  {
    return buttonDown[Inputbutton.button.Left] || Input.GetAxis("Horizontal") < 0;
  }
  internal static float GetHorizontal()
  {
    return (LeftDown() ? -1 : 0) + (rightDown() ? 1 : 0);
  }
  internal static float GetHorizontalRIghtOrLeft()
  {
    return (LeftDown() ? -1 : 1) + (rightDown() ? 1 : 1);
  }
  internal static bool rightDown()
  {
    return buttonDown[Inputbutton.button.Right] || Input.GetAxis("Horizontal") > 0;
  }
  internal static bool upDown()
  {
    return buttonDown[Inputbutton.button.Up] || Input.GetAxis("Vertical") > 0;
  }
  internal static bool DownDown()
  {
    return buttonDown[Inputbutton.button.Down] || Input.GetAxis("Vertical") < 0;
  }

  internal static bool interacting()
  {
    return singleton.interact == Interact.Interact && interactDown();
  }

  internal static bool Casting()
  {
    return (singleton.interact == Interact.Cast || singleton.interact == Interact.CastAriel) && (interactDown() || CastingCount() > 0);
  }
  internal static bool canCast(bool grounded)
  {
    bool moving = animator.GetBool("Is_moving");
    bool crouched = animator.GetBool("Is_Crouched");
    bool Jumping = animator.GetBool("Is_Jumping");
    if (grounded)
    {
      if (crouched)
      {
        return player.spellbook[type.fire] == spell.dash || player.spellbook[type.water] == spell.dash;
      }
      else
      {
        return player.spellbook[type.time] == spell.rewind || player.spellbook[type.fire] == spell.blast || player.spellbook[type.water] == spell.blast || player.spellbook[type.air] == spell.platform || player.spellbook[type.earth] == spell.platform;
      }
    }
    else
    {
      return player.spellbook[type.earth] == spell.climb || player.spellbook[type.fire] == spell.jump || player.spellbook[type.water] == spell.jump || player.spellbook[type.air] == spell.jump || player.spellbook[type.air] == spell.glide;
    }

  }

  internal static int CastingCount()
  {
    if (Input.GetKey(KeyCode.Z))
    {

      return 1;
    }
    if (Input.GetKey(KeyCode.X))
    {

      return 2;
    }
    if (Input.GetKey(KeyCode.C))
    {

      return 3;
    }
    if (Input.GetKey(KeyCode.V))
    {

      return 4;
    }
    return 0;
  }

  internal static float GetVertical()
  {
    return (DownDown() ? -1 : 0) + (upDown() ? 1 : 0);
  }
  internal static float GetVerticalUpOrDown()
  {
    return (DownDown() ? -1 : 1) + (upDown() ? 1 : 1);
  }

  public static bool fire, water, air, earth = false;
  internal static void canCast()
  {
    if (!animator.GetBool("Is_moving"))
    {
      water = player.spellbook[type.water] == spell.blast;
      fire = player.spellbook[type.fire] == spell.blast;
      earth = player.spellbook[type.earth] == spell.platform;
      air = player.spellbook[type.air] == spell.platform;

    }
      if (animator.GetBool("IsWalled"))
      {
        earth = player.spellbook[type.earth] == spell.climb;
      }
      else
      {
        if (animator.GetBool("Is_Crouched"))
        {
          water = player.spellbook[type.water] == spell.dash;
          fire = player.spellbook[type.fire] == spell.dash;
        }
        else
        {
          if (animator.GetBool("Is_Jumping"))
          {
            water = player.spellbook[type.water] == spell.jump;
            air = player.spellbook[type.air] == spell.jump || player.spellbook[type.air] == spell.glide;
          }
        }
      }



    }

  }

