CREATE DATABASE SISTEMA_CLUB_EXPANDIDO;
--DROP DATABASE SISTEMA_CLUB_EXPANDIDO;
--USE master;

USE SISTEMA_CLUB_EXPANDIDO;

CREATE TABLE PROVINCIA(
	ID_PROVINCIA INT IDENTITY(1, 1) PRIMARY KEY,
	DESC_PROVINCIA NVARCHAR(100) NOT NULL
);
CREATE TABLE LOCALIDAD(
	ID_LOCALIDAD INT IDENTITY(1, 1) PRIMARY KEY,
	DESC_LOCALIDAD NVARCHAR(200) NOT NULL,
	COD_POSTAL NVARCHAR(20) NULL,
	ID_PROVINCIA INT FOREIGN KEY REFERENCES PROVINCIA(ID_PROVINCIA)
);

CREATE TABLE DEPORTE(
	ID_DEPORTE INT IDENTITY(1, 1) PRIMARY KEY,
	DESC_DEPORTE NVARCHAR(200) NOT NULL
);

CREATE TABLE REGLAMENTO(
	ID_REGLAMENTO INT IDENTITY(100, 1) PRIMARY KEY,
	NOMBRE_REGLAMENTO NVARCHAR(100) NOT NULL,
	DESC_REGLAMENTO NVARCHAR(500) NOT NULL,
	ESTADO NVARCHAR(50) NOT NULL,
	FECHA_CONFECCION DATETIME NOT NULL,
	FECHA_VIGENCIA DATETIME NOT NULL
);

CREATE TABLE CATEGORIA(
	ID_CATEGORIA INT IDENTITY(1, 1) PRIMARY KEY,
	DESC_CATEGORIA NVARCHAR(200) NOT NULL
);

CREATE TABLE PRODUCTO(
	ID_PRODUCTO INT IDENTITY(1000, 1) PRIMARY KEY,
	DESC_PRODUCTO NVARCHAR(200) NOT NULL,
	ID_CATEGORIA INT FOREIGN KEY REFERENCES CATEGORIA(ID_CATEGORIA)
);

--CREATE TABLE CATEGORIA_PROD(
--	ID_CATEGORIA INT FOREIGN KEY REFERENCES CATEGORIA(ID_CATEGORIA),
--	ID_PRODUCTO INT FOREIGN KEY REFERENCES PRODUCTO(ID_PRODUCTO)
--);

CREATE TABLE CLUB(
	ID_CLUB INT IDENTITY(1, 1) PRIMARY KEY,
	DESC_CLUB NVARCHAR(200) NOT NULL,
	ID_REGLAMENTO INT,
	ID_LOCALIDAD INT FOREIGN KEY REFERENCES LOCALIDAD(ID_LOCALIDAD)
);

ALTER TABLE CLUB
ADD CONSTRAINT FK_REGLAMENTO
FOREIGN KEY (ID_REGLAMENTO)
REFERENCES REGLAMENTO(ID_REGLAMENTO) 
ON DELETE SET NULL

CREATE TABLE CLUB_DEPORTE(
	ID_CLUB INT,
	ID_DEPORTE INT,
	CONSTRAINT FK_CLUB_DEP PRIMARY KEY (ID_CLUB, ID_DEPORTE),
	CONSTRAINT FK_CLUB 
		FOREIGN KEY (ID_CLUB) REFERENCES CLUB(ID_CLUB),
	CONSTRAINT FK_DEPORTE
		FOREIGN KEY (ID_DEPORTE) REFERENCES DEPORTE(ID_DEPORTE)
);

CREATE TABLE CLUB_PRODUCTO(
	ID_CLUB INT,
	ID_PRODUCTO INT,
	CONSTRAINT FK_CLUB_PROD PRIMARY KEY (ID_CLUB, ID_PRODUCTO),
	CONSTRAINT FK_CLUBP 
		FOREIGN KEY (ID_CLUB) REFERENCES CLUB(ID_CLUB),
	CONSTRAINT FK_PRODUCTOP
		FOREIGN KEY (ID_PRODUCTO) REFERENCES PRODUCTO(ID_PRODUCTO)
);

CREATE TABLE VENDEDOR(
	ID_VENDEDOR INT IDENTITY(1, 1) PRIMARY KEY,
	NOMBRE NVARCHAR(200) NOT NULL,
	APELLIDO NVARCHAR(200) NOT NULL
);

CREATE TABLE PRODUCTO_LOCALIDAD(
	ID_PRODUCTO INT,
	ID_LOCALIDAD INT,
	PRECIO DECIMAL(18, 2) NOT NULL,
	CONSTRAINT FK_PROD_LOC PRIMARY KEY (ID_LOCALIDAD, ID_PRODUCTO),
	CONSTRAINT FK_LOCA 
		FOREIGN KEY (ID_LOCALIDAD) REFERENCES LOCALIDAD(ID_LOCALIDAD),
	CONSTRAINT FK_PRODUCTOL
		FOREIGN KEY (ID_PRODUCTO) REFERENCES PRODUCTO(ID_PRODUCTO)
);

CREATE TABLE PROD_LOCALIDAD_CLUB(
	ID_PRODUCTO INT,
	ID_LOCALIDAD INT,
	ID_CLUB INT,
	ID_VENDEDOR INT,
	COMISION INT NOT NULL,
	CONSTRAINT FK_PROD_LOCAC PRIMARY KEY (ID_LOCALIDAD, ID_PRODUCTO, ID_CLUB, ID_VENDEDOR),
	CONSTRAINT FK_LOCAL 
		FOREIGN KEY (ID_LOCALIDAD) REFERENCES LOCALIDAD(ID_LOCALIDAD),
	CONSTRAINT FK_PRODUCTOLC
		FOREIGN KEY (ID_PRODUCTO) REFERENCES PRODUCTO(ID_PRODUCTO),
	CONSTRAINT FK_CLUBLP 
		FOREIGN KEY (ID_CLUB) REFERENCES CLUB(ID_CLUB),
	CONSTRAINT FK_VENDLC
		FOREIGN KEY (ID_VENDEDOR) REFERENCES VENDEDOR(ID_VENDEDOR),
);

INSERT INTO PROVINCIA VALUES('SANTA FE'),('MENDOZA'),('CORDOBA')
INSERT INTO LOCALIDAD VALUES('ROSARIO', 2000, 1), ('VILLA G.G', 2100, 1), ('VILLA MARIA', 3200, 3)
INSERT INTO DEPORTE VALUES('NATACION'),('FUTBOL'),('VOLEY'),('HOCKEY'),('RUGBY')
INSERT INTO REGLAMENTO VALUES('REGLAMENTO 1', 'LOREM IMSUP ... LALALA', 'Vigente', '2016-08-02 12:00:00', '2016-08-02 10:00:00')
INSERT INTO CATEGORIA VALUES('ACUATICO'), ('TIERRA'), ('CESPED'), ('AIRE')
INSERT INTO PRODUCTO VALUES('PELOTA DE TENIS CESPED', 3), ('KAYAC', 1), ('PARAPENTE', 4), ('PARACAIDAS', 4)  
INSERT INTO CLUB VALUES('CLUB ATLETICO NEWELLS OLD BOYS', 100, 1), ('CLUB ATLETICO ROSARIO CENTRAL', null, 1), 
('CLUB ATLETICO CENTRAL CORDOBA', null, 3)
INSERT INTO CLUB_DEPORTE VALUES(1, 2),(1, 5),(1, 3),(2, 2),(2, 1),(2, 5),(3, 1),(3, 2),(3, 4),(3, 5)
INSERT INTO CLUB_PRODUCTO VALUES(1, 1002), (1, 1001), (2, 1000), (3, 1000), (3, 1001), (3, 1002), (3, 1003)
INSERT INTO PRODUCTO_LOCALIDAD VALUES(1001, 1, 50.00), (1002, 1, 150.00), (1003, 1, 2500.00), (1001, 2, 250.00), (1002, 2, 450.00)
INSERT INTO VENDEDOR VALUES('PEDRO', 'ALONSO'), ('NACHO', 'SCOCCO'), ('MAXI', 'RODRIGUEZ')
INSERT INTO PROD_LOCALIDAD_CLUB VALUES(1001, 1, 1, 3, 500)