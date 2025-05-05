using System.Collections;
using UnityEngine;

public class GenerateurAnimaux : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab des animaux")]
    private GameObject[] animaux;

    [SerializeField, Tooltip("Emplacement ou peut apparaitre les animaux.")]
    private Transform[] emplacementPredefini;


    [SerializeField, Tooltip("Nombre d'animaux a instancier")]
    private int nombreAnimaux = 10;

    private bool coroutineEnCours = false;

    /// <summary>
    /// Méthode appelée au départ
    /// </summary>
    void Start()
    {
        GenererAnimaux();
    }

   /// <summary>
   /// Méthode appelée une fois par frame
   /// </summary>   
    void Update()
    {
        int nombreAnimauxActuels = transform.childCount;
        if (nombreAnimauxActuels < nombreAnimaux && !coroutineEnCours)
        {
            StartCoroutine(GenereAnimalAvecDelai());
        }
    }

    /// <summary>
    /// Méthode pour générer un nombre fixe d'animaux
    /// </summary>
    private void GenererAnimaux()
    {
        for (int i = 0; i < nombreAnimaux; i++)
        {
            GenereAnimal();
        }
    }

    /// <summary>
    /// Méthode pour générer un animal à une position aléatoire
    /// </summary>
    private void GenereAnimal()
    {
        
            GameObject animal = Instantiate(animaux[Random.Range(0, animaux.Length)],
            emplacementPredefini[Random.Range(0, emplacementPredefini.Length)].position, Quaternion.identity);
            animal.transform.parent = transform;
        
       
    }

    /// <summary>
    /// Coroutine pour générer un animal après un délai
    /// </summary>
    /// <returns>délais de 2 seconde</returns>
    private IEnumerator GenereAnimalAvecDelai()
    {
        coroutineEnCours = true; // Indique que la coroutine est en cours  
        yield return new WaitForSeconds(2f);
        GenereAnimal(); // Générer l'animal après 1 seconde  
        coroutineEnCours = false; // Réinitialiser l'état de la coroutine  
    }
}