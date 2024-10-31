using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampFIre : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<IDamageable> things = new List<IDamageable>();

    private void Start()
    {
        //InvokeRepeating("DealDamage", 0, damageRate);
        StartCoroutine(RepeatInvoke());  // Q2 - 개선 문제 추가 내용
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            things.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
    }

    private IEnumerator RepeatInvoke() // Q2 - 개선 문제 추가 내용
    {
        WaitForSeconds timeTerm = new WaitForSeconds(damageRate);

        while (true)
        {
            DealDamage();

            yield return timeTerm;
        }
    }
}