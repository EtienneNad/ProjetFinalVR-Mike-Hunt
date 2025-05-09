using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DonneeJoueur : MonoBehaviour
{
    public static DonneeJoueur Instance { get; private set; }

    [SerializeField, Tooltip("Nombre d'animaux a tuer")]
    private int nombreAnimauxRestant;

    [SerializeField, Tooltip("Canvas du menu")]
    private GameObject menu;

    [SerializeField, Tooltip("Bouton pour recommencer le jeu")]
    private Button boutonRecommencer;

    [SerializeField, Tooltip("Bouton pour quitter le jeu")]
    private Button boutonQuitter;

    [SerializeField, Tooltip("Valeur actuel du nombre d'animaux restant")]
    private UnityEvent<int[]> valeurNombreAnimauxRestant;

    bool menuOuvert = false;


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

        // Ajout des listeners aux boutons
        if (boutonRecommencer != null)
            boutonRecommencer.onClick.AddListener(RecommencerJeu);

        if (boutonQuitter != null)
            boutonQuitter.onClick.AddListener(QuitterJeu);
    }
    /// <summary>
    /// M�thode Update pour v�rifier les entr�es de l'utilisateur et mettre � jour l'interface utilisateur.
    /// </summary>
    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.Start) && menuOuvert || nombreAnimauxRestant <= 0)
        {
            AfficherMenu();
        }
        if(OVRInput.Get(OVRInput.RawButton.Start) && !menuOuvert)
        {
            FermerMenu();
        }
        
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }

    /// <summary>
    /// M�thode pour afficher le menu de pause.
    /// </summary>
    private void AfficherMenu()
    {
        if (menu != null)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            menuOuvert = true;
        }
    }

    public void FermerMenu()
    {
        if (menu != null)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
            menuOuvert = false;
        }
    }

    public void RecommencerJeu()
    {
        Time.timeScale = 1f; // R�active le temps avant de recharger
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitterJeu()
    {
        Application.Quit();
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
