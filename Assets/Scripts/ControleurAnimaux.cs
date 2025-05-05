using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ControleurAnimaux : MonoBehaviour
{
    private Vector3 destination;// Destination de l'animal
    private NavMeshAgent agent; // R�f�rence au NavMeshAgent

    private Vector3 destinationDepart;// Destination de d�part de l'animal

    [SerializeField, Tooltip("nombre de vie de l'animal")]
    private float vie = 3f;
    private bool coroutineEnCours = false; // Bool�en pour v�rifier si la coroutine est d�j� en cours  

    /// <summary>
    /// M�thode appel�e au d�part
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
    /// M�thode pour g�n�rer une destination al�atoire
    /// </summary>
    /// <returns>La nouvelle destination</returns>
    private Vector3 DeplacementAleatoire()
    {
        return new Vector3(Random.Range(-150f, 150f), 0.0f, Random.Range(-150f, 150f));
    }

    /// <summary>
    ///  M�thode appel�e une fois par frame
    /// </summary>
    void Update()
    {
        // V�rifier si la destination a �t� atteinte ou si l'agent est suffisamment proche de la destination  
        if (!agent.pathPending)
        {
            // Choisir une nouvelle destination al�atoire  
            destination = DeplacementAleatoire();
            agent.SetDestination(destination);
        }

        // D�marrer la coroutine seulement si elle n'est pas d�j� en cours  
        if (!coroutineEnCours)
        {
            StartCoroutine(ChangerDestinationApresDelai());
        }

        // V�rifier si la vie de l'animal est inf�rieure ou �gale � 0
        if(vie <= 0)
        {
            Destroy(gameObject); // D�truire l'objet si la vie est inf�rieure ou �gale � 0  
        }
    }

    /// <summary>
    /// M�thode permettant de v�rifier si l'animal se fait tirer
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
    /// Coroutine permetant de changer la destination apr�s un d�lai
    /// </summary>
    /// <returns>d�lais de 15 secondes</returns>
    private IEnumerator ChangerDestinationApresDelai()
    {
        coroutineEnCours = true; // Indiquer que la coroutine est en cours d'ex�cution  

        yield return new WaitForSeconds(15f); // Attendre 15 secondes  

        // Choisir une nouvelle destination al�atoire  
        destination = DeplacementAleatoire();
        agent.SetDestination(destination);
        Debug.Log("Nouvelle destination apr�s 15 secondes: " + destination);

        coroutineEnCours = false; // R�initialiser le bool�en indiquant que la coroutine est termin�e  
    }
}