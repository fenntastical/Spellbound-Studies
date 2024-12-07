using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordParent : MonoBehaviour
{
    public SpriteRenderer swordRenderer;
    public Animator swordAnim;
    public SpriteRenderer swordEffectRenderer;
    public Animator swordEffectAnim;
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timer > .5)
            {
                timer = 0;
                Attack();
            }
            // Attack();
            // AudioMgr.Instance.PlaySFX("Attack");
        }
    }

    public void Attack()
    {
        swordAnim.SetTrigger("SwordAttack");
        swordEffectAnim.SetTrigger("SwordTrigger");
    }
}
