USE [TabloidMVC]
GO

SET IDENTITY_INSERT [UserType] ON
INSERT INTO [UserType] ([ID], [Name]) VALUES (1, 'Admin'), (2, 'Author');
SET IDENTITY_INSERT [UserType] OFF


SET IDENTITY_INSERT [Category] ON
INSERT INTO [Category] ([Id], [Name]) 
VALUES (1, 'Other'), (2, 'Close Magic'), (3, 'Politics'), (4, 'Science'), (5, 'Improv'), 
	   (6, 'Cthulhu Sightings'), (7, 'History'), (8, 'Home and Garden'), (9, 'Entertainment'), 
	   (10, 'Cooking'), (11, 'Music'), (12, 'Movies'), (13, 'Regrets'), (14, 'Technology');
SET IDENTITY_INSERT [Category] OFF


SET IDENTITY_INSERT [Tag] ON
INSERT INTO [Tag] ([Id], [Name])
VALUES (1, 'C#'), (2, 'JavaScript'), (3, 'Cyclopean Terrors'), (4, 'Family');
SET IDENTITY_INSERT [Tag] OFF

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
VALUES (1, 'Admina', 'Strator', 'admin', 'admin@example.com', SYSDATETIME(), NULL, 1);
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Post] ON
INSERT INTO [Post] (
	[Id], [Title], [Content], [ImageLocation], [CreateDateTime], [PublishDateTime], [IsApproved], [CategoryId], [UserProfileId])
VALUES (
	1, 'C# is the Best Language', 
'There are those' + char(10) + 'who do not believe' + char(10) + 'C# is the best.' + char(10) + 'They are wrong.',
    'https://gizmodiva.com/wp-content/uploads/2017/10/SCOTT-A-WOODWARD_1SW1943-1170x689.jpg', SYSDATETIME(), SYSDATETIME(), 1, 4, 1),
	(
	2, 'Fast Times as Ridgemont High', 
'This movie taught me everything I know',
    'https://decider.com/wp-content/uploads/2020/09/fast-times-ridgemont-high-dudes.jpg?quality=80&strip=all&w=1200', SYSDATETIME(), SYSDATETIME(), 1, 13, 1),
	(
	3, 'Where Did the Doggy Go', 
'There was a cute dog I saw once, and then I never saw it again.',
    'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/dog-puppy-on-garden-royalty-free-image-1586966191.jpg?crop=1.00xw:0.669xh;0,0.190xh&resize=1200:*', SYSDATETIME(), null, 1, 11, 1);
SET IDENTITY_INSERT [Post] OFF

SET IDENTITY_INSERT [PostTag] ON
INSERT INTO [PostTag] ([Id], [PostId], [TagId])
VALUES (1, 1, 3);
SET IDENTITY_INSERT [PostTag] OFF

SET IDENTITY_INSERT [Comment] ON
INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime])
VALUES (1, 1, 1, 'Article', 'One of the best reads in recent memory', SYSDATETIME());
SET IDENTITY_INSERT [Comment] OFF
