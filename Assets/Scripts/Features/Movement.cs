using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class Movement :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureFixedUpdate, IFeatureAction //Other channels
    {
        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        //Properties
        [Header("Properties")]
        public float maxSpeed;
        public float acceleration;
        //References
        //Componentes
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;

            maxSpeed = settings.Search("maxSpeed");
            acceleration = settings.Search("acceleration");

            ToggleActive(true);
        }

        public void FixedUpdateFeature(Controller controller)
        {
            if(!active) return;

            LimitSpeed();

            InputEntity input = controller as InputEntity;
            if(input == null) return;

            Move(input.directionInput);
        }

        public void FeatureAction(Controller controller, params Setting[] settings)
        {
            if(!active) return;
            
            if(settings.Length <= 0) return;
        
            Vector2 direction = settings[0].vector2Value;
            
            Move(direction);
        }

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero || !active) return;

            Vector2 movement = direction.normalized * acceleration;

            if(rb != null) rb.AddForce(movement * Time.fixedDeltaTime * 10f);
        }

        public void LimitSpeed()
        {
            if (!active) return;

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
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