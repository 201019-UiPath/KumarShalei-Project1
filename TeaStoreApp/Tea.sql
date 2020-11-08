-- drop table orderitems;
-- drop table orders;
-- drop table customers;
-- drop table inventory;
-- drop table locations;
-- drop table products;



CREATE TABLE Products (
    productId serial primary key,
    productName varchar(20) not null,
	numberOfTeaBags int,
    price DECIMAL(10,2),
	description varchar(250) not null
);

CREATE TABLE Customers (
    customerId serial primary key,
	customerFirstName varchar(100) not null,
	customerLastName varchar(100) not null,
	customerEmail varchar(100)
);

create table Locations
(
	locationId serial primary key,
	city varchar(20),
	stateAcronym varchar(2)
);

CREATE TABLE Inventory (
	id serial primary key,
	locationId int references locations (locationId),
    productId int references products (productId),
    stock int
);

CREATE TABLE Orders (
	orderId serial primary key,
	customerId int references Customers (customerid),
	locationId int references Locations (locationid),
	totalPrice Decimal(10,2),
	payed bool
);

Create Table OrderItems (
	orderItemsId serial primary key,
	orderId int REFERENCES Orders (orderId),
	productId int references Products (productId),
	amount int,
	totalPrice Decimal(10,2)
 
);

Insert into Products (productName,numberOfTeaBags, price, description) values 
('Chamomile', 16, 4.99, 'Hibiscus Tea is made from the colorful flowers of the hibiscus plant. It has a pink-red color and refreshing, tart flavor. It can be enjoyed hot or iced.'),
('Chrysanthemum', 10, 11.99, 'Treats respiratory problems, high blood pressure, and hyperthyroidism.'),
('Peppermint', 15, 5.99, 'Peppermint Tea is mostly used to support digestive tract health, it also has antioxidant, anticancer, antibacterial and antiviral properties.'),
('Ginger', 16, 4.99, 'Ginger tea is a spicy and flavorful drink that packs a punch of healthy, disease-fighting antioxidants.'),
('Lemon Balm',15,4.99,'Lemon balm tea has a light, lemony flavor and seems to have health-promoting properties.'),
('Rose Hip',15,4.99, 'Rose hip tea is made from the fruit of the rose plant. It is high in vitamin C and beneficial plant compounds.'),
('Sage',16,4.99, 'Sage tea is well known for its medicinal properties, and scientific research has begun to support several of its health benefits, especially for brain health.'),

('Earl Grey', 20, 7.99, 'Earl Grey tea is a tea blend which has been flavoured with the addition of oil of bergamot.'),
('Lapsang Souchong', 20, 5.99, 'Lapsang Souchong is a black tea consisting of Camellia sinensis leaves that are smoke-dried over a pinewood fire.'),
('Masala Chai', 20, 8.99, 'Masala chai is a tea beverage made by boiling black tea in milk and water with a mixture of aromatic herbs and spices.'),
('Darjeeling', 20, 6.99, 'Darjeeling tea is a tea made from Camellia sinensis that is grown and processed in the Darjeeling or Kalimpong Districts in West Bengal, India.'),
('Assam', 20, 5.99, 'Assam tea is named after the region of its production, Assam, India, and is manufactured specifically from the plant Camellia sinensis var. assamica.'),
('Yunnan Black', 15, 7.99, 'Yunnan black tea, also known as ‘Dianhong’ is a fully oxidised tea grown high in the mountainous region of Lincang between 1680-1900 metres above sea level.'),
('Keemun Black', 10, 6.99, 'Keemun black teas are known for their aromatic fragrance, mellow flavor and beautiful aesthetic value as well as the brilliant red liquor the brewed tea leaves produce.'),

('Matcha', 15, 5.99, 'Matcha is a Japanese green tea powder made from finely powdered dried tea leaves.'),
('Genmaicha',15,7.99,'Genmaicha is a Japanese brown rice green tea consisting of green tea mixed with roasted popped brown rice.'),
('White Peony',16, 6.99, 'White Peony (known as Bai Mudan) is a type of white tea made from plucks each with one leaf shoot and two immediate young leaves of the camellia sinensis plant.'),
('Silver Needle', 5, 11.99, 'Silver Needle tea is perfect for improving digestion, especially for when you feel stomach cramps or nausea.');


insert into Customers (customerFirstName, customerLastName, customerEmail) values
('Shalei', 'Kumar', 'Manager123@gmail.com'),
('Vivian', 'Yu', 'vyu1234@gmail.com'),
('Laramie', 'Cole', 'lacol23@yahoo.com'),
('Sonia', 'Nemani', 'discodancer72@aol.com');


Insert into Locations (city, stateAcronym) values 
('Albany', 'NY'),
('Buffalo', 'NY'),
('Syracuse','NY');

insert into Inventory (locationId, productId, stock) values
(1,1,10),
(1,2,19),
(1,3,18),
(1,4,10),
(1,5,19),
(1,6,10),
(1,7,19),
(1,8,14),
(1,9,13),
(1,10,15),
(1,11,13),
(1,12,19),
(1,13,18),
(1,14,17),
(1,15,12),
(1,16,14),
(1,17,17),
(1,18,15),

(2,1,8),
(2,2,14),
(2,3,13),
(2,4,19),
(2,5,17),
(2,6,18),
(2,7,12),
(2,8,13),
(2,10,16),
(2,11,15),
(2,15,17),
(2,17,19),

(3,1,17),
(3,2,18),
(3,3,19),
(3,4,13),
(3,5,11),
(3,6,12),
(3,7,12),
(3,8,12),
(3,10,17),
(3,11,13),
(3,13,15),
(3,15,13),
(3,16,19),
(3,17,15),
(3,18,17);


insert into Orders (customerId, locationId, totalPrice,	payed) values 
(2,3,14.98,false);

insert into orderItems (orderId,productId,amount,totalPrice) values
(1,15,1,5.99),
(1,10,1,8.99);

alter table orders disable trigger all;

ALTER TABLE orders 
    action [, ... ]
select * from orders


