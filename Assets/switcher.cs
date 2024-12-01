using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum type{
    fire,water,earth,air,time
}
public enum spell{
    jump,dash,blast,
    platform,glide,climb,
    rewind,rewrite,none
}
public class switcher : MonoBehaviour
{
    public type type;
    public spell spell;
    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        if(!valid(spell,type)){
                Debug.LogWarning("Attempting to create switcher object with invalid spell/element combo.");
        }
        GetComponent<SpriteRenderer>().color = getColor();
    }

    private Color getColor()
    {
        switch(type) {
            case type.fire:
                return new Color(1,0.8f,0.8f,1);
            case type.water:
                return new Color(0.8f,0.8f,1,1);
            case type.earth:
                return new Color(0.8f,1,0.8f,1);
            case type.air:
                return new Color(0.8f,0.8f,0.8f,1);

            }
            return Color.white;
        }
    
   

    private bool valid(spell spell, type type)
    {
        switch(spell){
            case spell.jump:
                return type==type.fire||type==type.air||type==type.water;
            case spell.dash:
                return type==type.fire||type==type.water;
            case spell.blast:
                return type==type.fire||type==type.water;
            case spell.platform:
                return type == type.earth || type == type.air;
            case spell.glide:
                return  type == type.air;
            case spell.climb:
                return type == type.earth;
            case spell.rewind:
                return  type == type.time;
            case spell.rewrite:
                return  type == type.time;
        }
        return false;
    }
    public Collider2D collider;
    // Update is called once per frame

    void clear(type t){
        switch(t){
            case type.fire:
                Player.animator.SetBool("FireJump", false);
                Player.animator.SetBool("FireSlide", false);
                Player.animator.SetBool("FireBlast", false);
                break;
            case type.water:
                Player.animator.SetBool("WaterJump", false);
                Player.animator.SetBool("WaterSlide", false);
                Player.animator.SetBool("WaterBlast", false);
                break;
            case type.air:
                Player.animator.SetBool("AirJump", false);
                Player.animator.SetBool("AirPlatform", false);
                Player.animator.SetBool("AirGlide", false);
                break;
            case type.earth:
                Player.animator.SetBool("EarthClimb", false);
                Player.animator.SetBool("EarthPlatform", false);
                break;
                case type.time:
                Player.animator.SetBool("TimeWind", false);
                Player.animator.SetBool("TimeWrite", false);
                break;
        }
    }
    void set(type t,spell s){
        clear(t);
        Player.player.spellbook[type] = spell;
        Player.animator.SetBool("IsLearning", true);
        var spellType = (s, t);
                switch(spellType){
            case (spell.jump, type.fire):
                Player.animator.SetBool("FireJump", true);
                break;
            case (spell.jump, type.water):
                Player.animator.SetBool("WaterJump", true);
                break;
            case (spell.jump, type.air):
                Player.animator.SetBool("AirJump", true);
                break;
            case (spell.dash, type.fire):
                Player.animator.SetBool("FireSlide", true);
                break;
            case (spell.dash, type.water):
                Player.animator.SetBool("WaterSlide", true);
                break;
            case (spell.blast, type.fire):
                Player.animator.SetBool("FireBlast", true);
                break;
            case (spell.blast, type.water):
                Player.animator.SetBool("WaterBlast", true);
                break;
            case (spell.platform, type.air):
                Player.animator.SetBool("AirPlatform", true);
                break;
            case (spell.platform, type.earth):
                Player.animator.SetBool("EarthPlatform", true);
                break;
            case (spell.glide, type.air):
                Player.animator.SetBool("AirGlide", true);
                break;
            case (spell.climb,type.earth):
        Player.animator.SetBool("EarthClimb", true);
                break;
            case (spell.rewind,type.time):
                Player.animator.SetBool("TimeWind", true);
                break;
            case (spell.rewrite,type.time):
                Player.animator.SetBool("TimeWrite", true);
                break;
        }
    }
    void Update()
    {
        if(collider.IsTouching(Player.collider)){
            if( Player.player.spellbook[type]!=spell){
                set(type, spell);
            }else{
            
            Player.animator.SetBool("IsLearning", false);
        }
        }else{
            
            Player.player.interact = Player.Interact.Cast;
        }
        
    }
}
