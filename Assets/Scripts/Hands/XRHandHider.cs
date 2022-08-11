using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Hands
{
    public class XRHandHider : MonoBehaviour
    {
        [SerializeField] private XRBaseControllerInteractor _controller;
        [SerializeField] private Rigidbody _handRigidBody;
        [SerializeField] private float _handShowDelay = 0.15f;
        [SerializeField] private GameObject _hand;

        [SerializeField] private ConfigurableJoint _configurableJoint;

        private Quaternion _originalHandRot;
        // Start is called before the first frame update
        void Start()
        {
            _controller.selectEntered.AddListener(SelectEntered);
            _controller.selectEntered.AddListener(SelectExited);
            _originalHandRot = _handRigidBody.transform.localRotation;
        }
        
        private void SelectEntered(SelectEnterEventArgs arg0)
        {
            Debug.Log((arg0.interactable.transform.name));
           // if (arg0.interactableObject is BaseTeleportationInteractable) return;

           _handRigidBody.gameObject.SetActive(false);
           _hand.SetActive(false);
           
           _configurableJoint.connectedBody = null; //CancelInvoke(nameof(ShowHand));
        }

        private void SelectExited(SelectEnterEventArgs arg0)
        {
            if (arg0.interactable is BaseTeleportationInteractable) return;
           // Invoke(nameof(ShowHand),_handShowDelay);
           ShowHand();
        }

        private void ShowHand()
        {
            _handRigidBody.gameObject.SetActive(true);
            _handRigidBody.transform.position = _controller.transform.position;
            _handRigidBody.transform.rotation = Quaternion.Euler(_controller.transform.eulerAngles+_originalHandRot.eulerAngles);
            _configurableJoint.connectedBody = _handRigidBody;
            
        }
        

    }
}
