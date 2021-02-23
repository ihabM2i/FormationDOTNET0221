using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    class IHM
    {
        private CashRegister cashRegister;

        public IHM()
        {
            cashRegister = new CashRegister();
        }
        public void Start()
        {
            string choice;
            do
            {
                MainMenu();
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateProductAction();
                        break;
                    case "2":
                        MakeOrderAction();
                        break;

                }
            } while (choice != "0");
        }
        private void MainMenu()
        {
            Console.WriteLine("1----Ajouter un produit à la caisse");
            Console.WriteLine("2----Réaliser une vente");
        }

        private void OrderMenu()
        {
            Console.WriteLine("1---Ajouter un produit à la vente");
            Console.WriteLine("2---Payer par carte");
            Console.WriteLine("3---Payer en espèce");
        }

        private void CreateProductAction()
        {
            Console.Clear();
            Console.Write("Merci de saisir le titre du produit : ");
            string title = Console.ReadLine();
            Console.Write("Merci de saisir le prix du produit : ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Merci de saisir le stock du produit : ");
            int stock = Convert.ToInt32(Console.ReadLine());
            Product p = cashRegister.CreateProduct(title, price, stock);
            if(p == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur de création de produit");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine(p);
            }
        }

        private void MakeOrderAction()
        {
            Console.Clear();
            Order order = new Order();
            string choice;
            do
            {
                OrderMenu();
                choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        AddProductToOrderAction(order);
                        break;
                    case "2":
                        CardPaymentAction(order);
                        break;
                    case "3":
                        CashPaymentAction(order);
                        break;
                }
            } while (choice == "1");

            //Ajoute la vente dans la caisse
            cashRegister.AddOrder(order);
            
            Console.WriteLine(order);
            Console.WriteLine("une touche pour continuer....");
            Console.ReadLine();
            Console.Clear();
            
        }

        private void AddProductToOrderAction(Order order)
        {
            //Demander à l'utilisateur le numéro du produit si le produit existe il faut l'ajouter à la vente sinon on affiche un message d'erreur
            Console.Write("Merci de saisir l'id du produit : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Product product = cashRegister.SearchProductById(id);
            if(product == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Aucun produit avec cet id");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                order.AddProduct(product);
                Console.WriteLine("Produit ajouté");
            }
        }

        private void CashPaymentAction(Order order)
        {
            CashPayment payment = new CashPayment();
            Console.WriteLine("Merci saisir le montant donné : ");
            decimal givenAmount = Convert.ToDecimal(Console.ReadLine());
            payment.GivenAmount = givenAmount;
            if (order.Confirm(payment))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Paiement Ok");
                Console.WriteLine("La monnaie est de {0} euros", payment.Change);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur paiement ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private void CardPaymentAction(Order order)
        {
            CardPayment payment = new CardPayment();
            //order.Payment = payment;
            //if(payment.Pay(order.Total))
            //{
            //    order.Payment = payment;
            //}
            if (order.Confirm(payment))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Paiement Ok");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur paiement ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
