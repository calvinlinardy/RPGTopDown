using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishGranter : Collidable
{
    bool isTouched = false;
    [SerializeField] Sprite grimSprite = null;
    [SerializeField] Sprite loserSprite = null;
    [SerializeField] int pesosRichAmount = 99999;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && coll.tag == "Fighter")
        {
            if (!isTouched)
            {
                GameManager.instance.pesos = pesosRichAmount;
                GameManager.instance.player.SwapGrimSprites(grimSprite);
                GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = true;
                GameManager.instance.experience = 9999;
                GameManager.instance.weapon.SetWeaponLevel(6);
                isTouched = true;
            }
        }

        if (coll.tag == "Weapon")
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        GameManager.instance.ShowText("Why did you do that, kid?", 20, Color.gray, transform.position, Vector3.up * 60, 3.0f);
        GameManager.instance.pesos = 0;
        GameManager.instance.player.SwapGrimSprites(loserSprite);
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = true;
        GameManager.instance.experience = 0;
        GameManager.instance.weapon.SetWeaponLevel(0);
    }
}
