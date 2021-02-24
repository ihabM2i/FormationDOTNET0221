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