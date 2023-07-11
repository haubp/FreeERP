CREATE TABLE User (
	user_id int auto_increment primary key,
	username varchar(50),
	password varchar(50),
	type varchar(50)
);

CREATE TABLE SaleTicket (
	ticket_id int auto_increment primary key,
	user_id int references User(user_id),
	date_created DATETIME,
	content varchar(50),
	product varchar(50),
	status varchar(50)
);

CREATE TABLE CustomerSuccessTicket (  
    ticket_id int auto_increment primary key,
    user_id int references User(user_id),
    date_created DATETIME,
    content varchar(50),
	status varchar(50)
);

CREATE TABLE EngineerTicket (  
    ticket_id int auto_increment primary key,
    user_id int references User(user_id),
    date_created DATETIME,
    content varchar(50),
	status varchar(50)
);
