using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Seo é cód le pacáiste teanga. Tá sé in ann:
///     comhaid pacáiste teanga i bhformáid .csv a oscailt
///     frásaí i dteangacha éagsúil a lódáil chun códuimhir uathúil
///     
/// Caitheann gach comhad anim na teanga (no cód na teanga) a beith aige.
///     Mar shampla, beidh anim an pacáiste Gaeilge "Gaeilge.csv" nó "ga.csv"
///     Formáid an chomhaid:
///         Códfhocal-no-Códuimhir,Focal-nó-frása
///         ...
///     Mar shampla:
///         1,Dia duit
///         134,Dia daoibh
///         8345,Conas atá sibh? Tá mé go maith
///         [Deireadh]
/// </summary>
namespace PacáisteTeanga
{
    /// <summary>
    /// Seo é an cód a lódáileann comhaid teanga ón ríomhaire.
    /// </summary>
    public class Pacáiste
    {
        readonly Dictionary<int, string> nascaireacht;
        public string Teanga
        {
            get;
            private set;
        }

        /// <summary>
        /// Déanann sé seo Pacáiste nua chun an comhad teanga atá aimsithe
        /// </summary>
        /// <param name="ainmAnChomhaid">
        /// An comhad lena teangacha i bhformáid ceart (féach an tuarscáil atá suas ag barr an comhad)
        /// </param>
	    public Pacáiste(string ainmAnChomhaid)
        {
            nascaireacht = new Dictionary<int, string>();
            ComhadALódáil(ainmAnChomhaid);
        }

        /// <summary>
        /// Lódáileann sé seo an comhad i bhfoclóir frásaí
        /// </summary>
        /// <param name="comhad">
        /// An comhad leis an teanga i bhformáid .csv
        /// </param>
        void ComhadALódáil(string ainmAnChomhaid)
        {
            //anim na teanga (gan an fadú .csv)
            Teanga = ainmAnChomhaid.Split('.')[0];

            //Lódáil gach frása i nascaireachtTeangacha
            try
            {
                foreach (string líne in File.ReadLines(ainmAnChomhaid))
                {
                    //Tá dhá phíosa eolais sa líne: an cód agus an frása
                    string[] eolas = líne.Split(',');

                    //Tá eolas[0] an cód agus eolas[1] an frása
                    int cód = Int32.Parse(eolas[0]);
                    string frása = eolas[1];
                    nascaireacht.Add(cód, frása);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception("Níl formáid maith ag an gcomhad seo\n");
            }
        }

        /// <summary>
        /// Athraíonn sé seo an teanga ar fáil
        /// </summary>
        /// <param name="comhad">
        /// An comhad leis an teanga i bhformáid .csv
        /// </param>
        public void TeangaAAthrú(string ainmAnChomhaid)
        {
            nascaireacht.Clear();
            ComhadALódáil(ainmAnChomhaid);
        }

        /// <summary>
        /// Fillean sé seo frása an chóid faighte
        /// </summary>
        /// <param name="cód">Cód na fhrása teastaithe</param>
        /// <returns>
        /// Frása leis an cód seo, nó "null" mura bhfuil aon frása aimsithe
        /// </returns>
        public string FrásaAFháil(int cód)
        {
            nascaireacht.TryGetValue(cód, out string frása);
            return frása;
        }
    }
}