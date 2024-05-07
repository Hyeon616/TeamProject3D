using System.Collections;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageable
{

    public float hitDamage;
    [SerializeField] private GameObject monster;
    private Monster monsterHp;
    private MonsterController monsterController;

    private void Start()
    {
        monsterHp = monster.GetComponent<Monster>();
        monsterController = monster.GetComponent<MonsterController>();

    }

    public void Damage(int damage)
    {

        monsterHp.hp -= (hitDamage + damage);

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
