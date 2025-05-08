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
    /// Méthode Awake pour initialiser l'instance de la classe DonneeJoueur.
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
    /// Méthode Start pour initialiser le nombre d'animaux restant et afficher la valeur actuelle.
    /// </summary>
    private void Start()
    {
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }
    /// <summary>
    /// Méthode Update pour vérifier les entrées de l'utilisateur et mettre à jour l'interface utilisateur.
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
    /// Méthode pour afficher le menu de pause.
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
    /// Méthode pour afficher le menu de fin.
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
    ///  Méthode pour retirer un animal au nombre d'animaux restant.
    /// </summary>
    public void EnleverAnimaux()
    {
        nombreAnimauxRestant -= 1;
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());
    }

    /// <summary>
    ///  Méthode pour modifier la valeur du nombre d'animaux restant.
    /// </summary>
    /// <returns>la nouvelle valeur du nombre d'animaux</returns>
    private int[] ModificationNombreAnimauxRestant()
    {
        return new int[] {nombreAnimauxRestant};
    }
}
