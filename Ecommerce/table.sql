CREATE TABLE image 
(
	id int PRIMARY KEY IDENTITY(1,1),
	produit_id int not null,
	url varchar(255) not null
);
CREATE TABLE categorie
(
	id int PRIMARY KEY IDENTITY(1,1),
	titre varchar(255) not null,
);
CREATE TABLE produit
(
	id int PRIMARY KEY IDENTITY(1,1),
	titre varchar(255) not null,
	prix decimal not null,
	description text not null,
	categorie_id int not null
);

CREATE TABLE panier
(
	id int PRIMARY KEY IDENTITY(1,1),
	total decimal not null,
	utilisateur_id int,
	date_achat datetime not null
);
CREATE TABLE produit_panier
(
	id int PRIMARY KEY IDENTITY(1,1),
	panier_id int not null,
	produit_id int not null,
	qty int not null,
);

CREATE TABLE utilisateur
(
	id int PRIMARY KEY IDENTITY(1,1),
	email varchar(255) not null,
	password varchar(255) not null,
);