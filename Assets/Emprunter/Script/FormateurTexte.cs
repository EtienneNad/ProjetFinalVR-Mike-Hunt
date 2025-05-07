/// Inspirer du contenu vue en cours, fait par: Alexandre Ouellet et modifier par: �tienne Nadeau.
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Formatte plusieurs valeurs selon une cha�ne d�termin�e. Les �l�ments � substituer sont indiqu� par {x} ou x est un nombre plus grand ou �gal � 1
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class FormateurTexte : MonoBehaviour
{
    /// <summary>
    /// Mod�le de texte qui doit �tre utilis� pour la substitution
    /// </summary>
    private string modeleTexte;

    private void Awake()
    {
        modeleTexte = GetComponent<TextMeshProUGUI>().text;
    }

    /// <summary>
    /// Modifie le texte affich� pour substituer toutes les occurences de {x} par la valeur dans le tableau. Les valeurs doivent �tre cons�cutives
    /// </summary>
    /// <param name="valeurs">Les valeurs � substituer dans la chaine.</param>
    public void FormatterValeursFloat(params float[] valeurs)
    {
        int index = 0;
        string chaine = modeleTexte;

        while (index < valeurs.Length && Substituer(valeurs[index], index + 1, ref chaine))
        {
            index++;
        }

        GetComponent<TextMeshProUGUI>().text = chaine;
    }
    /// <summary>
    /// Modifie le texte affich� pour substituer toutes les occurences de {x} par la valeur dans le tableau. Les valeurs doivent �tre cons�cutives
    /// </summary>
    /// <param name="valeurs">Les valeurs � substituer dans la chaine.</param>
    public void FormatterValeursInt(params int[] valeurs)
    {
        int index = 0;
        string chaine = modeleTexte;

        while (index < valeurs.Length && Substituer(valeurs[index], index + 1, ref chaine))
        {
            index++;
        }

        GetComponent<TextMeshProUGUI>().text = chaine;
    }

    /// <summary>
    /// Remplace la s�quence {indice} dans cha�ne par la valeur indiqu�e et retourne vrai si la substitution a eu lieu
    /// </summary>
    /// <param name="valeur">La valeur � remplacer</param>
    /// <param name="indice">L'indice auquel faire le remplacement</param>
    /// <param name="chaine">La cha�ne dans laquelle faire la substitution</param>
    /// <returns>True si la substitution a eu lieu, false autrement</returns>
    private bool Substituer(object valeur, int indice, ref string chaine)
    {
        string valeurChaine = valeur.ToString();

        chaine = chaine.Replace($"{{{indice}}}", valeurChaine);
        return true;
    }
}

