using CoursMCPDNETF.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCours
{
    //Annotation pour indiquer que la classe est une classe test
    [TestClass]
    public class CalculatriceTest
    {
        //Annotation pour indiquer que la méthode est une métode de test unitaire
        [TestMethod]
        public void TestAddition()
        {
            //Arrange
            //préparer l'objet qu'on souhaite tester
            //Avant il faut ajouter comme référence le projet ou se trouve les classes à tester dans les dependances du projet test
            //Pour créer un objet à partir d'un projet externe, il faut que la classe soit public
            Calculatrice calculatrice = new Calculatrice();
            //act
            //Démarrer la méthode qu'on shoutaie tester
            double result = calculatrice.Addition(10, 30);
            //Assert
            //Vérifier le resultat
            Assert.AreEqual(40, result);
            
        }
    }
}
