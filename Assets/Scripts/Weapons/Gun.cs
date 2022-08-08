using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable _grabInteractable;

        [SerializeField] protected Transform _gunBarrel;

        [SerializeField] protected XRSocketInteractor _ammoSocket;

        protected Ammo _ammoClip;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            Assert.IsNotNull(_grabInteractable,"You have not assigned grab interactable to gun"+name);
            Assert.IsNotNull(_ammoSocket,"You have not assigned gun barrel to gun"+name);
            Assert.IsNotNull(_gunBarrel,"You have not assigned ammo socket to gun"+name);
            
            _ammoSocket.selectEntered.AddListener(AmmoAttached);
            _ammoSocket.selectExited.AddListener(AmmoRemoved);
            
            _grabInteractable.activated.AddListener(Fire);
        
        }

        protected virtual  void Fire(ActivateEventArgs arg0)
        {
            if(!CanFire()) return;
            _ammoClip.amount-=1;
        }

        protected virtual  bool CanFire()
        {
            if (!_ammoClip)
            {
                Debug.Log("No ammo clip inserted");
                return false;
            }
        
            if (_ammoClip.amount <= 0)
            {
                _ammoClip.amount = 0;
                Debug.Log("No bullets in gun");
                return false;
            }

            return true;
        }

        protected virtual  void AmmoRemoved(SelectExitEventArgs arg0)
        {
           // IgnoreCollision(arg0.interactable,false);
            _ammoClip = null;
        }

        protected virtual void AmmoAttached(SelectEnterEventArgs arg0)
        {
           // IgnoreCollision(arg0.interactable,true);
            _ammoClip = arg0.interactable.transform.GetComponent<Ammo>();
        }

       /* private void IgnoreCollision(IXRInteractable interactable, bool ignore)
        {
            Collider[] myColliders = GetComponentsInChildren<Collider>();
            foreach (Collider myCollider in myColliders)
            {
                foreach (Collider interactableCollider in interactable.colliders)
                {
                    Physics.IgnoreCollision(myCollider,interactableCollider,ignore);
                    
                }
                
            }
        }*/

   
    }
}
