CREATE TABLE User (
	user_id int auto_increment primary key,
	username varchar(50),
	password varchar(50),
	type varchar(50)
);

CREATE TABLE Ticket (
	ticket_id int auto_increment primary key,
	type varchar(50),
	user_id int references User(user_id),
	date_created DATETIME,
	content varchar(50),
	product varchar(50),
	status varchar(50)
);

CREATE TABLE Comment (
	comment_id int auto_increment primary key,
	ticket_id int references Ticket(ticket_id),
	user_id int references User(user_id),
	date_created DATETIME,
	content varchar(50)
);

CREATE TABLE Bill (
    bill_id int auto_increment primary key,
    date_created DATETIME,
    user_id int references User(user_id),
    content varchar(50),
    amount int,
    status varchar(50)
);
