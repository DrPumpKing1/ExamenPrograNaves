using Features;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Controller, LivingEntity, InputEntity
{   
    public float currentHealth { get; set; }
    public Vector2 directionInput { get; set; }

    private void OnEnable()
    {
        SearchFeature<Life>().OnDeath += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        SearchFeature<Life>().OnDeath -= () => Destroy(gameObject);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        directionInput = ctx.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            SearchFeature<Shoot>().FeatureAction(this, new Setting("bulletName", "white", Setting.ValueType.String));
        }
    }

    public void OnShootAlternate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SearchFeature<Shoot>().FeatureAction(this, new Setting("bulletName", "black", Setting.ValueType.String));
        }
    }
}
