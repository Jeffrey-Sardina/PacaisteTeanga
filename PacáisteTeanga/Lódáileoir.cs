using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Seo é cód le pacáiste teanga. Tá sé in ann:
///     comhaid pacáiste teanga i bhformáid speisialta a oscailt
///     frásaí i dteangacha éagsúil a lódáil chun códuimhir uathúil
///     
/// Caitheann gach comhad an formáid seo a beith aige:
///     [Tosú]
///     Teanga1,Teanga2,...,TeangaN
///     Códfhocal-no-Códuimhir:Focal-I-dTeanga-1,Focal-I-dTeanga-2,...,Focal-I-dTeanga-N
///     ...
///     [Deireadh]
///     Mar shampla
///         Gaeilge,Béarla
///         beannú:Dia duit,Hello
///         beannú do cúpla daoine:Dia daoibh,Hello
///         8345:Conas atá sibh? Tá mé go maith, ach nílim inn ann mo mhadra a aimsiú!,How are you? I am good, but I am not able to find my dog!
/// </summary>
namespace PacáisteTeanga
{
    /// <summary>
    /// Seo é an cód a lódáileann comhaid teanga ón ríomhaire.
    /// </summary>
    public class Lódáileoir
    {
        readonly Dictionary<string, string[]> nascaireachtTeangacha;
        int uimhirTeangacha;

        /// <summary>
        /// Déanann sé seo Lódáileoir nua chun an comhad teanga atá aimsithe
        /// </summary>
        /// <param name="comhad">
        /// An comhad lena teangacha i bhformáid ceart (féach an tuarscáil atá suas ag barr an comhad)
        /// </param>
	    public Lódáileoir(string comhad)
        {
            nascaireachtTeangacha = new Dictionary<string, string[]>();
            LódáilComhad(comhad);
        }

        /// <summary>
        /// Lódáileann sé seo an comhad i bhfoclóir frásaí a úsáideann an cód seo
        /// </summary>
        /// <param name="comhad">
        /// An comhad lena teangacha i bhformáid ceart (féach an tuarscáil atá suas ag barr an comhad)
        /// </param>
        void LódáilComhad(string comhad)
        {
            string[] línte = File.ReadAllLines(comhad);

            //An céad líne--lódáil na teanghacha atá ann
            string[] teangacha = línte[0].Split(',');
            uimhirTeangacha = teangacha.Length;

            //Lódáil gach frása i nascaireachtTeangacha
            try
            {
                foreach (string líne in línte)
                {
                    string[] sealadach = líne.Split(':');
                    string cód = sealadach[0];
                    string[] frásaí = sealadach[1].Split(',');

                    nascaireachtTeangacha.Add(cód, frásaí);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception("Níl formáid maith ag an gcomhad seo\n");
            }
        }
    }
}