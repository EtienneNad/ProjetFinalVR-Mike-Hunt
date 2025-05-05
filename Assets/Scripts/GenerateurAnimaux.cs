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
    /// M�thode appel�e au d�part
    /// </summary>
    void Start()
    {
        GenererAnimaux();
    }

   /// <summary>
   /// M�thode appel�e une fois par frame
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
    /// M�thode pour g�n�rer un nombre fixe d'animaux
    /// </summary>
    private void GenererAnimaux()
    {
        for (int i = 0; i < nombreAnimaux; i++)
        {
            GenereAnimal();
        }
    }

    /// <summary>
    /// M�thode pour g�n�rer un animal � une position al�atoire
    /// </summary>
    private void GenereAnimal()
    {
        
            GameObject animal = Instantiate(animaux[Random.Range(0, animaux.Length)],
            emplacementPredefini[Random.Range(0, emplacementPredefini.Length)].position, Quaternion.identity);
            animal.transform.parent = transform;
        
       
    }

    /// <summary>
    /// Coroutine pour g�n�rer un animal apr�s un d�lai
    /// </summary>
    /// <returns>d�lais de 2 seconde</returns>
    private IEnumerator GenereAnimalAvecDelai()
    {
        coroutineEnCours = true; // Indique que la coroutine est en cours  
        yield return new WaitForSeconds(2f);
        GenereAnimal(); // G�n�rer l'animal apr�s 1 seconde  
        coroutineEnCours = false; // R�initialiser l'�tat de la coroutine  
    }
}