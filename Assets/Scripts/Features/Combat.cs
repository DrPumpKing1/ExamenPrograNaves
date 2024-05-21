using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class Combat :  MonoBehaviour, IActivable, IFeatureSetup //Other channels
    {
        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        //Properties
        [Header("States")]
        public string tagToAttack;
        public float attack;
        //References
        //Componentes

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;

            //Setup Properties
            attack = settings.Search("attack");
            tagToAttack = settings.Search("tagToAttack");

            ToggleActive(true);

            CombatEntity combatEntity = controller as CombatEntity;
            if (combatEntity != null) combatEntity.attack = attack;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!active) return;

            if (collision.gameObject.CompareTag(tagToAttack))
            {
                var otherController = collision.gameObject.GetComponent<Controller>();
                var controller = GetComponent<Controller>();
                var link = new AttackLink(controller, otherController, null);

                Destroy(gameObject);
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