using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Drawing;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ControlleurFusil : MonoBehaviour
{
    /// <summary>
    /// Emplacement du canon
    /// </summary>
    [SerializeField]
    private Transform canon;

    /// <summary>
    /// Prefab d'une balle
    /// </summary>
    [SerializeField]
    private GameObject prefabBalle;


    /// <summary>
    /// Vitesse de la balle
    /// </summary>
    [SerializeField]
    private float vitesseBalle = 20f;

    /// <summary>
    /// Booléen qui représente si le joueur est entrain de tirer. Sers à mettre un délai entre les balles.
    /// </summary>
    private bool enAttaque = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {


        // Déterminer la main via l'interactor
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            if (!enAttaque) // Vérifier si on n'est pas déjà en train de tirer  
            {
                onTirer(); // Tirer seulement si on n'est pas déjà en train de tirer  
            }
        }
        //mainQuiTient.gameObject.name.ToLower().Contains("left")
        else if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            if (!enAttaque) // Vérifier si on n'est pas déjà en train de tirer  
            {
                onTirer(); // Tirer seulement si on n'est pas déjà en train de tirer  
            }
        }
    }

    /// <summary>
    /// Tirer une balle
    /// </summary>
    public void onTirer()
    {
        //enAttaque = true;
        AudioSource sourceaudio = GetComponent<AudioSource>();
        sourceaudio.Play();
        StartCoroutine(EffetTir());
    }

    /// <summary>
    /// Coroutine pour tirer une balle
    /// </summary>
    /// <returns>Un délai de une seconde entre chaque tir.</returns>
    private IEnumerator EffetTir()
    {
        enAttaque = true; 
        //Créer la balle
        GameObject balleExistante = Instantiate(prefabBalle);
        balleExistante.transform.position = canon.position;
        balleExistante.transform.rotation = canon.rotation;
        //vitesse et direction de la balle
        balleExistante.GetComponent<Rigidbody>().AddForce(canon.forward * vitesseBalle, ForceMode.Impulse);

        //La détruire après 10 secondes
        Destroy(balleExistante, 10);
        yield return new WaitForSeconds(1f);
        enAttaque = false;
    }
}