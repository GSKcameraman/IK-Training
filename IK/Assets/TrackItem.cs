using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TrackItem : MonoBehaviour
{
    public float grabDistance = 1.5f;

    public TwoBoneIKConstraint twoBoneIKConstraint;
    public GameObject handTarget;
    private GameObject grabbleItem = null;

    private float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        twoBoneIKConstraint.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbleItem == null){
            t = 0;
            twoBoneIKConstraint.weight = 0;
            Collider[] colliders = Physics.OverlapSphere(transform.position,grabDistance);
            foreach (Collider collider in colliders){
                if (collider.gameObject.tag == "target"){
                    grabbleItem = collider.gameObject;
                }
            }
        }
        else{
            if (Vector3.Distance(transform.position, grabbleItem.transform.position) > grabDistance){
                grabbleItem = null;
            }
            handTarget.transform.position = grabbleItem.transform.position;
            handTarget.transform.LookAt(grabbleItem.transform,Vector3.left);
            t += Time.deltaTime * 2;
            if (t > 1){
                t = 1;
            }
            twoBoneIKConstraint.weight = t;
        }
    }
}
