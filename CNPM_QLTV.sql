USE master
GO

create database CNPM_QLTV
go

use CNPM_QLTV
go



CREATE TABLE BOOK(
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[BookName] [nvarchar](Max) NOT NULL,
	[Author] [nvarchar](35) NULL,
	[Describe] [nvarchar](Max) NULL,
	[CategoryID] [int] NULL,
	[Publish] [date] NULL,
	[Publisher] [nvarchar](30) NULL,
	[BookImage] [varchar](50) NULL,
	[BookIssue] [varchar](1) NULL, ---Yes,No,Lost,Broken
	PRIMARY KEY (BookID)
);



CREATE TABLE BOOKRETURN(
	[ReturnID] [int] IDENTITY(1,1) NOT NULL,
	[ReturnDate] [datetime] NULL,
	[StudentID] [varchar](10) NOT NULL,
	[BookID] [int] NULL,
	PRIMARY KEY (ReturnID)
)



CREATE TABLE BORROW(
	[BorrowID] [int] IDENTITY(1,1) NOT NULL,
	StudentID varchar(10) NOT NULL,
	[BookID] [int] NULL,
	[TakenDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	PRIMARY KEY (BorrowID)
)



CREATE TABLE CATEGORY(
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](30) NOT NULL,
	[Stat] [varchar](1) NULL,
	PRIMARY KEY (CategoryID)
)



CREATE TABLE FINE(
	[FineID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [int] NULL,
	[ReturnID] [int] NULL,
	[FineAmount] [varchar](1) NULL,
	PRIMARY KEY (FineID)
)



CREATE TABLE LIBRARYCARD(
	[CardID] [varchar](7) NOT NULL,
	[StudentName] [nvarchar](25) NOT NULL,
	[DateExpires] [datetime] NULL,
	[Issue] [nvarchar](25) NULL,
	PRIMARY KEY (CardID),
	CONSTRAINT chk_CardID CHECK (CardID LIKE 'TV[0-9][0-9][0-9][0-9][0-9]')
)



CREATE TABLE STUDENT(
	StudentID varchar(10) NOT NULL,
	[CardID] [varchar](7) NULL,
	[Birth] [date] NULL,
	[Email] [nvarchar](30) NULL,
	[Phone] [varchar](10) NULL,
	[Addr] [nvarchar](100) NULL,
	[Gender] [char](10) NOT NULL,
	PRIMARY KEY (StudentID),
	CONSTRAINT chk_StudentID CHECK (StudentID LIKE '20DH[0-9][0-9][0-9][0-9][0-9]')
);



CREATE TABLE USERS(
	[UserID] [varchar](5),
	[Fullname] [nvarchar](50) NOT NULL,
	[Roles] [char](2) NULL,
	[Username] [nvarchar](15) NULL,
	[Password] [nvarchar](30) NULL,
	[Birth] [date] NULL,
	[Gender] [char](10) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](10) NULL,
	[Address] [nvarchar](100) NULL,
	PRIMARY KEY (UserID),
	CONSTRAINT chk_UserID CHECK (UserID LIKE '[0-9][0-9][0-9][0-9][0-9]')
)

----FK Book--
Alter table BOOK ADD CONSTRAINT FK_BOOK_CategoryID FOREIGN KEY (CategoryID) REFERENCES CATEGORY(CategoryID) 
GO

----FK Bookreturn---
ALTER TABLE BOOKRETURN ADD CONSTRAINT FK_BOOKRETURN_BookID FOREIGN KEY (BookID) REFERENCES BOOK(BookID)
ALTER TABLE BOOKRETURN ADD CONSTRAINT FK_BOOKRETURN_StudentID FOREIGN KEY (StudentID) REFERENCES STUDENT(StudentID)
GO

--FK BORROW--
ALTER TABLE BORROW ADD CONSTRAINT FK_BOBORROW_BookID FOREIGN KEY (BookID) REFERENCES BOOK(BookID)
ALTER TABLE BORROW ADD CONSTRAINT FK_BOBORROW_StudentID FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
GO

--FK FINE--
ALTER TABLE FINE ADD CONSTRAINT FK_FINE_ReturnID FOREIGN KEY (ReturnID) REFERENCES BOOKRETURN(ReturnID)
ALTER TABLE FINE ADD CONSTRAINT FK_FINE_BorrowID FOREIGN KEY (BorrowID) REFERENCES BORROW(BorrowID)
GO

--FK STUDENT--
ALTER TABLE STUDENT ADD CONSTRAINT FK_STUDENT_CardID FOREIGN KEY (CardID) REFERENCES LIBRARYCARD(CardID)
GO


-------------------------------------INSERT DATA------------------------------------------------
---Category---
insert into Category
values('Science', 'N'),
('Foreign Languages', 'N'),
('Economy', 'N'),
('Politic', 'N'),
('Business', 'N')



---Book---
insert into Book
values('The Pragmatic Programmer', 'Andy Hunt', 'Ward Cunningham Straight from the programming trenches, The Pragmatic Programmer cuts through the increasing specialization and technicalities of modern software development to examine the core process--taking a requirement and producing working, maintainable code that delights its users. It covers topics ranging from personal responsibility and career development to architectural techniques for keeping your code flexible and easy to adapt and reuse. Read this book, and you will learn how to *Fight software rot; *Avoid the trap of duplicating knowledge; *Write flexible, dynamic, and adaptable code; *Avoid programming by coincidence; *Bullet-proof your code with contracts, assertions, and exceptions; *Capture real requirements; *Test ruthlessly and effectively; *Delight your users; *Build teams of pragmatic programmers; and *Make your developments more precise with automation. Written as a series of self-contained sections and filled with entertaining anecdotes, thoughtful examples, and interesting analogies, The Pragmatic Programmer illustrates the best practices and major pitfalls of many different aspects of software development. Whether you are a new coder, an experienced program', 1, '1999/10/2', 'Addison Wesley', 'the-pragmatic-programmer.jpg', 'N'),
('The Clean Coder: A Code of Conduct for Professional Programmers', 'Robert Martin', 'Programmers who endure and succeed amidst swirling uncertainty and nonstop pressure share a common attribute: They care deeply about the practice of creating software. They treat it as a craft. They are professionals. In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship. This book is packed with practical advice-about everything from estimating and coding to refactoring and testing. It covers much more than technique: It is about attitude. Martin shows how to approach software development with honor, self-respect, and pride, work well and work clean, communicate and estimate faithfully, face difficult decisions with clarity and honesty; and understand that deep knowledge comes with a responsibility to act. Readers will learn What it means to behave as a true software craftsman How to deal with conflict, tight schedules, and unreasonable managers How to get into the flow of coding, and get past writers block How to handle unrelenting pressure and avoid burnout How to combine enduring attitudes with new development paradigms How to manage your time, and avoid blind alleys, marshes, bogs, and swamps How to foster environments where programmers and teams can thrive When to say "No"-and how to say it When to say "Yes"-and what yes really means Great software is something to marvel at: powerful, elegant, functional, a pleasure to work with as both a developer and as a user. Great software is not written by machines. It is written by professionals with an unshakable commitment to craftsmanship. The Clean Coder will help you become one of them-and earn the pride and fulfillment that they alone possess', 1, '2011/5/4', 'Addison Wesley', 'The clean coder.jpg', 'N'),
(N'HACKER LƯỢC SỬ', 'Steven Levy', N'Cuốn sách nói về những nhân vật, cỗ máy, sự kiện định hình cho văn hóa và đạo đức hacker từ những hacker đời đầu ở đại học MIT. Câu chuyện hấp dẫn bắt đầu từ các phòng thí nghiệm nghiên cứu máy tính đầu tiên đến các máy tính gia đình. Tập hợp tài liệu cập nhật từ các tin tặc nổi tiếng như Bill Gates, Mark Zuckerberg, Richard Stallman,..Những sự thật về cuộc sống và con đường trở thành “tin tặc” của những con người đã thay đổi lịch sử phát triển của ngành Công nghệ. Cuốn sách của Steven Levy ghi lại những chiến công của các tin tặc thời kỳ đầu trong cuộc cách mạng máy tính - những kẻ mê máy tính thông minh và lập dị từ cuối những năm 1950 đến đầu thập niên 1980, dám mạo hiểm, bẻ cong quy tắc và đẩy thế giới vào một hướng đi hoàn toàn mới.', 1, '2015/09/15', 'Addison Wesley', 'hacker-luoc-su.jpg', 'N'),
('CLEAN CODE', 'Robert Martin', 'Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way. Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer—but only if you work at it. What kind of work will you be doing? You’ll be reading code—lots of code. And you will be challenged to think about what’s right about that code, and what’s wrong with it. More importantly, you will be challenged to reassess your professional values and your commitment to your craft', 1, '2008/8/1', 'Addison Wesley', 'clean-code.jpg', 'N'),
('Oxford School Dictionary', 'Oxford', N'Một ấn bản mới chính của Từ điển Trường học Oxford toàn diện, đáng tin cậy, bán chạy nhất với hỗ trợ chương trình học mới. Ấn bản mới tuyệt vời này sẽ nâng cao vốn từ vựng của con bạn với những từ và nghĩa mới từ khắp chương trình học. Phần chính tả, ngữ pháp và dấu câu mới hữu ích được bao gồm để nâng cao kỹ năng ngôn ngữ và cập nhật từ điển', 3, '2016/09/12', 'Oxford School', 'Oxford-School-Dictionary.jpg', 'N'),
('Oxford Advanced Learners Dictionary 8th with Vietnamese Translation (PB)', 'Oxford', 'Từ điển Anh – Anh – Việt dựa trên ấn bản OALD, được Viện Ngôn ngữ học thẩm định nội dung. Một trong những cuốn từ điển được bán chạy nhất mọi thời đại và được phát triển từ 1948 với hơn 100 triệu người đang sử dụng.', 3, '1980/10/10', 'Oxford School', 'Oxford-Advanced-Learner-.jpg', 'N'),
('English Grammar in Use Book w Ans', 'Raymond Murphy', 'The worlds best-selling grammar series for learners of English. English Grammar in Use Fourth edition is an updated version of the worlds best-selling grammar title. It has a fresh, appealing new design and clear layout, with revised and updated examples, but retains all the key features of clarity and accessibility that have made the book popular with millions of learners and teachers around the world. This "with answers" version is ideal for self-study. An online version, book without answers, and book with answers and CD-ROM are available separately.', 3, '2017/4/5', 'Cambridge', 'English-Grammar-in-Use.jpg', 'N'),
(N'Marugoto - Ngôn Ngữ Và Văn Hóa Nhật Bản: Trung Cấp 1 - B1', 'Japan Foundation', N'“Marugoto - Ngôn ngữ và Văn hóa Nhật Bản” chính là bộ giáo trình đáp ứng hoàn hảo nhu cầu trên. Tựa đề Marugoto trong tiếng Nhật có nghĩa là “trọn vẹn”, chứa đựng thông điệp mà những người thực hiện muốn gửi gắm: Một bộ giáo trình học tiếng Nhật toàn diện, kết hợp giữa yếu tố ngôn ngữ và văn hóa, giữa kiến thức và thực hành thực tế. Với Marugoto, người học hướng đến việc sử dụng tiếng Nhật một cách “trọn vẹn”: Trọn vẹn trong giao tiếp chân thực giữa người với người, trọn vẹn trong trải nghiệm ngôn ngữ cùng đời sống và văn hóa.', 3, '2021/3/15', N'NXB Đại Học Quốc Gia TP.HCM', 'Marugoto-B1.png', 'N'),
(N'Giáo trình Marugoto A1 - Hiểu biết ngôn ngữ văn hóa Nhật', 'Japan Foundation', N'Marugoto Rikai - Ngôn ngữ và văn hóa Nhật Bản - Hiểu biết ngôn ngữ. Marugoto có nghĩa là Trọn vẹn, chứa đựng thông điệp về sự hài hòa giữ ngôn ngữ và văn hóa. giáo trình giới thiệu những tình huống giao tiếp thực tế, giúp người học trải nghiệm nhiều khía cạnh đa dạng của văn hóa Nhật Bản và đặc trưng trong đời sống của người Nhật một cách trọn vẹn.', 3, '2018/8/29', N'NXB Đại Học Quốc Gia TP.HCM', 'Marugoto-A1.jpg', 'N'),
(N'7000 Từ Vựng Tiếng Nhật Theo Chủ Đề', 'Huy Khang', N'Quyển sách “7000 từ vựng tiếng Nhật theo chủ đề” phân loại từ vựng theo 145 chủ đề, giới thiệu các bài học được nghiên cứu cẩn thận nhằm phát triển vốn từ vựng của học viên qua các chủ đề khác nhau. Bắt đầu qua các chủ đề thông dụng rồi dần dần mở rộng qua các chủ đề rộng lớn, bao quát thế giới xung quanh. Các bài học đầu tiên về gia đình, nhà ở, các hoạt động về cộng đồng, trường học, nơi làm việc, mua sắm, giải trí và các chủ đề thông dụng khác.', 3, '2020-12-01', N'NXB Thanh Niên','7000-tu-vung-Tieng-Nhat.jpg', 'N')



--LIBRARY CARD--
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV79373', 'Venita Truwert', '2023/04/01', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08361', 'Heinrick Hutable', '2022/11/07', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV72251', 'Cam Laurenzi', '2022/12/08', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV19926', 'Ritchie Lax', '2022/12/03', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV23349', 'Brianne Elcoate', '2022/08/05', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV37292', 'Arny Alcott', '2022/06/23', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV76654', 'Abbey Napper', '2022/09/12', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV97184', 'Skip Sidebottom', '2023/03/11', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV92759', 'Hodge Netting', '2022/09/28', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV63660', 'Nevsa Cuddy', '2022/10/16', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV63660', 'Sue Mincher', '2022/09/24', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV37292', 'Jedd Schall', '2022/04/15', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08361', 'Christal Beardwell', '2023/02/24', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV72251', 'Melvin Dewar', '2022/04/28', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV99973', 'Dominick Cholomin', '2022/08/22', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64480', 'Roxana Karlolak', '2022/05/29', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV76654', 'Winslow Tesauro', '2022/10/28', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV05707', 'Portia Elverston', '2023/04/08', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64329', 'Carry Purse', '2022/10/12', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV14233', 'Frannie Bertomieu', '2022/06/27', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV72251', 'Yuma Glanert', '2022/11/05', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV61867', 'Beatrice McCook', '2023/04/10', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV18478', 'Sheena Polack', '2022/11/08', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV89454', 'Alexis Jeckell', '2022/12/19', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08049', 'Crista Buckles', '2022/12/19', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV36445', 'Micaela Hagan', '2022/09/22', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV41308', 'Bridie Housiaux', '2023/02/23', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV46339', 'Rose Trodler', '2022/12/12', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV90673', 'Emmye Mattis', '2022/11/15', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV90673', 'Gordon Valente', '2022/06/21', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV61867', 'Ashia Antecki', '2022/12/04', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV43774', 'Rickey Esley', '2022/12/05', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV56818', 'Antony Eldredge', '2022/11/09', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV97184', 'Quinlan Fieldhouse', '2022/08/12', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV89454', 'Cathyleen Bamling', '2022/10/04', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV05707', 'Ethyl Shooter', '2022/10/29', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV23349', 'Chan Huckleby', '2022/11/01', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV42374', 'Abdul Harrhy', '2023/02/27', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV53103', 'Margery Darington', '2023/02/02', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV14233', 'Janessa Apark', '2022/07/04', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV56818', 'Kermie Thunder', '2022/11/25', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV42374', 'Aurea Benjamin', '2023/01/01', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV89454', 'Shelba Bayfield', '2022/10/18', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV25540', 'Josy Routham', '2022/09/27', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV83489', 'Neddie Maysor', '2022/11/04', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV68400', 'Lock Teall', '2022/11/12', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV56818', 'Kris Kryzhov', '2023/01/27', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64498', 'Lavena Blagdon', '2023/04/10', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV99703', 'Forest Meaking', '2023/03/22', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV21193', 'Meredithe Lindenberg', '2022/12/04', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV32052', 'Lissy Kryska', '2022/10/22', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV36445', 'Kacie Poizer', '2023/02/11', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV90673', 'Aida Moreman', '2022/06/17', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08361', 'Prentice Barense', '2022/11/27', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV60370', 'Cris Itzak', '2022/09/16', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV48353', 'Rudd Penhaligon', '2022/12/18', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV17804', 'Lynnell Garrish', '2022/11/28', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV92759', 'Paloma Kauschke', '2022/08/04', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV41308', 'Kalle Bray', '2023/01/19', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV42374', 'Dennis Kill', '2023/02/02', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV53103', 'Ermina Emburey', '2022/11/29', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV94917', 'Lacey Skelington', '2023/02/20', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV54173', 'Lemmy Cumberledge', '2023/03/07', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV11543', 'Patten Andryushchenko', '2022/12/13', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV14233', 'Andrej Bicknell', '2022/12/01', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV89454', 'Griselda Lerwill', '2022/10/24', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV61867', 'Mireielle Simons', '2022/12/04', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV99703', 'Joey Kasper', '2022/09/02', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV43774', 'Isabelita Dunley', '2023/03/16', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV02977', 'Marcelle Narracott', '2022/06/17', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV84115', 'Carley Fairlamb', '2022/07/20', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08049', 'Terry Ruddick', '2022/08/16', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV08693', 'Roda Ganiclef', '2022/04/21', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV25540', 'Zechariah O''Kieran', '2023/02/18', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV79210', 'Rosanna Gozard', '2023/02/27', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV05707', 'Nert Linn', '2023/02/05', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV56818', 'Abdul Medlen', '2022/05/17', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV59140', 'Kylen Cussins', '2022/09/16', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV98352', 'Carl Dahlbom', '2022/11/05', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV37292', 'Cristionna Croxton', '2023/03/27', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64480', 'Ebeneser Hewertson', '2022/05/02', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV99973', 'Lyndsie Sans', '2023/02/06', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV23349', 'Kimmie Lademann', '2022/10/08', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64498', 'Rhett Gabriel', '2022/11/24', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV63660', 'Giacomo Remirez', '2023/02/19', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV64329', 'Verine Henworth', '2022/04/24', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV79373', 'Wallache Crabb', '2022/05/22', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV83489', 'Erma Gillease', '2022/07/05', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV44577', 'Glenden Drew-Clifton', '2022/08/23', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV65721', 'Nevsa Shone', '2022/07/29', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV73859', 'Consuela Heathfield', '2022/08/11', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV18478', 'Cchaddie Stevens', '2022/07/09', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV45309', 'Thurstan McVey', '2022/06/25', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV97184', 'Lukas Netley', '2022/10/04', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV46339', 'Nathan Sea', '2022/10/06', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV68400', 'Van Whaites', '2023/02/01', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV72251', 'Jeanne Ciciari', '2022/07/01', 'Không sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV19926', 'Delainey Ringham', '2022/11/28', 'Đang sử dụng');
insert into LIBRARYCARD (CardID, StudentName, DateExpires, Issue) values ('TV76654', 'Grata Tipling', '2022/12/15', 'Không sử dụng');
----------------------


-----STUDENT-----
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH41505', 'TV56818', '2023/02/05', 'prenouf0@blogtalkradio.com', '9198693976', '4 Dunning Parkway', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH91920', 'TV63660', '2022/06/22', 'sgarth1@i2i.jp', '7767081359', '06 Armistice Plaza', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH91868', 'TV54173', '2022/05/28', 'ddrayton2@wired.com', '2497192905', '196 Kedzie Crossing', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH48752', 'TV43774', '2022/12/24', 'bmenichelli3@cnbc.com', '5121284658', '30 Garrison Pass', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH90249', 'TV45309', '2022/07/13', 'cchalles4@cocolog-nifty.com', '4003134427', '5 Myrtle Park', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH91545', 'TV42374', '2022/05/28', 'sbisacre5@sakura.ne.jp', '3825705881', '887 Thompson Road', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH14749', 'TV60370', '2022/06/27', 'ctaffee6@yale.edu', '5093461830', '417 Anderson Alley', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH97539', 'TV76654', '2023/01/01', 'umcvicker7@reference.com', '4504682992', '0 Graceland Pass', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH80064', 'TV64480', '2022/06/20', 'hmcfadden8@bbb.org', '8343647677', '878 South Court', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH05477', 'TV64329', '2022/06/08', 'nivushkin9@nih.gov', '4107319982', '048 Duke Lane', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH13833', 'TV64498', '2023/03/04', 'ckealya@edublogs.org', '5664035342', '867 Coolidge Street', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH51740', 'TV90673', '2022/10/01', 'wmcguinessb@acquirethisname.com', '3214735366', '313 Bunting Junction', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH84120', 'TV98352', '2022/10/23', 'apluvierc@dell.com', '8092484317', '54 Sunnyside Crossing', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH00202', 'TV97184', '2023/01/03', 'rmorrilld@latimes.com', '8406490178', '566 Prairie Rose Avenue', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH87945', 'TV42374', '2023/01/27', 'grussane@ustream.tv', '8779220153', '95 Ridgeway Road', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH46066', 'TV79373', '2022/06/08', 'imosef@wix.com', '5422823036', '9 Commercial Park', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH84399', 'TV19926', '2022/05/01', 'jblodgettg@gov.uk', '2379676174', '36 Esker Junction', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH33706', 'TV21193', '2023/04/06', 'nkynochh@ucsd.edu', '5446070560', '95 Oriole Crossing', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH51744', 'TV37292', '2022/06/07', 'dselwynei@istockphoto.com', '1656431966', '44 Glacier Hill Way', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH54325', 'TV48353', '2022/10/14', 'aostlerj@sina.com.cn', '1364663778', '2035 Oak Valley Court', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH91477', 'TV99973', '2022/10/02', 'lwilesk@1und1.de', '7444405064', '94 Knutson Point', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH63526', 'TV32052', '2022/12/30', 'agarralsl@clickbank.net', '1911206321', '4 Bellgrove Alley', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH74791', 'TV08049', '2023/01/29', 'alatliffm@time.com', '3112051187', '2276 Eliot Plaza', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH20920', 'TV99703', '2022/05/28', 'rkingtonn@cbsnews.com', '4881494930', '4 American Drive', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH67503', 'TV64498', '2022/06/05', 'gmayoo@whitehouse.gov', '9392633125', '49066 Golf Avenue', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH05676', 'TV83489', '2022/08/03', 'xsaurinp@cnet.com', '8779721533', '8 Jackson Center', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH19650', 'TV99703', '2023/03/24', 'bcelliq@1und1.de', '3901810703', '38 High Crossing Road', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH26732', 'TV72251', '2023/03/18', 'npepisr@nsw.gov.au', '5793313707', '96 Norway Maple Place', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH19048', 'TV42374', '2023/03/26', 'dbarghs@discuz.net', '7003582207', '3837 Boyd Parkway', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH70756', 'TV08693', '2022/09/18', 'csivornt@ed.gov', '8683482123', '06886 Annamark Pass', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH57224', 'TV45309', '2022/05/25', 'ypettiu@tinypic.com', '8881178072', '7522 Northland Avenue', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH99403', 'TV99703', '2023/01/20', 'dstuckv@wikimedia.org', '8235497501', '67 Manufacturers Lane', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH04078', 'TV45309', '2022/07/21', 'tmeerew@wix.com', '6751133200', '1 Rieder Center', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH57697', 'TV64498', '2023/03/14', 'rsketx@jigsy.com', '8372296942', '67042 Carioca Parkway', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH71239', 'TV84115', '2023/03/05', 'nvousdeny@apple.com', '2119724353', '55 Blaine Parkway', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH52948', 'TV18478', '2023/03/02', 'wgreyz@de.vu', '9138732955', '6401 Hoepker Junction', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH00091', 'TV79210', '2022/04/17', 'rwildey10@sciencedaily.com', '5631145069', '81 Raven Street', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH78024', 'TV25540', '2022/08/07', 'madelman11@independent.co.uk', '3568588540', '94 7th Court', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH01705', 'TV83489', '2022/05/21', 'tpardal12@facebook.com', '6919678716', '2511 Lillian Drive', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH47346', 'TV37292', '2022/09/17', 'bmccullen13@tinyurl.com', '7961653880', '07703 Milwaukee Plaza', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH62126', 'TV17804', '2022/11/06', 'jchishull14@instagram.com', '2543450685', '93 Eliot Court', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH89700', 'TV42374', '2023/01/18', 'mconre15@huffingtonpost.com', '7372777923', '530 Lakeland Avenue', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH48754', 'TV17804', '2023/03/19', 'rsclater16@cyberchimps.com', '1748328164', '31 Kenwood Park', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH97582', 'TV83489', '2022/06/05', 'bfreemantle17@microsoft.com', '5972744932', '0 Porter Junction', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH23956', 'TV60370', '2022/05/11', 'mmullins18@archive.org', '1596256366', '1 Forster Avenue', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH85485', 'TV48353', '2022/04/16', 'dduhig19@vk.com', '8119124601', '5 Hermina Circle', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH12052', 'TV41308', '2022/10/28', 'cmuckle1a@creativecommons.org', '2412384629', '8 Fairview Parkway', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH92603', 'TV76654', '2023/04/08', 'acliss1b@macromedia.com', '9799213702', '31 Butternut Center', 'Male');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH01804', 'TV54173', '2023/01/21', 'hthackwray1c@ow.ly', '8845450123', '974 Grayhawk Lane', 'Female');
insert into STUDENT (StudentID, CardID, Birth, Email, Phone, Addr, Gender) values ('20DH52111', 'TV99703', '2022/04/17', 'glethardy1d@github.com', '1316065508', '5 Drewry Circle', 'Female');


-------BORROW------------
insert into BORROW
values ('20DH12052', 1, '2023/01/26', '2023/04/05'),
('20DH97582', 2, '2022/10/23', '2022/09/26'),
('20DH47346', 9, '2022/07/15', '2023/03/08'),
('20DH71239', 5, '2022/08/03', '2023/02/13'),
('20DH01804', 10, '2022/01/26', '2022/05/13'),
('20DH52111', 3, '2022/12/29', '2023/03/25'),
('20DH85485', 7, '2023/01/26', '2023/06/13'),
('20DH89700', 6, '2019/12/30', '2020/11/29'),
('20DH78024', 4, '2021/01/12', '2021/02/20')


---------BOOK RETURN--------
insert into BOOKRETURN
values('2023/04/01', '20DH85485', 1),
('2022/09/26', '20DH47346', 4),
('2022/09/26', '20DH52111', 5),
('2022/09/26', '20DH01804', 10),
('2022/09/26', '20DH78024', 9),
('2022/09/26', '20DH71239', 8),
('2022/09/26', '20DH89700', 3)



---------USER----------------
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('69675', 'Barret Rieflin', 'AD', 'brieflin0', 'ItmWEqhFGv', '2022/07/01', 'Male', 'brieflin0@tamu.edu', '7566759315', 'Room 1818');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('90525', 'Charles Frunks', 'AD', 'cfrunks1', 'X1lUQ0vfS', '2022/09/18', 'Polygender', 'cfrunks1@pagesperso-orange.fr', '5402186512', 'Suite 52');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('41903', 'Katalin Gowen', 'US', 'kgowen2', 'ZvZNsrXg', '2022/12/02', 'Female', 'kgowen2@sitemeter.com', '7497598095', 'Suite 29');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('86571', 'Hayley Arondel', 'US', 'harondel3', 'IqKj0L2DpW7', '2022/06/09', 'Female', 'harondel3@e-recht24.de', '5392405863', 'Suite 61');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('64433', 'Domini Dellatorre', 'AD', 'ddellatorre4', 'pkHyHt0u6ZyN', '2023/02/11', 'Female', 'ddellatorre4@sina.com.cn', '3051816998', 'Suite 98');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('32538', 'Jeana Girke', 'AD', 'jgirke5', 'PXU8El3', '2022/06/18', 'Female', 'jgirke5@ocn.ne.jp', '2628651196', '17th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('40523', 'Glyn Lucien', 'US', 'glucien6', 'X4mfRJNptlh', '2022/09/20', 'Male', 'glucien6@yellowbook.com', '6309553746', '4th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('79306', 'Lira Stredwick', 'AD', 'lstredwick7', 'uatDRz', '2023/03/15', 'Female', 'lstredwick7@163.com', '3329240847', 'Suite 74');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('58753', 'Fania Fetherstone', 'US', 'ffetherstone8', 'kK4sL1d', '2023/03/12', 'Female', 'ffetherstone8@yandex.ru', '8917843885', 'Apt 577');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('26810', 'Rancell Medcraft', 'AD', 'rmedcraft9', 'z2aMqRE', '2022/08/27', 'Genderfluid', 'rmedcraft9@tinyurl.com', '6921487049', '9th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('24575', 'Alethea Bacop', 'AD', 'abacopa', '4TkLgrVeQ', '2022/06/11', 'Female', 'abacopa@skyrock.com', '2527105558', 'Suite 82');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('12924', 'Vladamir Duffan', 'AD', 'vduffanb', 'vLOy3ElmMCZ', '2022/06/07', 'Male', 'vduffanb@cnet.com', '3732251906', 'Suite 98');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('31653', 'Caresse McElroy', 'AD', 'cmcelroyc', 'mbcRWj', '2022/11/07', 'Female', 'cmcelroyc@aboutads.info', '2803308707', 'Apt 617');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('68050', 'Flinn Bispo', 'AD', 'fbispod', '5DdgcdkR', '2022/12/25', 'Male', 'fbispod@irs.gov', '7242987355', 'Room 139');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('25857', 'Juditha Sextie', 'AD', 'jsextiee', 'zLckz2', '2023/03/14', 'Female', 'jsextiee@flavors.me', '9727452588', 'Apt 1831');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('45648', 'Arvy Chastan', 'AD', 'achastanf', 'vdigT1k', '2022/10/24', 'Male', 'achastanf@msn.com', '3196288961', '13th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('96502', 'Yank Messum', 'AD', 'ymessumg', 'KdK2BSP2pP', '2023/03/01', 'Male', 'ymessumg@dagondesign.com', '3957287014', 'Suite 48');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('68830', 'Sergei Martinovic', 'AD', 'smartinovich', '1McBMOtICcj', '2023/03/02', 'Male', 'smartinovich@state.gov', '5944691686', 'Room 490');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('84948', 'Heddi Caneo', 'US', 'hcaneoi', 'BnYX7Pk3', '2022/10/28', 'Non-binary', 'hcaneoi@google.co.uk', '1252520153', 'Room 926');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('62257', 'Eddi Asson', 'US', 'eassonj', '20LVdMAykqdy', '2022/04/20', 'Bigender', 'eassonj@elpais.com', '2317899461', '9th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('84622', 'Marlon Otter', 'AD', 'motterk', '1xcG7t5f', '2022/07/24', 'Male', 'motterk@liveinternet.ru', '1778349791', 'Apt 731');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('19633', 'Meredeth Costard', 'AD', 'mcostardl', 'RGGtu2C1WMme', '2023/02/20', 'Male', 'mcostardl@cornell.edu', '4711204402', 'Room 1677');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('53525', 'Thorny Culleford', 'US', 'tcullefordm', 'AL3WLRhQp1R', '2023/01/20', 'Male', 'tcullefordm@thetimes.co.uk', '3664588571', 'Suite 34');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('61730', 'Clarette Shapcott', 'AD', 'cshapcottn', 'xEtkNbH', '2022/10/27', 'Female', 'cshapcottn@addthis.com', '1389763288', 'Apt 678');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('17782', 'Solomon Fawks', 'AD', 'sfawkso', 'v8nZN5WeTzC', '2022/06/18', 'Male', 'sfawkso@usnews.com', '2737347079', 'Room 1044');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('75654', 'Bernelle Bleaden', 'US', 'bbleadenp', 'zCH7Mu3v7VUD', '2022/06/14', 'Female', 'bbleadenp@netlog.com', '3743675336', 'Room 1628');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('88376', 'Birdie Mose', 'US', 'bmoseq', 'JhwwMn4rFSb', '2022/04/14', 'Female', 'bmoseq@reddit.com', '5645296805', 'Apt 759');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('04687', 'Paton Winch', 'US', 'pwinchr', 'lPYc2ZeP', '2022/04/23', 'Male', 'pwinchr@twitpic.com', '4506840170', '19th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('07635', 'Tisha Verrick', 'US', 'tverricks', 'fkvmIZNX', '2022/12/02', 'Genderqueer', 'tverricks@opera.com', '5477273293', 'Room 75');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('16762', 'Lorrayne Pentony', 'AD', 'lpentonyt', 's4bFmJgt', '2022/12/08', 'Female', 'lpentonyt@barnesandnoble.com', '7411129855', 'PO Box 87450');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('20172', 'Georgina Lodwick', 'AD', 'glodwicku', 'CrXaCCVD8nq0', '2023/02/26', 'Female', 'glodwicku@etsy.com', '1053254951', 'Apt 1387');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('96799', 'Tybie Kidson', 'AD', 'tkidsonv', 'OCwwESa3LaP', '2022/11/01', 'Female', 'tkidsonv@etsy.com', '6918295685', 'Apt 620');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('49979', 'Otho Galvin', 'US', 'ogalvinw', 'rferivIrr', '2022/12/21', 'Male', 'ogalvinw@zdnet.com', '1376211222', 'Room 1501');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('15845', 'Maison Island', 'US', 'mislandx', 'eosV0JGe9T', '2022/11/05', 'Male', 'mislandx@rediff.com', '8545210919', 'Room 1901');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('94236', 'Venus Lissaman', 'AD', 'vlissamany', 'pTM3YDaaMCA', '2023/03/03', 'Female', 'vlissamany@howstuffworks.com', '2437145931', 'PO Box 97340');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('83190', 'Billy Shipley', 'US', 'bshipleyz', 'SHK5qjXDvM', '2023/02/25', 'Female', 'bshipleyz@yellowpages.com', '5907278425', 'PO Box 13127');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('87699', 'Angel Faragher', 'US', 'afaragher10', '3ISpk4D', '2023/03/12', 'Female', 'afaragher10@adobe.com', '4107673665', '5th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('61165', 'Sayre Mars', 'US', 'smars11', '7jNpOrW1u2', '2022/06/10', 'Female', 'smars11@netscape.com', '3019266021', 'Suite 44');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('12743', 'Britteny Scherer', 'AD', 'bscherer12', 'IBoVwr4K5UL', '2022/07/11', 'Female', 'bscherer12@vimeo.com', '2514716106', '17th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('71405', 'Johanna Swindle', 'AD', 'jswindle13', 'alm5ZqeHG0iJ', '2023/02/07', 'Female', 'jswindle13@acquirethisname.com', '8974574248', 'Room 772');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('89517', 'Eddy Goldstraw', 'US', 'egoldstraw14', 'UNPoMX6kUG', '2022/06/21', 'Polygender', 'egoldstraw14@feedburner.com', '4878082759', 'Suite 36');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('68290', 'Geordie Richford', 'AD', 'grichford15', 'AY2jwE', '2022/09/20', 'Male', 'grichford15@plala.or.jp', '7682267105', 'Room 12');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('96190', 'Wilmette Delion', 'US', 'wdelion16', 'ueZWhEs0J', '2023/04/03', 'Female', 'wdelion16@cisco.com', '7716091696', 'Apt 1371');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('91892', 'Valaria Borley', 'AD', 'vborley17', 'inDp3IKRlNmQ', '2022/05/27', 'Female', 'vborley17@dot.gov', '5096281171', 'PO Box 45666');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('45638', 'Mohandis Swinney', 'US', 'mswinney18', 'CjwB0Ax', '2022/12/19', 'Male', 'mswinney18@twitter.com', '2968716493', 'PO Box 95038');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('55298', 'Isadore Loghan', 'AD', 'iloghan19', 'yRz5YkoP', '2022/11/13', 'Male', 'iloghan19@wufoo.com', '8063331217', '17th Floor');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('73534', 'Fidel MacAskie', 'US', 'fmacaskie1a', 'tRNQmpE4', '2022/04/21', 'Male', 'fmacaskie1a@sciencedaily.com', '3351249904', 'Room 1744');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('02489', 'Dorolisa Grabeham', 'AD', 'dgrabeham1b', 'CyiBXn2uzct', '2022/08/18', 'Female', 'dgrabeham1b@wired.com', '5514864848', 'Room 1558');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('43879', 'Travus Axelbee', 'US', 'taxelbee1c', 'IGMI4b', '2022/08/15', 'Genderfluid', 'taxelbee1c@nasa.gov', '9792247482', 'PO Box 43065');
insert into USERS (UserID, Fullname, Roles, Username, Password, Birth, Gender, Email, Phone, Address) values ('59132', 'Cordelia Morant', 'AD', 'cmorant1d', 'uG0AABW25G', '2022/07/16', 'Female', 'cmorant1d@vistaprint.com', '3813530423', 'Room 932');

