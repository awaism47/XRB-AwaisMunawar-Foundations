using UnityEngine;

public class Handedness : MonoBehaviour
{
    public enum Handed
    {
        Left=0,
        Right=1
    }

    public Handed handed;

    [SerializeField] private GameObject[] leftHandedObjects;

    [SerializeField] private GameObject[] rightHandedObjects;
    
    // Start is called before the first frame update
    void Awake()
    {
        if ((handed == Handed.Left))
        {
            foreach (GameObject obj in leftHandedObjects)
            {
                obj.SetActive(true);
                
            }
            foreach (GameObject obj in rightHandedObjects)
            {
                obj.SetActive(false);
                
            }
            
        }
        if ((handed == Handed.Right))
        {
            foreach (GameObject obj in rightHandedObjects)
            {
                obj.SetActive(true);
                
            }
            foreach (GameObject obj in leftHandedObjects)
            {
                obj.SetActive(false);
                
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
