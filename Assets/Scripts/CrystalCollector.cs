using UnityEngine;

public class CrystalCollector : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            gameManager.AddCrystal();

            Debug.Log("Kristal: " + gameManager.collectedCrystal + "/" + gameManager.targetCrystal);

            if (gameManager.collectedCrystal >= gameManager.targetCrystal)
            {
                Debug.Log("Oyun bitti!");
                return;
            }

            Vector3 newPosition;
            float distance;

            do
            {
                float randomX = Random.Range(-5f, 5f);
                float randomZ = Random.Range(-5f, 5f);

                newPosition = new Vector3(randomX, 0.5f, randomZ);

                distance = Vector3.Distance(transform.position, newPosition);

            } while (distance < 4f);

            other.transform.position = newPosition;
        }
    }
}