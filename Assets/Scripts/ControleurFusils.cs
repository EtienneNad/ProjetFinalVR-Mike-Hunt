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
    private bool enAttaque;

    [SerializeField] private DistanceHandGrabInteractable interactable;

    private HandGrabInteractor mainQuiTient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        interactable.WhenPointerEventRaised += OnGrabChanged;
    }

    void OnDestroy()
    {
        interactable.WhenPointerEventRaised -= OnGrabChanged;
    }

    void Update()
    {
        if (mainQuiTient == null || enAttaque)
            return;

        // Déterminer la main via l'interactor
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            onTirer();
        }
        //mainQuiTient.gameObject.name.ToLower().Contains("left")
        else if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            onTirer();
        }
    }

    private void OnGrabChanged(PointerEvent pointerEvent)
    {
        if (pointerEvent.Type == PointerEventType.Select)
        {
            mainQuiTient = pointerEvent.Data as HandGrabInteractor;
        }
        else if (pointerEvent.Type == PointerEventType.Unselect)
        {
            mainQuiTient = null;
        }
    }

    /// <summary>
    /// Tirer une balle
    /// </summary>
    public void onTirer()
    {
        enAttaque = true;
        StartCoroutine(EffetTir());
    }
    
    /// <summary>
    /// Coroutine pour tirer une balle
    /// </summary>
    /// <returns>Un délai de une seconde entre chaque tir.</returns>
    private IEnumerator EffetTir()
    {
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
