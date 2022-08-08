select * into CommentBackup from Comment 
select * into PostBackup from Post 
select * into UsersBackup from Users
select * into UserFriendBackup from user_Friend

drop table user_Friend
drop table Post
drop table Users
drop table parent_child_Comment
drop table Comment

create table Users
(
	userId int identity (12345678,1),
	firstName varchar(40),
	lastName varchar(40),
	mobilePhone varchar(10),
	email varchar(50) not null,
	userName varchar(20) not null,
	password varchar(15) not null,
	registeredAt DATETIME,
	lastLogin DATETIME,
	intro varchar (100),
	profile varchar(500),
	profilePicture varchar (1000),
	constraint pk_userId primary key (userId),
	constraint uk_mobilePhone unique (mobilePhone),
	constraint uk_email unique (email),
	constraint uk_userName unique (userName)
)

ALTER TABLE Users
DROP CONSTRAINT uk_mobilePhone;


create table Post
(
	id int identity (1000,1), 
	userId int,
	message varchar(5000),
	createdAt DATETIME,
	updatedAt DATETIME,
	Image varchar (1000),
	constraint pk_id primary key (id),
	constraint fk_userId_post foreign key (userId) references Users
)

create table Comment
(
	commentId int identity (100,1),
	userId int,
	comment varchar (5000),
	createdAt DATETIME,
	updatedAt DATETIME,
	postId int,
	comment_Image varchar (1000),
	constraint pk_commentId primary key (commentId),
	constraint fk_userId_comment foreign key (userId) references Users,
	constraint fk_postId_comment foreign key (postId) references Post
)

create table parent_child_Comment
(
	parent_CommentId int,
	child_CommentId int,
	constraint fk_parent_CommentId foreign key (parent_CommentId) references Comment,
	constraint fk_child_CommentId foreign key (child_CommentId) references Comment
)

create table user_Friend 
(
	id int identity (5000,1),
	userId int,
	friendId int,
	friendStatus varchar (30),
	createdAt DATETIME,
	updatedAt DATETIME,
	constraint pk_id_friend primary key (id),
	constraint fk_userId_friend foreign key (userId) references Users,
	constraint fk_friendId_friend foreign key (friendId) references Users,
	constraint chk_friendStatus check (friendStatus in ('Blocked', 'Accepted', 'Pending'))
)


insert into Users values ('Kennedy', 'Edwards', '1011011011', 'KingMarley@gmail.com', 'JamaicanMeCrazy', 'grasspass123', '2022-08-01 03:45:03.000', '2022-08-01 03:45:03.000', 'This is where the intro is', 'This is where the profile is', 'https://upload.wikimedia.org/wikipedia/commons/0/0a/Flag_of_Jamaica.svg')
insert into Users values ('Michelle', 'Tone', '8675309', 'callMeShelli@gmail.com', 'SnooSnoo', 'CallMeMaybe2000', '2022-08-01 03:45:03.000', '2022-08-01 03:45:45.000', 'Bad Girls Club', 'SnookiBook', 'https://www.oxygen.com/sites/oxygen/files/styles/media-gallery-computer/public/legacy/45gina.jpg?itok=-9xvalzm')
insert into Users values ('Joshua', 'Smith', '4567823456', 'BackHomeBaller@aol.com', 'StillOutside', 'MCHammer9', '2022-08-01 03:45:03.000', '2022-08-01 03:45:03.000', 'This is where the intro is', 'This is where the profile is', 'https://www.thefamouspeople.com/profiles/images/josh-smith-3.jpg')
insert into Users values ('Diana', 'Morris', '5555907', 'DirtyDianaNaNuNo@gmail.com', 'DirtyDiana', 'MJ#1985', '2022-08-01 03:45:03.000', '2022-08-02 02:45:45.000', 'May your hard times only last as long as a Kardashian wedding', 'Social Beezz Nutzz', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRBwgu1A5zgPSvfE83nurkuzNEoXs9DMNr8Ww&usqp=CAU')

select * from Users
select * from user_Friend

insert into Post values (12345680, 'Send Thoughts and Prayers I got trapped in a spray tan machine yesterday', '2022-08-02 13:50:00.000', '2022-08-02 13:58:00.000', 'http://jenniferhansen.com.au/wp-content/uploads/2012/12/Bad-spray-tan.jpg') 
insert into Post values (12345679, 'Feelin like Beyonce with a Horse made out of Cheese puffs', '2022-08-02 20:30:00.000', '2022-08-02 20:32:00.000', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSODYtKOYvO2DbiZx8ifqNwoiiV_w2Xfj6h6A&usqp=CAU') 
insert into Post values (12345678, 'Test Post of Post from UserID 12345678', '2022-08-02 21:27:58.927', '2022-08-02 21:27:58.927', ' ') 
insert into Post values (12345681, 'Party Time! This will work to pull in the existing data and update the message and the updatedAt time to 6:33 (or at least visibly different from the created at time)!', '2022-08-04 00:30:48.217', '2022-08-03 18:33:37.687', 'https://www.pixelstalk.net/wp-content/uploads/2016/08/Funny-Random-Wallpaper-1.jpg') 

select * from Post

insert into Comment values (12345678, 'Did you make it out?','2022-08-03 14:57:23.247','2022-08-03 14:57:23.247',1000,'https://i.etsystatic.com/iap/c38f7c/3146503340/iap_300x300.3146503340_n0im5e9u.jpg?version=0')
insert into Comment values (12345679, 'comment test Diego','2022-08-04 15:06:40.743','2022-08-04 15:06:40.743',1000,'https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Flastfm.freetls.fastly.net%2Fi%2Fu%2Far0%2F6d51eb23455f49c7bf5557cd9bd2f960.jpg&f=1&nofb=1')
insert into Comment values (12345678, 'Yes I survived lol','2022-08-06 00:12:01.800','2022-08-06 00:12:01.800',1000,'string')
insert into Comment values (12345680, 'The comment and the date should be different (comment was "Testing new comment on post 1009")','2022-08-06 00:44:29.263','2022-08-05 18:47:44.460',1009,'https://avante.biz/wp-content/uploads/Random-Image-Wallpapers/Random-Image-Wallpapers-025.jpg')
select * from Comment

insert into user_Friend values (12345678, 12345681, 'Blocked', '2022-08-02 21:15:00.000', '2022-08-03 18:55:20.990')
insert into user_Friend values (12345679, 12345678, 'Pending', '2022-08-03 19:21:04.120', '2022-08-03 19:21:04.120')
insert into user_Friend values (12345680, 12345681, 'Pending', '2022-08-03 19:25:56.120', '2022-08-03 19:25:56.120')
insert into user_Friend values (12345678, 12345679, 'Pending', '2022-08-04 12:28:25.420', '2022-08-04 12:28:25.420')
insert into user_Friend values (12345679, 12345680, 'Pending', '2022-08-04 12:41:21.857', '2022-08-04 12:41:21.857')
insert into user_Friend values (12345680, 12345679, 'Pending', '2022-08-04 12:50:24.933', '2022-08-04 12:50:24.933')
select * from user_Friend
