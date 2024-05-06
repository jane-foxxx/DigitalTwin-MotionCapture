using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;
/*
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
*/

public class main : MonoBehaviour
{
    /// <summary> Joints Name
    /// rShldrBend 0, rForearmBend 1, rHand 2, rThumb2 3, rMid1 4,
    /// lShldrBend 5, lForearmBend 6, lHand 7, lThumb2 8, lMid1 9,
    /// lEar 10, lEye 11, rEar 12, rEye 13, Nose 14,
    /// rThighBend 15, rShin 16, rFoot 17, rToe 18,
    /// lThighBend 19, lShin 20, lFoot 21, lToe 22,    
    /// abdomenUpper 23,
    /// hip 24, head 25, neck 26, spine 27
    /// 
    /// </summary>

    private Vector3 initPos;
    public float startdistance;

    Animator animator;
    private Transform root, spine, neck, head, leye, reye, lshoulder, lelbow, lhand, lthumb2, lmid1, rshoulder, relbow, rhand, rthumb2, rmid1, lhip, lknee, lfoot, ltoe, rhip, rknee, rfoot, rtoe;
    private Quaternion midRoot, midSpine, midNeck, midHead, midLshoulder, midLelbow, midLhand, midRshoulder, midRelbow, midRhand, midLhip, midLknee, midLfoot, midRhip, midRknee, midRfoot;
    public Transform nose;

    public UDPReceive udpReceive;
    public float Mul;
    //mediapipe posenet
    public bool isFirst;
    //public float startdistance;
    Dictionary<int, int> media2pose_index = new Dictionary<int, int> {
        {0, 11},
        {1, 13},
        {2, 15},
        {3, 21},
        {4, 17},
        {5, 12},
        {6, 14},
        {7, 16},
        {8, 22},
        {9, 18},
        {10, 7},
        {11, 2},
        {12, 8},
        {13, 5},
        {14, 0},
        {15, 23},
        {16, 25},
        {17, 27},
        {18, 31},
        {19, 24},
        {20, 26},
        {21, 28},
        {22, 32}
    };

    public GameObject[] sket;
    public List<Vector3> pred3dCurrent = new List<Vector3>(33);
    void Start()
    {

        animator = this.GetComponent<Animator>();
  
        root = animator.GetBoneTransform(HumanBodyBones.Hips);
        spine = animator.GetBoneTransform(HumanBodyBones.Spine);
        neck = animator.GetBoneTransform(HumanBodyBones.Neck);
        head = animator.GetBoneTransform(HumanBodyBones.Head);
        leye = animator.GetBoneTransform(HumanBodyBones.LeftEye);
        reye = animator.GetBoneTransform(HumanBodyBones.RightEye);

        lshoulder = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        lelbow = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        lhand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        lthumb2 = animator.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
        lmid1 = animator.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);

        rshoulder = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        relbow = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
        rhand = animator.GetBoneTransform(HumanBodyBones.RightHand);
        rthumb2 = animator.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
        rmid1 = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);

        lhip = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        lknee = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        lfoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        ltoe = animator.GetBoneTransform(HumanBodyBones.LeftToes);

        rhip = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        rknee = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        rfoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        rtoe = animator.GetBoneTransform(HumanBodyBones.RightToes);

        //initPos = new Vector3(vGear.head.transform.position.x, root.position.y, vGear.head.transform.position.z + 1.8f);
       
        initPos = root.position;

        Vector3 forward = TriangleNormal(root.position, lhip.position, rhip.position);
        // Root
        midRoot = Quaternion.Inverse(root.rotation) * Quaternion.LookRotation(forward);

        midSpine = Quaternion.Inverse(spine.rotation) * Quaternion.LookRotation(spine.position - neck.position, forward);
        midNeck = Quaternion.Inverse(neck.rotation) * Quaternion.LookRotation(neck.position - head.position, forward);

        midHead = Quaternion.Inverse(head.rotation) * Quaternion.LookRotation(nose.position - head.position);

        midLshoulder = Quaternion.Inverse(lshoulder.rotation) * Quaternion.LookRotation(lshoulder.position - lelbow.position, forward);
        midLelbow = Quaternion.Inverse(lelbow.rotation) * Quaternion.LookRotation(lelbow.position - lhand.position, forward);
        midLhand = Quaternion.Inverse(lhand.rotation) * Quaternion.LookRotation(
            lthumb2.position - lmid1.position,
            TriangleNormal(lhand.position, lthumb2.position, lmid1.position)
            );
  
        midRshoulder = Quaternion.Inverse(rshoulder.rotation) * Quaternion.LookRotation(rshoulder.position - relbow.position, forward);
        midRelbow = Quaternion.Inverse(relbow.rotation) * Quaternion.LookRotation(relbow.position - rhand.position, forward);
        midRhand = Quaternion.Inverse(rhand.rotation) * Quaternion.LookRotation(
            rthumb2.position - rmid1.position,
            TriangleNormal(rhand.position, rthumb2.position, rmid1.position)
            );

        midLhip = Quaternion.Inverse(lhip.rotation) * Quaternion.LookRotation(lhip.position - lknee.position, forward);
        midLknee = Quaternion.Inverse(lknee.rotation) * Quaternion.LookRotation(lknee.position - lfoot.position, forward);
        midLfoot = Quaternion.Inverse(lfoot.rotation) * Quaternion.LookRotation(lfoot.position - ltoe.position, lknee.position - lfoot.position);
        
        midRhip = Quaternion.Inverse(rhip.rotation) * Quaternion.LookRotation(rhip.position - rknee.position, forward);
        midRknee = Quaternion.Inverse(rknee.rotation) * Quaternion.LookRotation(rknee.position - rfoot.position, forward);
        midRfoot = Quaternion.Inverse(rfoot.rotation) * Quaternion.LookRotation(rfoot.position - rtoe.position, rknee.position - rfoot.position);
    }


    Vector3 TriangleNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = a - b;
        Vector3 d2 = a - c;

        Vector3 dd = Vector3.Cross(d1, d2);
        dd.Normalize();

        return dd;
    }


    void updatePose()
    {
        
        string data = udpReceive.data;
        if (data.Length == 0)
        {
            return;
        }
        string[] points = data.Split(',');
        if (points.Length == 0)
        {
            return;
        }
        List<Vector3> pred3d = new List<Vector3>();
        foreach (KeyValuePair<int, int> kvp in media2pose_index)
        {
            //print(kvp.Key);
            //print(kvp.Value);
            float x = -float.Parse(points[0 + (kvp.Value * 3)]) / 3;
            float y = float.Parse(points[1 + (kvp.Value * 3)]) / 3;
            float z = float.Parse(points[2 + (kvp.Value * 3)]) / 10;
            pred3d.Add(new Vector3(x, y, z));

        }
        /// rShldrBend 0, rForearmBend 1, rHand 2, rThumb2 3, rMid1 4,
        /// lShldrBend 5, lForearmBend 6, lHand 7, lThumb2 8, lMid1 9,
        /// lEar 10, lEye 11, rEar 12, rEye 13, Nose 14,
        /// rThighBend 15, rShin 16, rFoot 17, rToe 18,
        /// lThighBend 19, lShin 20, lFoot 21, lToe 22,    
        /// abdomenUpper 23,
        /// hip 24, head 25, neck 26, spine 27
     
        Vector3 necks = new Vector3((pred3d[5].x + pred3d[0].x) / 2, (pred3d[5].y + pred3d[0].y) / 2, (pred3d[0].z + pred3d[5].z) / 2);
        Vector3 hips = new Vector3((pred3d[15].x + pred3d[19].x) / 2, (pred3d[15].y + pred3d[19].y) / 2, (pred3d[15].z + pred3d[19].z) / 2);
     
        pred3d.Add((hips + necks) / 2);
     
        pred3d.Add((hips + pred3d[23]) / 2);
        Vector3 nhv = Vector3.Normalize((pred3d[10] + pred3d[12]) / 2 - necks);
        Vector3 nv = pred3d[14] - necks;
        Vector3 heads = necks + nhv * Vector3.Dot(nhv, nv);
        pred3d.Add(heads);
     
        pred3d.Add(necks);
     
        pred3d.Add(new Vector3((pred3d[0].x + pred3d[19].x) / 2, (pred3d[0].y + pred3d[19].y) / 2, (pred3d[0].z + pred3d[19].z) / 2));

        for (int i = 0; i <= 32; i++)
        {
            float x = -float.Parse(points[0 + (i * 3)]) / 400;
            float y = float.Parse(points[1 + (i * 3)]) / 400;
            float z = float.Parse(points[2 + (i * 3)]) / 1300;
            if (sket.Length > 0)
            {// sket[i].transform.localPosition = new Vector3(x, y, z);
                sket[i].transform.localPosition = Vector3.Lerp(sket[i].transform.localPosition, new Vector3(x, y, z), Time.deltaTime * 8);

            }



        }
        for (int i = 0; i < pred3d.Count; i++)
        {
            pred3dCurrent[i] = Vector3.Lerp(pred3dCurrent[i], pred3d[i], Time.deltaTime * 8);
        }

        /*
        float tallShin = (Vector3.Distance(pred3dCurrent[16], pred3dCurrent[17]) + Vector3.Distance(pred3dCurrent[20], pred3dCurrent[21])) / 2.0f;
        float tallThigh = (Vector3.Distance(pred3dCurrent[15], pred3dCurrent[16]) + Vector3.Distance(pred3dCurrent[19], pred3dCurrent[20])) / 2.0f;
        float tallUnity = (Vector3.Distance(lhip.position, lknee.position) + Vector3.Distance(lknee.position, lfoot.position)) / 2.0f +
            (Vector3.Distance(rhip.position, rknee.position) + Vector3.Distance(rknee.position, rfoot.position));
        */
        root.position = initPos + new Vector3(hips.x, 0, hips.z) * Mul; 

        if (hips.magnitude > 0.1F && !isFirst)
        {
            initPos -= new Vector3(hips.x, 0, hips.z) * Mul;
            isFirst = true;
            startdistance = (pred3dCurrent[15] - pred3dCurrent[19]).magnitude;
        }

        


      

        Vector3 forward = TriangleNormal(pred3dCurrent[15], pred3dCurrent[24], pred3dCurrent[19]);
        // Root
        //Quaternion quaternion90 = Quaternion.Euler(0, -90, 0);
        root.rotation = Quaternion.LookRotation(forward) * Quaternion.Inverse(midRoot);
    
        //  root.rotation = Quaternion.Inverse( Quaternion.LookRotation(forward)) * Quaternion.Inverse(midRoot);

        spine.rotation = Quaternion.LookRotation(pred3dCurrent[27] - pred3dCurrent[26], forward) * Quaternion.Inverse(midSpine);
        neck.rotation = Quaternion.LookRotation(pred3dCurrent[26] - pred3dCurrent[25], forward) * Quaternion.Inverse(midNeck);
        lshoulder.rotation = Quaternion.LookRotation(pred3dCurrent[5] - pred3dCurrent[6], forward) * Quaternion.Inverse(midLshoulder);
        lelbow.rotation = Quaternion.LookRotation(pred3dCurrent[6] - pred3dCurrent[7], forward) * Quaternion.Inverse(midLelbow);
        rshoulder.rotation = Quaternion.LookRotation(pred3dCurrent[0] - pred3dCurrent[1], forward) * Quaternion.Inverse(midRshoulder);
        relbow.rotation = Quaternion.LookRotation(pred3dCurrent[1] - pred3dCurrent[2], forward) * Quaternion.Inverse(midRelbow);
        lhip.rotation = Quaternion.LookRotation(pred3dCurrent[19] - pred3dCurrent[20], forward) * Quaternion.Inverse(midLhip);
        lknee.rotation = Quaternion.LookRotation(pred3dCurrent[20] - pred3dCurrent[21], forward) * Quaternion.Inverse(midLknee);
        rhip.rotation = Quaternion.LookRotation(pred3dCurrent[15] - pred3dCurrent[16], forward) * Quaternion.Inverse(midRhip);
        rknee.rotation = Quaternion.LookRotation(pred3dCurrent[16] - pred3dCurrent[17], forward) * Quaternion.Inverse(midRknee);
        ////
        head.rotation = Quaternion.LookRotation(pred3dCurrent[14] - pred3dCurrent[25], TriangleNormal(pred3dCurrent[10], pred3dCurrent[12], pred3dCurrent[14])) * Quaternion.Inverse(midHead);
        
        //rhand.rotation = Quaternion.LookRotation(pred3dCurrent[8] - pred3dCurrent[9], TriangleNormal(pred3dCurrent[7], pred3dCurrent[8], pred3dCurrent[9]))* Quaternion.Inverse(midLhand);
        //lhand.rotation = Quaternion.LookRotation(pred3dCurrent[3] - pred3dCurrent[4], TriangleNormal(pred3dCurrent[2], pred3dCurrent[3], pred3dCurrent[4])) * Quaternion.Inverse(midRhand);
        ////
        
        rfoot.rotation = Quaternion.LookRotation(pred3dCurrent[21] - pred3dCurrent[22], pred3dCurrent[20] - pred3dCurrent[21]) * Quaternion.Inverse(midRfoot);
        lfoot.rotation = Quaternion.LookRotation(pred3dCurrent[17] - pred3dCurrent[18], pred3dCurrent[16] - pred3dCurrent[17]) * Quaternion.Inverse(midLfoot);
        //Debug.DrawLine(spine.position, neck.position);
    }

    // Update is called once per frame
    void Update()
    {
        updatePose();

    }
    /*Vector3 MirrorOnYZPlane(Vector3 originalVector)
    {
        return new Vector3(originalVector.x, originalVector.y, originalVector.z);
    }
    */
}
