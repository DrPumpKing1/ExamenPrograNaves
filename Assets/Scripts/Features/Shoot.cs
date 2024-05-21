using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class Shoot :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureUpdate, IFeatureAction //Other channels
    {
        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        [SerializeField] private float shootTimer;
        public bool canShoot => shootTimer <= 0;
        //Properties
        [Header("Properties")]
        public Vector2 shootDirection;
        public float shootForce;
        public float shootCooldown;
        public float bulletsLifeTime;
        public GameObject bulletPrefabWhite;
        public GameObject bulletPrefabBlack;
        //References
        //Componentes

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;

            //Setup Properties
            shootDirection = settings.Search("shootDirection");
            shootForce = settings.Search("shootForce");
            shootCooldown = settings.Search("shootCooldown");
            bulletsLifeTime = settings.Search("bulletsLifeTime");
            bulletPrefabWhite = settings.Search("bulletPrefabWhite");
            bulletPrefabBlack = settings.Search("bulletPrefabBlack");

            ToggleActive(true);
        }

        public void FeatureAction(Controller controller, params Setting[] settings)
        {
            if (!active) return;

            if (settings.Length <= 0) return;

            string bulletName = settings[0].stringValue;

            ShootBullet(bulletName);
        }

        private void ShootBullet(string bulletName)
        {
            if (!canShoot) return;

            GameObject bullet = Instantiate(bulletName == "white" ? bulletPrefabWhite : bulletPrefabBlack, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if(rb != null) rb.AddForce(shootDirection.normalized * shootForce, ForceMode2D.Impulse);
            Destroy(bullet, bulletsLifeTime);
            shootTimer = shootCooldown;
        }

        public void UpdateFeature(Controller controller)
        {
            if (shootTimer > 0) shootTimer -= Time.deltaTime;
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