using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondTriggerSFX : MonoBehaviour
{
    private GameObject playerView = null;

    public AudioSource playSound;

    public List<ViewTargets> vT = new List<ViewTargets>();

    [SerializeField]
    float viewingAngleHori, viewingAngleVert;

    [SerializeField]
    int count = 0;

    [SerializeField]
    bool spooped = false;

    void Start()
    {
        playerView = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private bool between0_90(float a) {
        return (0 <= a) && (a <= 90);
    }

    private bool between270_360(float a) {
        return (270 <= a) && (a < 360);
    }

    private bool checkViewingAngle(float V, float H) {
        bool Vc = false;
        bool Hc = false;
        Debug.Log("Triggered");
        //Debug.Log(string.Format("V = {0}", V));
        //Debug.Log(string.Format("LVA = {0}", lowerYAngle));
        //Debug.Log(string.Format("HVA = {0}", upperYAngle));
        //Debug.Log(string.Format("H = {0}", H));
        //Debug.Log(string.Format("LHA = {0}", lowerXAngle));
        //Debug.Log(string.Format("UHA = {0}", upperXAngle));
        
        ViewTargets t = vT[0];

        // Vertical check
        if (between0_90(t.lowerLimit) && between270_360(t.upperLimit)) {
            Vc = ((V >= t.upperLimit) || (V <= t.lowerLimit));
        }
        else if (between0_90(t.lowerLimit) && between0_90(t.upperLimit)) {
            Vc = ((V <= t.lowerLimit) && (V >= t.upperLimit));
        }
        else if (between270_360(t.lowerLimit) && between270_360(t.upperLimit)) {
            Vc = ((V >= t.upperLimit) && (V <= t.lowerLimit));
        }

        // Horizontal check
        if (t.leftLimit < t.rightLimit) {
            Hc = ((t.leftLimit <= H) && (H <= t.rightLimit));
        }
        else if (t.leftLimit > t.rightLimit) {
            Hc = !((t.rightLimit <= H) && (H <= t.leftLimit));
        }

        Debug.Log(string.Format("Hc = {0}", Hc));
        Debug.Log(string.Format("Vc = {0}", Vc));

        //return (((lowerYAngle > V) && (V > upperYAngle)) && ((lowerXAngle < H) && (H < upperXAngle)));
        return Vc && Hc;
    }

    void OnTriggerEnter(Collider other) {
        //checkViewingAngle(viewingAngleVert, viewingAngleHori);
        if (!spooped) {
            viewingAngleHori = Camera.main.transform.eulerAngles.y; // H
            viewingAngleVert = Camera.main.transform.eulerAngles.x; // V
            Debug.Log("viewingAngleHori:" + viewingAngleHori);
            Debug.Log("viewingAngleVert:" + viewingAngleVert);
            if (checkViewingAngle(viewingAngleVert, viewingAngleHori)) {
                playSound.Play();
                spooped = !spooped;
                count++;
            }
        }
    }
}

[System.Serializable] 
public class ViewTargets
{
    [Tooltip("Upper Limit must be above Lower Limit. Left Limit must be to the left of Right Limit")]
    public int upperLimit, lowerLimit, leftLimit, rightLimit;
    //lowerLimit: 90 is looking at floor and looking up decreases value to 0
    //upperLimit: 270 us looking at ceiling and looking down increases value to 360
    //in case of both being between 270-360: upperLimit > lowerLimit
    //in case of both being between 0-90: upperLimit < lowerLimit
    //leftLimit < rightLimit, ie. leftLimit must be counterclock-wise in relation to rightLimit
}

/* USAGE GUIDE
 * 1. Create cube that acts as a speaker for some audio
 * 2. Add audio source to cube and select audio clip (Make sure audio source has spatial blend = 1)
 * 3. Create cube that acts as a trigger
 * 4. Add this script to the cube
 * 5. Open dropdown menu VT (viewing targets)
 * 6. Use "Viewing angle hori" and "Viewing angle vert" to determine direction that player should be looking.
 * 7. Drag the speaker cube from the hiararchy to "Play Sound" under the script.
 * obs. make sure to add VolumeLevel script to any audio source
 */