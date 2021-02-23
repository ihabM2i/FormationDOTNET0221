using CoursMCPDNETF.Classes;
using CoursMCPDNETF.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestCours.Classes;

namespace TestCours
{
    [TestClass]
    public class JeuPenduTest
    {
        [TestMethod]
        public void TestChar_Char_Ok()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            JeuPendu jeu = new JeuPendu(generateur);
            //Act
            bool result = jeu.TestChar('B');
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestChar_Char_Not_Ok()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            JeuPendu jeu = new JeuPendu(generateur);
            //Act
            bool result = jeu.TestChar('e');
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestChar_Ok_Essai()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            int nbEssai = 10;
            JeuPendu jeu = new JeuPendu(generateur,nbEssai);
            //Act
            jeu.TestChar('E');
            jeu.TestChar('B');
            //Assert
            Assert.AreEqual(nbEssai-1,jeu.NbEssai);
        }

        [TestMethod]
        public void TestChar_NotOk_Essai()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            int nbEssai = 10;
            JeuPendu jeu = new JeuPendu(generateur, nbEssai);
            //Act
            jeu.TestChar('E');
            //Assert
            Assert.AreEqual(nbEssai-1, jeu.NbEssai);
        }

        [TestMethod]
        public void TestChar_Ok_Masque()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            JeuPendu jeu = new JeuPendu(generateur);
            //Act
            jeu.TestChar('B');
            jeu.TestChar('o');
            //Assert
            Assert.AreEqual("Bo**o**", jeu.Masque);
        }

        [TestMethod]
        public void TestChar_NotOk_Masque()
        {
            //Arrange
            IGenerateur generateur = new FakeGenerateurMot();
            JeuPendu jeu = new JeuPendu(generateur);
            //Act
            jeu.TestChar('B');
            jeu.TestChar('e');
            //Assert
            Assert.AreEqual("B******", jeu.Masque);
        }

        [TestMethod]
        public void GenererMasque_Ok()
        {
            //Arrange
            //IGenerateur generateur = new FakeGenerateurMot();
            //Exemple d'utilisation du package Moq
            //La méthode of va générer un objet qui respecte l'interface
            IGenerateur generateur = Mock.Of<IGenerateur>();
            //Ajouter le fonctionnement de la méthode Generer
            Mock.Get(generateur).Setup(g => g.Generer()).Returns("Bonjour");
            JeuPendu jeu = new JeuPendu(generateur);
            //Act
           
            //Assert
            Assert.AreEqual("*******", jeu.Masque);
        }
    }
}
