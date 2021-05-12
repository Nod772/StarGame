using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    [Header("Set in Inteseptor")]
    public GameObject prefabProjectile;

    [Header("Set in Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimimgMode;
    public float velocityMult = 8f;
    private Rigidbody projectileRigibody;

    // Start is called before the first frame update


    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S==null)
            {
                return Vector3.zero;
            }
            else
            {
                return S.launchPos;
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         if (!aimimgMode)
        {
            return;
        }
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude>maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButton(0))
        {
            aimimgMode = false;
            projectileRigibody.isKinematic = false;
            projectileRigibody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }
       
    }

    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    private void OnMouseEnter()
    {
        print("Slignshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        print("Slignshot:OnMouseExit()");

        launchPoint.SetActive(false);
    }
    private void OnMouseDown()
    {
        aimimgMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectileRigibody = projectile.GetComponent<Rigidbody>();
        projectileRigibody.isKinematic = true;
    }
}
