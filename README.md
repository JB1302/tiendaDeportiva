Integrantes del equipo:

José Alejandro González Agüero - API/Back End
Jonathan Steven Barrantes Jiménez - Back End/Front End
Isaac Andrey Sánchez Delgado - Front end/Api
José Andrés Sequeira Conejo - Front end/Api

(Los roles se fueron intercambiando dependiendo de lo necesitado en el proyecto)

En caso de que no funcionen los controller de la capa Web:
Copiar puerto del URL de API y colocarlo en la variable urlbase en los conrollers de la capa web.

La aplicación genera mediante code first la base de datos y luego por medio de un defaultconnection se comunica esta. La base de datos se
llena con el siguiente script:

-- LIMPIEZA
DELETE FROM dbo.DetallePedidoes;
DELETE FROM dbo.Pedidoes;
DELETE FROM dbo.Productoes;

-- Reiniciar Seeding a 0
DBCC CHECKIDENT ('dbo.DetallePedidoes', RESEED, 0);
DBCC CHECKIDENT ('dbo.pedidoes', RESEED, 0);
DBCC CHECKIDENT ('dbo.Productoes', RESEED, 0);

-- Categoria:
-- 1 = Futbol
-- 2 = Basquetbol
-- 3 = Natacion
-- 4 = Tenis

-- EstadoPedido:
-- 0 = Pendiente
-- 1 = Completado


-- PRODUCTOS
INSERT INTO dbo.Productoes (Nombre, Descripcion, Precio, Stock, Categoria, Activo)
VALUES
(N'Balón de fútbol profesional', N'Balón tamaño 5 para entrenamiento y partido', 18990.00, 25, 1, 1),
(N'Balón de fútbol sala', N'Balón de menor rebote para futsal', 17450.00, 18, 1, 1),
(N'Tacos FG ligeros', N'Tacos para césped natural', 42990.00, 14, 1, 1),
(N'Espinilleras junior', N'Protección liviana para entrenamiento', 8990.00, 30, 1, 1),
(N'Medias deportivas fútbol', N'Par de medias altas acolchadas', 4990.00, 40, 1, 1),
(N'Conos de entrenamiento x10', N'Set de conos para agilidad', 10990.00, 22, 1, 1),
(N'Red para portería', N'Red resistente para marco estándar', 25990.00, 8, 1, 1),
(N'Guantes de portero básicos', N'Guantes con palma de agarre', 15990.00, 16, 1, 1),
(N'Guantes de portero pro', N'Guantes con refuerzo en dedos', 28990.00, 10, 1, 1),
(N'Short de fútbol', N'Short liviano de secado rápido', 7990.00, 35, 1, 1),
(N'Camiseta de fútbol local', N'Camiseta de manga corta para juego', 12990.00, 28, 1, 1),
(N'Inflador manual', N'Inflador portátil con aguja', 6990.00, 20, 1, 1),
(N'Bolso deportivo mediano', N'Bolso para uniforme y accesorios', 14990.00, 12, 1, 1),

(N'Balón de básquet indoor', N'Balón para cancha interna', 19990.00, 20, 2, 1),
(N'Balón de básquet outdoor', N'Balón resistente para cancha externa', 18990.00, 18, 2, 1),
(N'Aro de básquet portátil', N'Aro con estructura ajustable', 89990.00, 5, 2, 1),
(N'Malla para aro', N'Malla de repuesto', 4990.00, 25, 2, 1),
(N'Muñequera deportiva', N'Muñequera absorbente', 3990.00, 32, 2, 1),
(N'Rodillera de compresión', N'Rodillera elástica de soporte', 11990.00, 15, 2, 1),
(N'Camiseta sin mangas básquet', N'Camiseta transpirable', 10990.00, 22, 2, 1),
(N'Short de básquet', N'Short amplio de secado rápido', 9990.00, 24, 2, 1),
(N'Zapatos de básquet', N'Calzado con amortiguación y agarre', 55990.00, 9, 2, 1),
(N'Bomba para balón', N'Bomba compacta para inflado', 5990.00, 18, 2, 1),
(N'Botella deportiva 750ml', N'Botella reutilizable', 4490.00, 40, 2, 1),
(N'Set de conos x20', N'Conos para dribbling y coordinación', 14990.00, 14, 2, 1),

(N'Gorra de natación silicona', N'Gorra flexible y resistente al cloro', 5990.00, 26, 3, 1),
(N'Lentes de natación básicos', N'Lentes con ajuste simple', 7490.00, 24, 3, 1),
(N'Lentes de natación pro', N'Lentes antivaho', 12990.00, 16, 3, 1),
(N'Tabla de patada', N'Tabla flotante para entrenamiento', 9990.00, 12, 3, 1),
(N'Pull buoy', N'Flotador para trabajo de brazos', 8990.00, 10, 3, 1),
(N'Aletas cortas', N'Aletas para técnica y potencia', 21990.00, 11, 3, 1),
(N'Traje de baño competitivo', N'Traje de baño de secado rápido', 24990.00, 13, 3, 1),
(N'Traje de baño entrenamiento', N'Traje resistente al uso diario', 18990.00, 17, 3, 1),
(N'Toalla microfibra', N'Toalla compacta de secado rápido', 7990.00, 30, 3, 1),
(N'Bolso impermeable', N'Bolso para ropa húmeda', 16990.00, 9, 3, 1),
(N'Tapones para oídos', N'Tapones de silicona para piscina', 3490.00, 28, 3, 1),
(N'Naricera para natación', N'Clip nasal para entrenamiento', 2990.00, 25, 3, 1),

(N'Raqueta de tenis iniciación', N'Raqueta ligera para principiantes', 32990.00, 10, 4, 1),
(N'Raqueta de tenis intermedia', N'Raqueta balanceada', 45990.00, 8, 4, 1),
(N'Pelotas de tenis x3', N'Tubo con tres pelotas', 6990.00, 35, 4, 1),
(N'Pelotas de tenis x6', N'Pack de seis pelotas', 12990.00, 22, 4, 1),
(N'Grip para raqueta', N'Cinta de agarre absorbente', 3990.00, 40, 4, 1),
(N'Overgrip x3', N'Paquete de tres overgrips', 6490.00, 25, 4, 1),
(N'Muñequera tenis', N'Muñequera liviana', 3490.00, 30, 4, 1),
(N'Falda deportiva tenis', N'Falda con short interno', 15990.00, 14, 4, 1),
(N'Camisa polo tenis', N'Camisa deportiva tipo polo', 13990.00, 18, 4, 1),
(N'Zapatos de tenis', N'Calzado para cancha dura', 49990.00, 7, 4, 1),
(N'Raquetero doble', N'Bolso para dos raquetas', 27990.00, 6, 4, 1),
(N'Protector antivibración', N'Accesorio para reducir vibración', 2490.00, 50, 4, 1),
(N'Cuerda para raqueta', N'Juego de cuerdas de repuesto', 11990.00, 20, 4, 1);


-- pedidoes
INSERT INTO dbo.Pedidoes (Fecha, IdUsuario, MontoTotal, Estado)
VALUES
('2026-01-05 10:15:00', N'user01', 37980.00, 1),
('2026-01-06 11:00:00', N'user02', 17450.00, 0),
('2026-01-07 14:20:00', N'user03', 42990.00, 1),
('2026-01-08 09:40:00', N'user04', 26970.00, 1),
('2026-01-09 16:10:00', N'user05', 19960.00, 0),
('2026-01-10 13:30:00', N'user06', 21980.00, 1),
('2026-01-11 15:00:00', N'user07', 25990.00, 0),
('2026-01-12 08:50:00', N'user08', 15990.00, 1),
('2026-01-13 12:45:00', N'user09', 28990.00, 1),
('2026-01-14 17:25:00', N'user10', 15980.00, 0),

('2026-01-15 10:05:00', N'user01', 25980.00, 1),
('2026-01-16 11:35:00', N'user02', 6990.00, 0),
('2026-01-17 14:50:00', N'user03', 14990.00, 1),
('2026-01-18 09:15:00', N'user04', 19990.00, 1),
('2026-01-19 16:40:00', N'user05', 37980.00, 0),
('2026-01-20 13:10:00', N'user06', 89990.00, 1),
('2026-01-21 15:25:00', N'user07', 14970.00, 0),
('2026-01-22 08:30:00', N'user08', 7980.00, 1),
('2026-01-23 12:10:00', N'user09', 11990.00, 1),
('2026-01-24 17:45:00', N'user10', 21980.00, 0),

('2026-01-25 10:20:00', N'user01', 19980.00, 1),
('2026-01-26 11:50:00', N'user02', 55990.00, 0),
('2026-01-27 14:10:00', N'user03', 11980.00, 1),
('2026-01-28 09:25:00', N'user04', 13470.00, 1),
('2026-01-29 16:15:00', N'user05', 14990.00, 0),
('2026-01-30 13:40:00', N'user06', 11980.00, 1),
('2026-01-31 15:55:00', N'user07', 7490.00, 0),
('2026-02-01 08:45:00', N'user08', 12990.00, 1),
('2026-02-02 12:35:00', N'user09', 19980.00, 1),
('2026-02-03 17:05:00', N'user10', 8990.00, 0),

('2026-02-04 10:30:00', N'user01', 21990.00, 1),
('2026-02-05 11:10:00', N'user02', 24990.00, 0),
('2026-02-06 14:35:00', N'user03', 37980.00, 1),
('2026-02-07 09:55:00', N'user04', 15980.00, 1),
('2026-02-08 16:20:00', N'user05', 16990.00, 0),
('2026-02-09 13:00:00', N'user06', 13960.00, 1),
('2026-02-10 15:15:00', N'user07', 8970.00, 0),
('2026-02-11 08:20:00', N'user08', 32990.00, 1),
('2026-02-12 12:55:00', N'user09', 45990.00, 1),
('2026-02-13 17:35:00', N'user10', 13980.00, 0),

('2026-02-14 10:45:00', N'user01', 12990.00, 1),
('2026-02-15 11:25:00', N'user02', 11970.00, 0),
('2026-02-16 14:05:00', N'user03', 12980.00, 1),
('2026-02-17 09:35:00', N'user04', 13960.00, 1),
('2026-02-18 16:50:00', N'user05', 15990.00, 0),
('2026-02-19 13:20:00', N'user06', 13990.00, 1),
('2026-02-20 15:40:00', N'user07', 49990.00, 0),
('2026-02-21 08:10:00', N'user08', 27990.00, 1),
('2026-02-22 12:25:00', N'user09', 12450.00, 1),
('2026-02-23 17:15:00', N'user10', 11990.00, 0);


-- DETALLES DE PEDIDO
INSERT INTO dbo.DetallePedidoes (IdPedido, IdProducto, Cantidad, PrecioUnitario)
VALUES
(1, 1, 2, 18990.00),
(2, 2, 1, 17450.00),
(3, 3, 1, 42990.00),
(4, 4, 3, 8990.00),
(5, 5, 4, 4990.00),
(6, 6, 2, 10990.00),
(7, 7, 1, 25990.00),
(8, 8, 1, 15990.00),
(9, 9, 1, 28990.00),
(10, 10, 2, 7990.00),

(11, 11, 2, 12990.00),
(12, 12, 1, 6990.00),
(13, 13, 1, 14990.00),
(14, 14, 1, 19990.00),
(15, 15, 2, 18990.00),
(16, 16, 1, 89990.00),
(17, 17, 3, 4990.00),
(18, 18, 2, 3990.00),
(19, 19, 1, 11990.00),
(20, 20, 2, 10990.00),

(21, 21, 2, 9990.00),
(22, 22, 1, 55990.00),
(23, 23, 2, 5990.00),
(24, 24, 3, 4490.00),
(25, 25, 1, 14990.00),
(26, 26, 2, 5990.00),
(27, 27, 1, 7490.00),
(28, 28, 1, 12990.00),
(29, 29, 2, 9990.00),
(30, 30, 1, 8990.00),

(31, 31, 1, 21990.00),
(32, 32, 1, 24990.00),
(33, 33, 2, 18990.00),
(34, 34, 2, 7990.00),
(35, 35, 1, 16990.00),
(36, 36, 4, 3490.00),
(37, 37, 3, 2990.00),
(38, 38, 1, 32990.00),
(39, 39, 1, 45990.00),
(40, 40, 2, 6990.00),

(41, 41, 1, 12990.00),
(42, 42, 3, 3990.00),
(43, 43, 2, 6490.00),
(44, 44, 4, 3490.00),
(45, 45, 1, 15990.00),
(46, 46, 1, 13990.00),
(47, 47, 1, 49990.00),
(48, 48, 1, 27990.00),
(49, 49, 5, 2490.00),
(50, 50, 1, 11990.00);


