CREATE TABLE contact (id int PRIMARY KEY IDENTITY(1,1), 
nom varchar(100) not null, 
prenom varchar(100) not null, 
telephone varchar(10) not null);

CREATE TABLE adresse (id int PRIMARY KEY IDENTITY(1,1),
rue varchar(255) not null,
ville varchar(100) not null,
codePostal varchar(5) not null,
personneId int not null)

SELECT p.id as personneId, p.nom, p.prenom, a.id as adresseId, a.rue, a.ville, a.codePostal from personne p left join adresse a on p.id = a.personneId


CREATE TABLE Mail (
	id int PRIMARY KEY IDENTITY(1,1),
	mail varchar(255) not null,
	contactId int not null
)

--Création des tables de l'application caisse
CREATE TABLE product (
	id int PRIMARY KEY IDENTITY(1,1),
	title varchar(255) not null,
	price  decimal not null,
	stock int not null
)

--Table commande
CREATE TABLE sale(
	id int PRIMARY KEY IDENTITY(1,1),
	total decimal not null,
	date_sale datetime not null,
	sale_status varchar(100) not null
)

--Table Produit acheté
CREATE Table sale_product(
	id int PRIMARY KEY IDENTITY(1,1),
	sale_id int not null,
	product_id int not null
	-- pour ajouter une Foreign key (clé étrangère) vers la table sale
	--CONSTRAINT fk_sale FOREIGN KEY (sale_id) REFERENCES Sale(id)
)

--Table paiement

CREATE TABLE payment (
	id int PRIMARY KEY IDENTITY(1,1),
	amount decimal not null,
	order_id int not null,
	payment_type varchar(255) not null,
	payment_date datetime not null,
)

CREATE TABLE client (id int PRIMARY KEY IDENTITY (1,1),
nom varchar(255) not null,
prenom varchar(255) not null,
telephone varchar(255) not null,
hotel_id int not null,
);




CREATE TABLE hotel (id int PRIMARY KEY IDENTITY (1,1),
nom varchar(255) not null);





CREATE TABLE chambre (id int PRIMARY KEY IDENTITY (1,1),
tarif decimal not null,
nbOccp int not null,
statut varchar(255) not null,
hotel_id int not null);




CREATE TABLE reservation (id int PRIMARY KEY IDENTITY (1,1),
statut varchar(255) not null,
client_id int not null,
hotel_id int not null);




CREATE TABLE reservation_chambre (id int PRIMARY KEY IDENTITY (1,1),
reservation_id int not null,
chambre_id int not null);