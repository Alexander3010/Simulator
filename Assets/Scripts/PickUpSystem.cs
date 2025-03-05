using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{
    public GameObject Player;
    public Transform HoldPos;
    public Image ThrowButtonImg;
    public float ThrowForce = 500f;
    public float PickUpRange = 5f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private int LayerNumber;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        ThrowButtonImg.gameObject.SetActive(false);
    }
    void Update()
    {
        if (heldObj != null)
        {
            MoveObject();
        }
    }
    public void TouchToPickUp()
    {
        if (heldObj == null)
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, PickUpRange))
            {
                if (Hit.transform.gameObject.tag == "canPickUp")
                {
                    PickUpObject(Hit.transform.gameObject);
                }
            }
        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = HoldPos.transform;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), Player.GetComponent<Collider>(), true);
            ThrowButtonImg.gameObject.SetActive(true);
        }
    }
    void MoveObject()
    {
        heldObj.transform.position = HoldPos.transform.position;
    }
    public void ThrowObject()
    {
        if (heldObj != null)
        {
            StopClipping();
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
            heldObj.layer = 0;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObjRb.AddForce(transform.forward * ThrowForce);
            heldObj = null;
            ThrowButtonImg.gameObject.SetActive(false);
        }
    }
    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] Hits;
        Hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (Hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
