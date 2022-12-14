using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleType : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;

    [SerializeField] private float TimeBtwnCharacters = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextVisible());
    }

    private IEnumerator TextVisible(){
        int totalVisibleCharacters = textMesh.text.Length;
        int counter = 0;

        while(true) {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMesh.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters){
                yield break;
            }

            counter += 1;
            yield return new WaitForSeconds(TimeBtwnCharacters);
        }
    }
}
