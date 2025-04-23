using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleJoueur : MonoBehaviour
{
    [SerializeField] private float vitesse = 5f; // Vitesse du joueur  
    [SerializeField] private float vitesseRotation = 100f; // Vitesse de rotation du joueur  

    private Vector2 deplacement; // D�placement du joueur.  
    private Rigidbody rigidbodyJoueur;
    private float rotation; // Rotation autour de l'axe Y  

    private void Start()
    {
        rigidbodyJoueur = GetComponent<Rigidbody>(); // Initialiser le Rigidbody  
    }

    private void FixedUpdate()
    {
        // Calculer le mouvement en fonction de l'input  
        Vector3 mouvement = (deplacement.y * transform.forward + deplacement.x * transform.right).normalized; // Avant/arri�re et gauche/droite  
        rigidbodyJoueur.MovePosition(rigidbodyJoueur.position + mouvement * vitesse * Time.fixedDeltaTime);

        Quaternion deltaRotation = Quaternion.Euler(0, rotation * vitesseRotation * Time.fixedDeltaTime, 0);
        rigidbodyJoueur.MoveRotation(rigidbodyJoueur.rotation * deltaRotation);

    }

    public void Deplacer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Console.WriteLine("deplacement"); // Afficher un message dans la console pour le d�bogage
            // Lire les valeurs d'input pour le mouvement  
            deplacement = context.action.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            Console.WriteLine("arret deplacement"); // Afficher un message dans la console pour le d�bogage
            deplacement = Vector2.zero; // R�initialiser le mouvement si l'entr�e est annul�e  
        }
    }

    public void Rotater(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Console.WriteLine("rotation"); // Afficher un message dans la console pour le d�bogage
            rotation = context.action.ReadValue<Vector2>().x; // Assurez-vous que cette action renvoie un float pour la rotation  
        }
        else if (context.canceled)
        {
            Console.WriteLine("arret rotation");
            rotation = 0; // R�initialiser la rotation si l'entr�e est annul�e  
        }
    }
}