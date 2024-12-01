using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellButton : MonoBehaviour
{
    public Image myImage;
    public type type;
    // Start is called before the first frame update
 private Color getColor()
    {
        Player.canCast();
        switch(type) {
            case type.fire:
                return new Color(1,0.8f,0.8f,Player.fire?1:0.4f);
            case type.water:
                return new Color(0.8f,0.8f,1,Player.water?1:0.4f);
            case type.earth:
                return new Color(0.8f,1,0.8f,Player.earth?1:0.4f);
            case type.air:
                return new Color(0.8f,0.8f,0.8f,Player.air?1:0.4f);

            }
            return Color.white;
        }
    // Update is called once per frame
    void FixedUpdate()
    {
        myImage.sprite = GetSprite();
         myImage.color = getColor();
        
    }
    public Sprite dash;
    public Sprite jump;
    public Sprite glide;
    public Sprite climb;
    public Sprite platform;
    public Sprite blast;

    private Sprite GetSprite()
    {
          switch(Player.player.spellbook[type]) {
            case spell.jump:
                return jump;
            case spell.dash:
                return dash;
            case spell.glide:
                return glide;
            case spell.climb:
                return climb;
            case spell.platform:
                return platform;

            case spell.blast:
                return blast;

            }
                return blast;
        }
    }

