using UnityEngine;

class Puddle: MagicCrouch{

    Sprite playerSprite; //What is the players sprite when crouched
    float height = 0.2f; //What is the players height when Crouched.

}

class Blaze: MagicCrouch{

    Sprite playerSprite; //What is the players sprite when crouched
    float height = 0.2f; //What is the players height when Crouched.

}
internal abstract class MagicCrouch
{
    Sprite playerSprite; //What is the players sprite when crouched
    float height; //What is the players height when Crouched.
}
internal class DefaultCrouch :MagicCrouch
{
    Sprite playerSprite; //What is the players sprite when crouched
    float height = 0.5f; //What is the players height when Crouched.
}