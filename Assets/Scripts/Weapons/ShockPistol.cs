using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class ShockPistol : Gun
    {
        [SerializeField] private Renderer[] _gunRenderer;
        [SerializeField] private Material[] _ammoScreenMaterials;


        protected override void Start()
        {
            var activeAmmoSocket = GetComponentInChildren<XRTagLimitedSocketInteractor>();
            _ammoSocket = activeAmmoSocket;
            base.Start();
            Assert.IsNotNull(_gunRenderer,"You have not assigned a renderer to gun"+name);
            Assert.IsNotNull(_ammoScreenMaterials,"You have not ammo materials to gun"+name);
            
        }

        protected override void AmmoRemoved(SelectExitEventArgs arg0)
        {
            base.AmmoRemoved(arg0);
            UpdateShockPistolScreen();
        }

        protected override void AmmoAttached(SelectEnterEventArgs arg0)
        {
            base.AmmoAttached(arg0);
            UpdateShockPistolScreen();
        }

        protected override void Fire(ActivateEventArgs arg0)
        {
            if(!CanFire()) return;
            base.Fire(arg0);
            UpdateShockPistolScreen();

            Rigidbody bullet = Instantiate(_ammoClip.bulletObject, _gunBarrel.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
            bullet.AddForce(_gunBarrel.forward*_ammoClip.bulletSpeed,ForceMode.Impulse);
            Destroy(bullet.gameObject,6f);
        }

        private void UpdateShockPistolScreen()
        {
            
            if (!_ammoClip)
            {
                AssignScreenMaterial(_ammoScreenMaterials[0]);
                return;

            }
            AssignScreenMaterial(_ammoScreenMaterials[_ammoClip.amount]);
        }

        private void AssignScreenMaterial(Material newMaterial)
        {
            foreach (var rend in _gunRenderer)
            {
                if(rend.gameObject.activeSelf) continue;
                Material[] mats = rend.materials;
                mats[1] = newMaterial;
                rend.materials = mats;
            }
           
        }
    }
}
