using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyControllerDestroyer : MonoBehaviour
{
    private DifficultyController difficultyController;

    private void Start()
    {
        difficultyController = FindAnyObjectByType<DifficultyController>();

        if(difficultyController != null)
        {
            Destroy(difficultyController.gameObject);
            Debug.Log("Destroyed difficultyController");
        }
    }
}
