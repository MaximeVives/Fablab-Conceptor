using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonHelper : MonoBehaviour
{
    public Image preview;
    public TextMeshProUGUI text;
    public GameObject go;
    public Vector3 spawnPosition;
    private MeshCollider meshCollider;

    public void HandleClick()
    {
        GameObject instance = Instantiate(go, spawnPosition, Quaternion.identity);
        instance.layer = 9;

        Rigidbody rb = instance.AddComponent<Rigidbody>();
        AddCollider(instance.transform);

        rb.useGravity = true;
        rb.isKinematic = false;

        XRGrabInteractable gi = instance.AddComponent<XRGrabInteractable>();
        gi.movementType = XRBaseInteractable.MovementType.Instantaneous;
        // Permettre à l'objet de garder la propriété kinematic lorsqu'il est grab  
        gi.forceGravityOnDetach = true;
        gi.useDynamicAttach = false;

    }

    private void AddCollider(Transform obj)
    {
        if (obj.GetComponent<MeshRenderer>() != null)
        {
            MeshCollider mc = obj.gameObject.GetComponent<MeshCollider>();
            if (mc == null)
            {
                mc = obj.gameObject.AddComponent<MeshCollider>();
            }
            mc.convex = true;
        }
        else
        {
            MeshCollider mc = obj.gameObject.AddComponent<MeshCollider>();
            mc.convex = true;
        }

        foreach (Transform child in obj)
        {
            AddCollider(child);
        }
    }
}
