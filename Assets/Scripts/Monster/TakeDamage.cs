using System.Collections;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageable
{

    public float hitDamage;
    private GameObject monster;
    private Monster monsterHp;
    private MonsterController monsterController;

    private void Start()
    {
        monster = transform.root.gameObject;
        monsterHp = monster.GetComponent<Monster>();
        monsterController = monster.GetComponent<MonsterController>();
        
    }

    public void Damage()
    {
        monsterHp.hp -= hitDamage;

        if (monsterHp.hp <= 0)
        {
            monsterController.state = MonsterController.State.DIE;
            StartCoroutine(MonsterSetActiveFalse(monster, 3f));
        }

    }

    IEnumerator MonsterSetActiveFalse(GameObject obj, float second)
    {

        yield return new WaitForSeconds(second);
        obj.SetActive(false);
    }
}
