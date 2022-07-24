using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Hands
{
    public class XRHandTeleport : MonoBehaviour
    {
        [SerializeField] private XRBaseInteractor _teleportController;
        [SerializeField] private XRBaseInteractor _mainController;
        [SerializeField] private Animator _handAnimator;

        [SerializeField] private GameObject _pointer;


        private void Start()
        {
            _teleportController.selectEntered.AddListener(MoveSelectionToMainController);
        }

        private void MoveSelectionToMainController(SelectEnterEventArgs arg0)
        {
            
            IXRSelectInteractable interactable = arg0.interactableObject;
            if(interactable is BaseTeleportationInteractable) return;
            if(interactable.isSelected) _teleportController.interactionManager.SelectEnter(_mainController,interactable);
        }


        // Update is called once per frame
        private void Update()
        {
            _pointer.SetActive(_handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Point") &&_handAnimator.gameObject.activeSelf);
        
        }
    }
}
