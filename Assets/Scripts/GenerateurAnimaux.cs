using System.Collections;
using UnityEngine;

public class GenerateurAnimaux : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animaux;

    [SerializeField]
    private int nombreAnimaux = 10;

    private bool coroutineEnCours = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        GenererAnimaux();
    }

    // Update is called once per frame  
    void Update()
    {
        int nombreAnimauxActuels = transform.childCount;
        if (nombreAnimauxActuels < nombreAnimaux && !coroutineEnCours)
        {
            StartCoroutine(GenereAnimalAvecDelai());
        }
    }

    private void GenererAnimaux()
    {
        for (int i = 0; i < nombreAnimaux; i++)
        {
            GenereAnimal();
        }
    }

    private void GenereAnimal()
    {
        GameObject animal = Instantiate(animaux[Random.Range(0, animaux.Length)],
            new Vector3(Random.Range(-150f, 150f), 0.0f, Random.Range(-150f, 150f)), Quaternion.identity);
        animal.transform.parent = transform;
    }

    private IEnumerator GenereAnimalAvecDelai()
    {
        coroutineEnCours = true; // Indique que la coroutine est en cours  
        yield return new WaitForSeconds(2f);
        GenereAnimal(); // Générer l'animal après 1 seconde  
        coroutineEnCours = false; // Réinitialiser l'état de la coroutine  
    }
}