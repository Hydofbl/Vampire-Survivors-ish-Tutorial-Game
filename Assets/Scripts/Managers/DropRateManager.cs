using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drop
    {
        public string Name;
        public GameObject ItemPrefab;
        public float DropRate;
    }

    public List<Drop> DropList;

    // When an enemy destroyed, looks for any possible drops
    private void OnDestroy()
    {
        // if scene is not loaded (exiting Play mode), pass this part.
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        float randomNumber = Random.Range(0f, 100f);

        List<Drop> possibleDrops = new List<Drop>();

        foreach (Drop drop in DropList)
        {
            if(randomNumber <= drop.DropRate)
            {
                possibleDrops.Add(drop);
            }
        }

        if(possibleDrops.Count > 0)
        {
            GameObject dropPrefab = possibleDrops[Random.Range(0, possibleDrops.Count)].ItemPrefab;
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }   
    }
}
