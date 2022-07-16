use medclin;

Create Table CoberturaMedica
(
Id int AUTO_INCREMENT primary key,
Descripcion varchar(50) unique not null,
Comentarios varchar(100) default '',
Activo bit default 1,
FechaCreacion datetime default now()
);

insert into CoberturaMedica (Descripcion, Comentarios) values ('Particular', '');
insert into CoberturaMedica (Descripcion, Comentarios) values ('Osde 210', 'Familiar');
insert into CoberturaMedica (Descripcion, Comentarios) values ('Osde 310', 'Familiar');
insert into CoberturaMedica (Descripcion, Comentarios) values ('Galeno Azul', 'Azul');
insert into CoberturaMedica (Descripcion, Comentarios) values ('Galeno Plata', 'Plata');
insert into CoberturaMedica (Descripcion, Comentarios) values ('PAMI', 'PAMI Cobertura Nacional');

Create Table Pacientes
(
Id int AUTO_INCREMENT primary key,
Dni varchar(20) unique not null,
Apellido varchar(50) not null,
Nombre varchar(50) not null,
FechaNacimiento DateTime not null,
IdCobertura int not null,
FOREIGN KEY (IdCobertura) REFERENCES CoberturaMedica(Id),
NumeroAfiliado varchar(50) not null default '',
Domicilio varchar(100) default '',
Email varchar(100) default '',
Telefono varchar(100) default '',
Comentarios varchar(500) default '',
Activo bit default 1,
FechaCreacion datetime default now()
);


Create Table Pacientes
(
Id int AUTO_INCREMENT primary key,
Dni varchar(20) unique not null,
Apellido varchar(50) not null,
Nombre varchar(50) not null,
Activo bit default 1
);

DELIMITER //

CREATE PROCEDURE `GetCoberturas`()
BEGIN
Select id, descripcion, comentarios, activo from CoberturaMedica;
END

DELIMITER //
CREATE PROCEDURE `CreateCobertura`(in inDescripcionCobertura varchar(50),in inComentarios varchar(100))
BEGIN
insert into CoberturaMedica (descripcion, comentarios) values (inDescripcionCobertura,inComentarios);
END

DELIMITER //

CREATE PROCEDURE `UpdateCobertura`(in inIdCobertura int, in inDescripcionCobertura varchar(50),in inComentarios varchar(100))
BEGIN

update CoberturaMedica
set descripcion = inDescripcionCobertura,
comentarios = inComentarios
where id = inIdCobertura;

END



