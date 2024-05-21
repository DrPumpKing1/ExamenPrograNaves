using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Features;

public class Enemy : Controller, LivingEntity, CombatEntity
{
    public float currentHealth { get; set; }
    public float attack { get; set; }

    private void OnEnable()
    {
        SearchFeature<Life>().OnDeath += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        SearchFeature<Life>().OnDeath -= () => Destroy(gameObject);
    }
}
