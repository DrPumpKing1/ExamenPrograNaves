using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class Life :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureAction, IFeatureUpdate //Other channels
    {
        public event Action OnDamage;
        public event Action OnDeath;

        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        [Header("States")]
        [SerializeField] private float currentHealth;
        //Properties
        [Header("Properties")]
        public float maxHealth;
        //References
        //Componentes

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;

            //Setup Properties
            maxHealth = settings.Search("maxHealth");
            currentHealth = maxHealth;

            ToggleActive(true);
        }

        public void UpdateFeature(Controller controller)
        {
            if(!active) return;

            LivingEntity life = controller as LivingEntity;
            if(life != null)
            {
                life.currentHealth = currentHealth;
            }
        }

        public void FeatureAction(Controller controller, params Setting[] settings)
        {
            if(!active) return;

            if (settings.Length <= 0) return;

            float damage = settings[0].floatValue;

            Damage(damage);
        }

        public void Damage(float damage)
        {
            if(!active) return;

            if (damage <= 0) return;

            OnDamage?.Invoke();

            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            if(currentHealth <= 0) OnDeath?.Invoke();
        }

        public bool GetActive()
        {
            return active;
        }

        public void ToggleActive(bool active)
        {
            this.active = active;
        }
    }
}