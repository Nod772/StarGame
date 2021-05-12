using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in Inteseptor")]
    public GameObject cloudSphere;
    public int numShepesMin = 6;
    public int numShepesMax = 10;
    
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public Vector2 sphereScaleX = new Vector2(4, 8);
    public Vector2 sphereScaleY = new Vector2(3, 4);
    public Vector2 sphereScaleZ = new Vector2(2, 4);
    public float scaleYMin = 2f;

    private List<GameObject> spheres;



    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numShepesMin, numShepesMax);
        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            Vector3 offset = Random.insideUnitSphere;

            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleX.x, sphereScaleX.y);
            scale.y = Random.Range(sphereScaleY.x, sphereScaleY.y);
            scale.z = Random.Range(sphereScaleZ.x, sphereScaleZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Restart();
        //}
        
    }
    private void Restart()
    {
        foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }
        Start();
    }
}
