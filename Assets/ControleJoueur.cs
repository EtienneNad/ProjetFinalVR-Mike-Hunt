using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleJoueur : MonoBehaviour
{
    [SerializeField] private float vitesse = 5f; // Vitesse du joueur  
    [SerializeField] private float vitesseRotation = 100f; // Vitesse de rotation du joueur  

    private Vector2 deplacement; // Déplacement du joueur.  
    private Rigidbody rigidbodyJoueur;
    private float rotation; // Rotation autour de l'axe Y  

    private void Start()
    {
        rigidbodyJoueur = GetComponent<Rigidbody>(); // Initialiser le Rigidbody  
    }

    private void FixedUpdate()
    {
        // Calculer le mouvement en fonction de l'input  
        Vector3 mouvement = (deplacement.y * transform.forward + deplacement.x * transform.right).normalized; // Avant/arrière et gauche/droite  
        rigidbodyJoueur.MovePosition(rigidbodyJoueur.position + mouvement * vitesse * Time.fixedDeltaTime);

        Quaternion deltaRotation = Quaternion.Euler(0, rotation * vitesseRotation * Time.fixedDeltaTime, 0);
        rigidbodyJoueur.MoveRotation(rigidbodyJoueur.rotation * deltaRotation);

    }

    public void Deplacer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Console.WriteLine("deplacement"); // Afficher un message dans la console pour le débogage
            // Lire les valeurs d'input pour le mouvement  
            deplacement = context.action.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            Console.WriteLine("arret deplacement"); // Afficher un message dans la console pour le débogage
            deplacement = Vector2.zero; // Réinitialiser le mouvement si l'entrée est annulée  
        }
    }

    public void Rotater(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Console.WriteLine("rotation"); // Afficher un message dans la console pour le débogage
            rotation = context.action.ReadValue<Vector2>().x; // Assurez-vous que cette action renvoie un float pour la rotation  
        }
        else if (context.canceled)
        {
            Console.WriteLine("arret rotation");
            rotation = 0; // Réinitialiser la rotation si l'entrée est annulée  
        }
    }
}