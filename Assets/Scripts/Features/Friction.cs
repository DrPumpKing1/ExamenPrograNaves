using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class Friction :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureFixedUpdate //Other channels
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
        public float friction;
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

            //Setup Properties
            friction = settings.Search("friction");

            ToggleActive(true);
        }

        public void FixedUpdateFeature(Controller controller)
        {
            if(!active) return;

            InputEntity input = controller as InputEntity;
            if(input == null) return;

            Vector2 direction = input.directionInput;
            ApplyFriction(direction);
        }

        public void ApplyFriction(Vector2 direction)
        {
            if (!active)
            {
                rb.drag = 0;
                return;
            }

            Vector2 velocity = rb.velocity;
            bool changeDir = changeDirection(direction, velocity);

            if (changeDir)
            {
                rb.drag = friction;
            }
            else
            {
                rb.drag = 0;
            }

            bool changeDirection(Vector2 direction, Vector2 velocity)
            {
                return Vector2.Dot(direction, velocity) < 0 || direction == Vector2.zero;
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