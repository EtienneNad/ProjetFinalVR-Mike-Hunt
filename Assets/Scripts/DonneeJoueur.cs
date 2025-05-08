using UnityEngine;
using UnityEngine.Events;

public class DonneeJoueur : MonoBehaviour
{
    public static DonneeJoueur Instance { get; private set; }

    [SerializeField, Tooltip("Nobre d'animaux a tuer")]
    private int nombreAnimauxRestant;

    [SerializeField, Tooltip("Canvas du menu pause")]
    private CanvasGroup menuPause;

    [SerializeField, Tooltip("canvas du menu fin")]
    private CanvasGroup menuFin; 

    [SerializeField, Tooltip("Valeur actuel du nombre d'animaux restant")]
    private UnityEvent<int[]> valeurNombreAnimauxRestant;


    /// <summary>
    /// M�thode Awake pour initialiser l'instance de la classe DonneeJoueur.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// M�thode Start pour initialiser le nombre d'animaux restant et afficher la valeur actuelle.
    /// </summary>
    private void Start()
    {
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }
    /// <summary>
    /// M�thode Update pour v�rifier les entr�es de l'utilisateur et mettre � jour l'interface utilisateur.
    /// </summary>
    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.Start))
        {
            AfficherMenuPause();
        }
        if (nombreAnimauxRestant <= 0)
        {
            AfficherMenuFin();
        }
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }

    /// <summary>
    /// M�thode pour afficher le menu de pause.
    /// </summary>
    private void AfficherMenuPause()
    {
        if (menuPause != null)
        {
            menuPause.alpha = 1;
            menuPause.interactable = true;
            menuPause.blocksRaycasts = true;
            Time.timeScale = 0;

        }
    }
    /// <summary>
    /// M�thode pour afficher le menu de fin.
    /// </summary>
    private void AfficherMenuFin()
    {
        if (menuPause != null)
        {
            menuFin.alpha = 1;
            menuFin.interactable = true;
            menuFin.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }

    /// <summary>
    ///  M�thode pour retirer un animal au nombre d'animaux restant.
    /// </summary>
    public void EnleverAnimaux()
    {
        nombreAnimauxRestant -= 1;
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());
    }

    /// <summary>
    ///  M�thode pour modifier la valeur du nombre d'animaux restant.
    /// </summary>
    /// <returns>la nouvelle valeur du nombre d'animaux</returns>
    private int[] ModificationNombreAnimauxRestant()
    {
        return new int[] {nombreAnimauxRestant};
    }
}
