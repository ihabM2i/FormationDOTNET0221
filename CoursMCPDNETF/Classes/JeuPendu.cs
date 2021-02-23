using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    public class JeuPendu
    {
        #region Attributs
        private IGenerateur generateur;
        private string motATrouve;
        private int nbEssai;
        private string masque;
        #endregion

        #region Constructeur
        public JeuPendu(IGenerateur g)
        {
            generateur = g;
            MotATrouve = g.Generer();
            GenererMasque();
            nbEssai = 10;
        }
        public JeuPendu(IGenerateur g, int n) : this(g)
        {
            nbEssai = n;
        }
        #endregion

        #region Propriétés
        public string MotATrouve { get => motATrouve; set => motATrouve = value; }
        public int NbEssai { get => nbEssai; set => nbEssai = value; }
        public string Masque { get => masque; set => masque = value; }
        #endregion

        public bool TestChar(char c)
        {
            bool found = false;
            string masqueTmp = "";
            for (int i = 0; i < MotATrouve.Length; i++)
            {
                if (MotATrouve[i] == c)
                {
                    found = true;
                    masqueTmp += c;
                }
                else
                {
                    masqueTmp += masque[i];
                }
            }
            masque = masqueTmp;
            if (found == false)
            {
                nbEssai--;
            }
            return found;
        }

        public bool TestWin()
        {
            if (motATrouve == masque && NbEssai > 0)
            {
                return true;
            }
            return false;
        }

        public void GenererMasque()
        {
            masque = "";
            for (int i = 0; i < MotATrouve.Length; i++)
            {
                masque += "*";
            }
        }

    }
}
