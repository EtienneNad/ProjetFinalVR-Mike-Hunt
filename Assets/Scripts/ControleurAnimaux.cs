using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ControleurAnimaux : MonoBehaviour
{
    private Vector3 destination;// Destination de l'animal
    private NavMeshAgent agent; // Référence au NavMeshAgent

    private Vector3 destinationDepart;// Destination de départ de l'animal

    [SerializeField, Tooltip("nombre de vie de l'animal")]
    private float vie = 3f;
    private bool coroutineEnCours = false; // Booléen pour vérifier si la coroutine est déjà en cours  

    /// <summary>
    /// Méthode appelée au départ
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destinationDepart = DeplacementAleatoire();
        agent.transform.position = destinationDepart;
        destination = DeplacementAleatoire();
        agent.SetDestination(destination);
    }

    /// <summary>
    /// Méthode pour générer une destination aléatoire
    /// </summary>
    /// <returns>La nouvelle destination</returns>
    private Vector3 DeplacementAleatoire()
    {
        return new Vector3(Random.Range(-150f, 150f), 0.0f, Random.Range(-150f, 150f));
    }

    /// <summary>
    ///  Méthode appelée une fois par frame
    /// </summary>
    void Update()
    {
        // Vérifier si la destination a été atteinte ou si l'agent est suffisamment proche de la destination  
        if (!agent.pathPending)
        {
            // Choisir une nouvelle destination aléatoire  
            destination = DeplacementAleatoire();
            agent.SetDestination(destination);
        }

        // Démarrer la coroutine seulement si elle n'est pas déjà en cours  
        if (!coroutineEnCours)
        {
            StartCoroutine(ChangerDestinationApresDelai());
        }

        // Vérifier si la vie de l'animal est inférieure ou égale à 0
        if(vie <= 0)
        {
            Destroy(gameObject); // Détruire l'objet si la vie est inférieure ou égale à 0  
        }
    }

    /// <summary>
    /// Méthode permettant de vérifier si l'animal se fait tirer
    /// </summary>
    /// <param name="collision">l'animal perd une vie</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Balle"))
        {
            vie -= 1f;
        }
    }
    /// <summary>
    /// Coroutine permetant de changer la destination après un délai
    /// </summary>
    /// <returns>délais de 15 secondes</returns>
    private IEnumerator ChangerDestinationApresDelai()
    {
        coroutineEnCours = true; // Indiquer que la coroutine est en cours d'exécution  

        yield return new WaitForSeconds(15f); // Attendre 15 secondes  

        // Choisir une nouvelle destination aléatoire  
        destination = DeplacementAleatoire();
        agent.SetDestination(destination);
        Debug.Log("Nouvelle destination après 15 secondes: " + destination);

        coroutineEnCours = false; // Réinitialiser le booléen indiquant que la coroutine est terminée  
    }
}