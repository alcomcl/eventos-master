USE [master]
GO
/****** Object:  Database [OnBreak]    ******/
CREATE DATABASE [OnBreakFinal] 
GO

USE [OnBreakFinal]
GO

--drop database OnBreak2;

CREATE TABLE [dbo].[ActividadEmpresa](
	[IdActividadEmpresa] [int] NOT NULL,
	[Descripcion] [nvarchar](30) NOT NULL,
 CONSTRAINT [ActividadEmpresa_PK] PRIMARY KEY CLUSTERED 
(
	[IdActividadEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Cliente](
	[RutCliente] [nvarchar](10) NOT NULL,
	[RazonSocial] [nvarchar](50) NOT NULL,
	[NombreContacto] [nvarchar](50) NOT NULL,
	[MailContacto] [nvarchar](30) NOT NULL,
	[Direccion] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](15) NOT NULL,
	[IdActividadEmpresa] [nvarchar](40) NOT NULL,
	[IdTipoEmpresa] [nvarchar](40) NOT NULL,
 CONSTRAINT [Cliente_PK] PRIMARY KEY CLUSTERED 
(
	[RutCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




CREATE TABLE [dbo].[Contrato](
	[Numero] [nvarchar](12) NOT NULL,
	[Creacion] [datetime] NOT NULL,
	[Termino] [nvarchar](40) NULL,
	[RutCliente] [nvarchar](10) NOT NULL,
	[TipoEvento] [nvarchar](40) NOT NULL,
	[ModalidadServicio] [nvarchar](40) NOT NULL,
	[Asistentes] [float] NOT NULL,
	[ValorBase]  [float] NOT NULL,
	[PersonalAdicional] [float] NOT NULL,
	[EstadoContrato] [nvarchar](12) NOT NULL,
	[ValorTotalContrato] [float] NOT NULL,
	[Observaciones] [nvarchar](250) NOT NULL,
 CONSTRAINT [Key1] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




CREATE TABLE [dbo].[TipoEmpresa](
	[IdTipoEmpresa] [int] NOT NULL,
	[Descripcion] [nvarchar](40) NOT NULL,
 CONSTRAINT [TipoEmpresa_PK] PRIMARY KEY CLUSTERED 
(
	[IdTipoEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




CREATE TABLE [dbo].[TipoEvento](
	[IdTipoEvento] [int] NOT NULL,
	[Descripcion] [nvarchar](40) NOT NULL,
 CONSTRAINT [Key3] PRIMARY KEY CLUSTERED 
(
	[IdTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




CREATE TABLE [dbo].[ModalidadServicio](
	[IdModalidad] [nvarchar](5) NOT NULL,
	[IdTipoEvento] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
	[ValorBase] [float] NOT NULL,
	[PersonalBase] [int] NOT NULL,
 CONSTRAINT [Key2] PRIMARY KEY CLUSTERED 
(
	[IdModalidad] ASC,
	[IdTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]





INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (1, N'Agropecuaria')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (2, N'Minería')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (3, N'Manufactura')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (4, N'Comercio')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (5, N'Hotelería')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (6, N'Alimentos')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (7, N'Transporte')
INSERT [dbo].[ActividadEmpresa] ([IdActividadEmpresa], [Descripcion]) VALUES (8, N'Servicios')

INSERT [dbo].[TipoEmpresa] ([IdTipoEmpresa], [Descripcion]) VALUES (10, N'SPA')
INSERT [dbo].[TipoEmpresa] ([IdTipoEmpresa], [Descripcion]) VALUES (20, N'EIRL')
INSERT [dbo].[TipoEmpresa] ([IdTipoEmpresa], [Descripcion]) VALUES (30, N'Limitada')
INSERT [dbo].[TipoEmpresa] ([IdTipoEmpresa], [Descripcion]) VALUES (40, N'Sociedad Anónima')

insert into TipoEvento values(40,'Matrimonio');
insert into TipoEvento values(50,'Cumpleaños Adulto');
insert into TipoEvento values(60,'Cumpleaños Infantil');
insert into TipoEvento values(65,'Evento Empresarial');
insert into TipoEvento values(70,'Desdepida de Soltero');
insert into TipoEvento values(80,'Evento Religioso');
insert into TipoEvento values(90,'Baby Shower'); 

INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CB001', 10, N'Light Break', 3, 2)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CB002', 10, N'Journal Break', 8, 6)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CB003', 10, N'Day Break', 12, 6)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CE001', 30, N'Ejecutiva', 25, 10)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CE002', 30, N'Celebración', 35, 14)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CO001', 20, N'Quick Cocktail', 6, 4)
INSERT [dbo].[ModalidadServicio] ([IdModalidad], [IdTipoEvento], [Nombre], [ValorBase], [PersonalBase]) VALUES (N'CO002', 20, N'Ambient Cocktail', 10, 5)

insert into Cliente values('172510418','Distribuidora de Alimentos', 'Juan Perez Aguliar', 'juan.perez@gmail.com', 'Antillanca Norte 1757', '966490273', 'Alimentos', 'Sociedad Anónima');
insert into Cliente values('172579884', 'Ventas al por mayor de Verduras', 'María Díaz Gonzalez', 'maria.diaz@gmail.com', 'Uspallata 1757', '934278817', 'Alimentos', 'EIRL');
insert into Cliente values('89650771', 'Servicios Aereos y venta de Alimentos', 'Daniel Gallardo Zurita', 'daniel.gallardo@gmail.com', 'Av. O´Higgins 4062', 864286817, 'Servicios', 'Sociedad Anónima');
insert into Cliente values('112057323', 'Transporte publico de Pasajeros', 'Ana Rojas Magallanes', 'ana.rojas@gmail.com', 'Av. Americo Vespucio Norte 307',37281163,'Transporte','Sociedad Anónima');
insert into Cliente values('43753940', 'Reparacion y mantencion de Notebook', 'Carlos Martinez Ortega','carlos.martinez@gmail.com','Antartica 4366',972981163,'Servicios','EIRL');
insert into Cliente values('107791078','Ventas al por mayor de Detergentes Industriales','Manuel Maldonado Urrutia','manuel.maldonado@gmail.com', 'Antartica 4366',972981163,'Servicios','EIRL');
insert into Cliente values('18533869k','Confeccion de ropa para Bebe','Ruben Mena Gonzales','ruben.mena@gmail.com','Atahualpa 1678',971081163,'Manufactura','SPA');
insert into Cliente values('123234952','Crianza y explotacion de Salmon','Marcos Carvajal Zuñiga', 'marcos.carvajal@gmail.com','El lingue 271',940267001,'Agropecuaria', 'Sociedad Anónima');
insert into Cliente values('86333554','Venta de abarrotes, rotizeria','Haroldo Menares Rivera','haroldo.menares@gmail.com','Lanceros del Rey 5980',917398263,'Alimentos','Sociedad Anónima');

 insert into Contrato values('201901251530','25/01/2019','27/01/2019 18:40','86333554','Evento Empresarial','Ejecutiva',18,687500,2,'INACTIVO',375000,'Puntualidad');
 insert into Contrato values('201810151230','15/10/2018','25/10/2018 15:30','123234952','Matrimonio','Celebración',31,962500,5,'INACTIVO',575000,'Comida Especial');
 insert into Contrato values('201808151830','15/08/2018','17/08/2018 09:15','18533869k','Cumpleaños Infantil','Quick Cocktail',25,165000,1,'INACTIVO',475000,'Llevar carrito de completos');
 insert into Contrato values('201803101130','10/03/2018','13/03/2018 16:00','107791078','Evento Religioso','Journal Break',29,220000,2,'INACTIVO',355000,'Puntualidad');



--BORRADO DE CONSTRAINT
ALTER TABLE [dbo].[Cliente]  DROP CONSTRAINT [Cliente_ActividadEmpresa_FK1];
ALTER TABLE [dbo].[Cliente]  DROP CONSTRAINT [Cliente_TipoEmpresa_FK1];
ALTER TABLE [dbo].[Contrato]  DROP CONSTRAINT [Contrato_Cliente_FK1];
ALTER TABLE [dbo].[Contrato]  DROP CONSTRAINT [Contrato_ModalidadEvento_FK1] ;
ALTER TABLE [dbo].[ModalidadServicio]  DROP CONSTRAINT [ModalidadServicio_TipoEvento_FK1];
--BORRADO DE TABLAS
drop table [dbo].[ActividadEmpresa];
drop table [dbo].[Cliente];
drop table [dbo].[Contrato];
drop table [dbo].[ModalidadServicio];
drop table [dbo].[TipoEvento];
drop table [dbo].[TipoEmpresa];

drop database [OnBreak];
use OnbreakFinal;
delete from Contrato;


select * from cliente;
select * from contrato;