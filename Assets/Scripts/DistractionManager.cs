using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionManager : MonoBehaviour
{
    public GameObject DistractionPrefab;
    public GameObject DistractionHolder;
    public GameObject DistractionBlocker;
    public bool isRunning;
    public int PrefabHealth;
    public float SpawnInterval;

    private void Awake() {
        isRunning = false;
    }
    
    private void Update() {
        if(GameObject.FindGameObjectWithTag("Distraction") != null) {
            DistractionBlocker.SetActive(true);
        } else {
            DistractionBlocker.SetActive(false);
        }
    }

    public IEnumerator SpawnDistractions() {
        isRunning = true;
        while(StudySceneManager.instance.attentionSpanManager.BrainRotLevel >= 1) {
            yield return new WaitForSecondsRealtime(SpawnInterval);
            Vector3 SpawnPos = new Vector3(Random.Range(0f, Screen.width),Random.Range(0f, Screen.height),0f);
            DistractionPrefab.GetComponent<Distraction>().Health = PrefabHealth;
            GameObject SpawnedDistraction = Instantiate(DistractionPrefab, Camera.main.ScreenToWorldPoint(SpawnPos),Quaternion.identity, DistractionHolder.transform);
            SpawnedDistraction.transform.localPosition = new Vector3(SpawnedDistraction.transform.localPosition.x, SpawnedDistraction.transform.localPosition.y, 0f);
        }
    }
}
