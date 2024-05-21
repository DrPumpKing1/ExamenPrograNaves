using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class PatternMovement :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureFixedUpdate //Other channels
    {
        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        [Header("States")]
        [SerializeField] private int indexPattern;
        //States Time Management
        [SerializeField] private float timer;
        //Properties
        [Header("Properties")]
        public List<string> pattern;
        //Properties / Time Management
        public float timeOnPattern;
        //References
        //Components
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;

            //Setup Properties
            string patternString = settings.Search("movementPattern");
            pattern = new List<string>(patternString.Split(','));
            timeOnPattern = settings.Search("timeOnPattern");

            indexPattern = 0;
            timer = timeOnPattern;

            ToggleActive(true);
        }

        public void FixedUpdateFeature(Controller controller)
        {
            if(!active) return;

            MoveOnPattern();
            
            Vector2 direction = StringToPattern(pattern[indexPattern]);

            controller.CallFeature<Movement>(new Setting("movementDirection", direction, Setting.ValueType.Vector2));
        }

        public void MoveOnPattern()
        {
            if(timer > 0)
            {
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                timer = timeOnPattern;
                indexPattern++;
                if(indexPattern >= pattern.Count)
                {
                    indexPattern = 0;
                }

                rb.velocity = Vector2.zero;
            }
        }

        public Vector2 StringToPattern(string pattern)
        {
            switch (pattern)
            {
                case "up":
                    return Vector2.up;
                case "down":
                    return Vector2.down;
                case "left":
                    return Vector2.left;
                case "right":
                    return Vector2.right;
                default:
                    return Vector2.zero;
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