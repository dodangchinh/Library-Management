USE master
GO
CREATE DATABASE QuanLyThuVien
GO
USE QuanLyThuVien
Go

-- -------------------------------
-- Table 1: structure for Role
-- -------------------------------
CREATE TABLE [Role]
(
	[Id] NVARCHAR(2) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Status] BIT NOT NULL,
)

INSERT INTO [Role]([Id], [Name], [Status]) 
VALUES
	(N'R1', N'Admin', 1),
	(N'R2', N'Librarian', 1),
	(N'R3', N'Guest', 1)

-- -------------------------------
-- Table 2: structure for Account
-- -------------------------------
CREATE TABLE [Account]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Username] NVARCHAR(10) NOT NULL,
	[Password] NVARCHAR(128) NOT NULL,
	[Status] BIT NOT NULL,
)

INSERT INTO [Account] ([Name], [Username], [Password], [Status])
VALUES 
('Admin', 'admin', '21232F297A57A5A743894AE4A801FC3', 1),
('Librarian', 'librarian', '35FA1BCB6FBFA7AA343AA7F253507176', 1);


-- -------------------------------
-- Table 3: structure for AccountRole
-- -------------------------------
CREATE TABLE [AccountRole]
(
	[No] INT IDENTITY(1,1) NOT NULL,
	[AccountID] INT NOT NULL,
	[RoleID] NVARCHAR(2) NOT NULL,

	FOREIGN KEY ([AccountID]) REFERENCES [Account](Id),
	FOREIGN KEY ([RoleID]) REFERENCES [Role](Id),
)

INSERT INTO [AccountRole] ([AccountID], [RoleID])
VALUES 
(1, 'R1'),
(2, 'R2');


-- -------------------------------
-- Table 4: structure for Reader
-- -------------------------------
CREATE TABLE [Reader]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL,
	[Status] BIT NOT NULL,
)

INSERT INTO [Reader] ([Id], [Name], [Status])
VALUES
    (N'R001', N'Nguyen Van A', 1),
    (N'R002', N'Tran Thi B', 1),
    (N'R003', N'Le Van C', 0);

-- -------------------------------
-- Table 5: structure for ReaderInformation
-- -------------------------------
CREATE TABLE [ReaderInformation]
(
	[ReaderID] NVARCHAR(5) NOT NULL,
	[BOF] DATETIME NOT NULL,
	[CitizenID] INT NOT NULL,
	[Email] NVARCHAR(250) NOT NULL,
	[Gender] BIT NOT NULL,

	FOREIGN KEY ([ReaderID]) REFERENCES [Reader](Id),
)

INSERT INTO [ReaderInformation] ([ReaderID], [BOF], [CitizenID], [Email], [Gender])
VALUES
    (N'R001', '1990-01-01', 123456789, 'dangchinh2409@gmail.com', 1),
    (N'R002', '1992-02-15', 987654321, 'chinhdo633@gmail.com', 0),
    (N'R003', '1988-03-20', 456789123, 'luutrudammay123@gmail.com', 1);

-- -------------------------------
-- Table 6: structure for CategoryBook
-- -------------------------------
CREATE TABLE [CategoryBook]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[Status] BIT NOT NULL,
)

INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C01', N'Truyện tranh', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C02', N'Tiểu thuyết', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C03', N'Luật Pháp', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C04', N'Kỹ năng sống và phát triển bản thân', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C05', N'Tiểu thuyết huyền bí và triết học', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C06', N'Tiểu thuyết và tâm lý và xã hội', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C07', N'Tiểu thuyết và lịch sử và chiến tranh', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C08', N'Hài hước và trào phúng và xã hội', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C09', N'Tiểu thuyết và thiếu nhi và tình cảm', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C10', N'Tiểu thuyết và lịch sử và tình cảm', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C11', N'Tiểu thuyết và chiến tranh và tình cảm', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C12', N'Tiểu thuyết và lịch sử và văn hóa', 1)
INSERT [dbo].[CategoryBook] ([Id], [Name], [Status]) VALUES (N'C13', N'Tiểu thuyết và đời sống và tình cảm', 1)


-- -------------------------------
-- Table 7: structure for Author
-- -------------------------------
CREATE TABLE [Author]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[BOF] DATETIME not null,
	[Status] BIT NOT NULL,
)

INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A01', N'Nguyễn Nhật Ánh', '01/02/1995', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A02', N'Ngô Thị Thúy Vân', '01/07/2005', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A03', N'Lê Minh Tú', '01/03/1999', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A04', N'Fujiko Fujio', '01/12/1962', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A05', N'Nguyễn Thị Hà', '01/11/2007', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A06', N'Dale Carnegie', '01/01/1936', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A07', N'Paulo Coelho', '01/04/1970', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A08', N'Fyodor Dostoyevsky', '01/11/1821', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A09', N'Tố Tâm', '01/08/1950', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A10', N'Trịnh Công Sơn', '01/10/1965', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A11', N'Nguyễn Nhật Ánh', '01/05/2003', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A12', N'Dương Hướng', '01/04/1955', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A13', N'Đoàn Giỏi', '01/04/1955', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A14', N'Lê Lựu', '01/03/1947', 1)
INSERT [dbo].[Author] ([Id], [Name], [boF], [Status]) VALUES (N'A15', N'Nguyễn Ngọc Tư', '01/06/2010', 1)



-- -------------------------------
-- Table 8: structure for Publisher
-- -------------------------------
CREATE TABLE [Publisher]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Phone] INT NOT NULL,
	[Address] NVARCHAR(30) NOT NULL,
)

insert into [Publisher]
values
('P01', N'NXB Kim Đồng', '0901931765', N'79 Hoàng Hoa Thám'),
('P02', N'NXB Kim Lân', '0901000765', N'79 Hoàng Hoa Thụ'),
('P03', N'NXB Hải Quân', '0101000765', N'79 Hoàng Hoa Hái')

-- -------------------------------
-- Table 9: structure for Language
-- -------------------------------
CREATE TABLE [Language]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);

INSERT INTO [Language] ([Id], [Name])
VALUES
    (N'L001', N'Tiếng Việt'),
    (N'L002', N'Tiếng Anh'),
    (N'L003', N'Tiếng Pháp'),
    (N'L004', N'Tiếng Đức'),
    (N'L005', N'Tiếng Tây Ban Nha'),
    (N'L006', N'Tiếng Bồ Đào Nha'),
    (N'L007', N'Tiếng Nga'),
    (N'L008', N'Tiếng Trung Quốc'),
    (N'L009', N'Tiếng Nhật'),
    (N'L010', N'Tiếng Hàn Quốc'),
    (N'L011', N'Tiếng Ý'),
    (N'L012', N'Tiếng Thái'),
    (N'L013', N'Tiếng Ả Rập'),
    (N'L014', N'Tiếng Hy Lạp'),
    (N'L015', N'Tiếng Hà Lan'),
    (N'L016', N'Tiếng Thổ Nhĩ Kỳ'),
    (N'L017', N'Tiếng Ba Lan'),
    (N'L018', N'Tiếng Hungary'),
    (N'L019', N'Tiếng Thụy Điển'),
    (N'L020', N'Tiếng Na Uy'),
    (N'L021', N'Tiếng Đan Mạch'),
    (N'L022', N'Tiếng Phần Lan'),
    (N'L023', N'Tiếng Séc'),
    (N'L024', N'Tiếng Slovakia'),
    (N'L025', N'Tiếng Hindi'),
    (N'L026', N'Tiếng Urdu'),
    (N'L027', N'Tiếng Bengal'),
    (N'L028', N'Tiếng Tamil'),
    (N'L029', N'Tiếng Telugu'),
    (N'L030', N'Tiếng Punjabi'),
    (N'L031', N'Tiếng Malay'),
    (N'L032', N'Tiếng Indonesia'),
    (N'L033', N'Tiếng Swahili'),
    (N'L034', N'Tiếng Zulu'),
    (N'L035', N'Tiếng Afrikaans'),
    (N'L036', N'Tiếng Hebrew'),
    (N'L037', N'Tiếng Romania'),
    (N'L038', N'Tiếng Bulgaria'),
    (N'L039', N'Tiếng Croatia'),
    (N'L040', N'Tiếng Serbia'),
    (N'L041', N'Tiếng Slovenia'),
    (N'L042', N'Tiếng Bosnia'),
    (N'L043', N'Tiếng Macedonia'),
    (N'L044', N'Tiếng Latvia'),
    (N'L045', N'Tiếng Lithuania'),
    (N'L046', N'Tiếng Estonia'),
    (N'L047', N'Tiếng Khmer'),
    (N'L048', N'Tiếng Lào'),
    (N'L049', N'Tiếng Myanmar'),
    (N'L050', N'Tiếng Gujarati'),
	(N'L051', N'Tiếng Breton'),
	(N'L052', N'Tiếng Hausa'),
    (N'L053', N'Tiếng isiZulu'),
    (N'L054', N'Tiếng Langi'),
    (N'L055', N'Tiếng Asu'),
    (N'L056', N'Tiếng Afar'),
    (N'L057', N'Tiếng Basaa');


-- -------------------------------
-- Table 10: structure for Translator
-- -------------------------------
create table Translator (
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[BOF] DATETIME not null,
	[Status] BIT NOT NULL,
)

INSERT INTO [Translator] ([Id], [Name], [boF], [Status])
VALUES
	('T01', N'Nguyễn Nhật Ánh', '05/11/1980', 1),
	('T02', N'Ngô Thị Thúy Vân', '02/07/1999', 1),
	('T03', N'Lê Minh Tú', '02/11/1994', 1),
	('T04', N'Fujiko Fujio', '01/12/1933', 1),
	('T05', N'Nguyễn Thị Hà', '02/01/2001', 1);

-- -------------------------------
-- Table 11: structure for BookTitle
-- -------------------------------
create table BookTitle (
	[Id] nvarchar(10) not null primary key,
	[CategoryBookID] nvarchar(5) not null,
	[Name] nvarchar(100) not null,
	[Summary] ntext not null,
	[UrlImage] nvarchar(100) not null,
	
	foreign key ([CategoryBookID]) references CategoryBook([Id])
)

INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT01', N'C03', N'Tư Duy Pháp Lý Của Luật Sư', N'Nội dung hấp dẫn', '~/Content/Images/TuDuyPhapLyCuaLuatSu.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT02', N'C01', N'Doraemon', N'Cuốn sách rất thú vị và đáng đọc', '~/Content/Images/Doraemon.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT03', N'C02', N'Giết con chim nhại', N'Một cuốn sách tuyệt vời cho những người yêu thích thể loại tiểu thuyết', '~/Content/Images/GietConChimNhai.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT04', N'C02', N'Con chim xanh biếc bay về', N'Tác giả đã tạo ra một câu chuyện đầy cảm hứng và ý nghĩa', '~/Content/Images/ConChimXanhBiecBayVe.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT05', N'C04', N'Đắc Nhân Tâm', N'Sách giới thiệu những nguyên tắc vàng trong việc đối nhân xử thế, thu phục lòng người và tạo ảnh hưởng tích cực.', '~/Content/Images/DacNhanTam.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT06', N'C05', N'Nhà Giả Kim', N'Sách kể về hành trình phiêu lưu của Santiago, một chàng chăn cừu người Tây Ban Nha, đi tìm kho báu ẩn giấu ở Ai Cập. Trên đường đi, anh gặp nhiều người và trải qua nhiều thử thách, đồng thời khám phá ra ý nghĩa sâu sắc của cuộc sống và tình yêu.', '~/Content/Images/NhaGiaKim.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT07', N'C06', N'Tội Ác Và Hình Phạt', N'Sách xoay quanh câu chuyện của Raskolnikov, một sinh viên nghèo khổ ở Sankt-Peterburg, quyết định giết chết một bà già cho vay nặng lãi để lấy tiền. Sau khi gây án, anh phải đối mặt với những cơn ác mộng, nỗi hối hận và sự truy lùng của cảnh sát. Anh cũng', '~/Content/Images/ToiAcVaHinhPhat.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT08', N'C07', N'Vàng Và Máu', N'Sách tái hiện lại cuộc chiến tranh Đông Dương lần thứ nhất giữa Pháp và Việt Nam, qua góc nhìn của một người lính Pháp, Tố Tâm, người yêu nước Việt Nam và ghét sự đàn áp của đế quốc. Sách là một tác phẩm độc đáo, mang âm hưởng của văn học hiện thực và bi kịch nhân sinh.', '~/Content/Images/VangVaMau.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT09', N'C08', N'Bốn Mươi Năm Nói Láo', N'Sách là một tuyển tập những bài viết hài hước và sắc bén của Trịnh Công Sơn, một nhà thơ, nhạc sĩ và họa sĩ nổi tiếng của Việt Nam. Sách bao gồm những chuyện vui, những bình luận châm biếm và những suy ngẫm sâu sắc về cuộc sống, văn hóa, chính trị và tình yêu của người Việt Nam trong bốn mươi năm qua.', '~/Content/Images/BonMuoiNamNoiLao.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT10', N'C09', N'Thấy Hoa Vàng Trên Cỏ Xanh', N'Sách kể về tuổi thơ của hai anh em Thành và Tú ở một làng quê miền Tây Nam Bộ, nơi có những cánh đồng lúa bát ngát, những con sông uốn lượn và những bông hoa vàng rực rỡ. Sách là một tác phẩm đẹp và trong sáng, mang đến cho người đọc những cảm xúc ngọt ngào và nhớ nhung về một thời ấu thơ đơn giản và hạnh phúc.', '~/Content/Images/ToiThayHoaVangTrenCoXanh.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT11', N'C10', N'Bến Không Chồng', N'Sách tái hiện lại cuộc sống của những người phụ nữ miền Nam trong thời kỳ kháng chiến chống Pháp, qua góc nhìn của chị Hà, một người phụ nữ trẻ, xinh đẹp, thông minh và quyết tâm. Sách là một tác phẩm độc đáo, mang âm hưởng của văn học hiện thực và nữ quyền.', '~/Content/Images/BenKhongChong.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT12', N'C07', N'Đất Rừng Phương Nam', N'Sách kể về cuộc chiến tranh Đông Dương lần thứ hai giữa Việt Nam và Pháp, qua góc nhìn của một người lính Việt Nam, Đoàn Giỏi, người yêu nước và anh dũng. Sách là một tác phẩm kinh điển, mang đến cho người đọc những cảnh tượng chiến tranh đẫm máu, những mối tình đẹp và những tình huynh đệ cao cả.', '~/Content/Images/DatRungPhuongNam.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT13', N'C11', N'Bồ câu không đưa thư', N'Sách kể về cuộc tình đẹp nhưng đầy bi kịch của cô gái Pháp Lan và chàng trai Việt Nam Hùng trong bối cảnh chiến tranh Việt Nam. Sách là một tác phẩm cảm động, mang đến cho người đọc những cảnh tượng chiến tranh khốc liệt, những mất mát đau lòng và những hy sinh cao cả.', '~/Content/Images/BoCauKhongDuaThu.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT14', N'C12', N'Thời Xa Vắng', N' Sách tái hiện lại cuộc sống của những người Việt Nam trong thời kỳ đầu thế kỷ XX, khi đất nước đang chịu sự đô hộ của thực dân Pháp. Sách là một tác phẩm sâu sắc, mang đến cho người đọc những góc nhìn đa chiều về những vấn đề về lịch sử, văn hóa, tôn giáo, tình yêu và gia đình của người Việt Nam đương thời.', '~/Content/Images/ThoiXaVang.png')
INSERT [dbo].[BookTitle] ([Id], [CategoryBookID], [Name], [Summary], [UrlImage]) VALUES (N'BT15', N'C13', N'Cánh Đồng Bất Tận', N' Sách kể về cuộc sống của những người dân miền Tây Nam Bộ, nơi có những cánh đồng lúa bạt ngàn, những con sông mênh mông và những bông sen tinh khiết. Sách là một tác phẩm đẹp và trong sáng, mang đến cho người đọc những cảm xúc ngọt ngào và nhẹ nhàng về một miền quê yên bình và hạnh phúc.', '~/Content/Images/CanhDongBatTan.png')
	
-- -------------------------------
-- Table 12: structure for BookISBN (sách gốc)
-- -------------------------------
create table BookISBN (
	[ISBN] nvarchar(10) not null primary key,
	[BookTitleID] nvarchar(10) not null,
	[AuthorID] nvarchar(5) not null, -- 1 tác giả
	[LanguageID] nvarchar(5) not null, -- ngôn ngữ gốc

	[Status] bit not null default(1),

	foreign key ([LanguageID]) references [Language]([Id]),
	foreign key ([BookTitleID]) references [BookTitle]([Id]),
	foreign key ([AuthorID]) references Author([Id]),
)

INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN01', N'BT01', N'A03', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN02', N'BT01', N'A01', N'L009', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN03', N'BT01', N'A02', N'L003', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN04', N'BT02', N'A02', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN05', N'BT02', N'A05', N'L003', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN06', N'BT06', N'A06', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN07', N'BT07', N'A07', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN08', N'BT08', N'A08', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN09', N'BT09', N'A09', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN10', N'BT10', N'A10', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN11', N'BT11', N'A11', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN12', N'BT12', N'A12', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN13', N'BT13', N'A13', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN14', N'BT14', N'A11', N'L001', 1)
INSERT [dbo].[BookISBN] ([ISBN], [BookTitleID], [AuthorID], [LanguageID], [Status]) VALUES (N'ISBN15', N'BT15', N'A14', N'L001', 1)
	

-- -------------------------------
-- Table 12A: structure for Book
-- -------------------------------
create table Book (
	[Id] int IDENTITY(1,1) not null primary key,
	[ISBN] nvarchar(10) not null, -- mã sách gốc
	[PublisherID] nvarchar(5) not null, -- mã nhà xuất bản
	[TranslatorID] nvarchar(5) not null, -- mã người dịch	
	[LanguageID] nvarchar(5) not null, -- ngôn ngữ khác
	[PublishDate] DateTime not null,
	[Price] decimal(12,3) not null,

	[Status] bit not null default(1),
	
	foreign key ([ISBN]) references [BookISBN]([ISBN]),
	foreign key ([LanguageID]) references [Language]([Id]),
	foreign key ([PublisherID]) references Publisher([Id]),
	foreign key ([TranslatorID]) references Translator([Id]),
)

INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN01', N'P01', N'T01', N'L001', CAST(0x0000AFD800C5C100 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 0)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN01', N'P01', N'T01', N'L001', CAST(0x0000AFF600E6B680 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN01', N'P02', N'T02', N'L001', CAST(0x0000B01500FF6EA0 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P02', N'T02', N'L009', CAST(0x0000B03300DBBA00 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 0)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P02', N'T02', N'L009', CAST(0x0000B05200C88020 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P02', N'T02', N'L009', CAST(0x0000B07100F9F060 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P03', N'T03', N'L009', CAST(0x0000B08F005AA320 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P03', N'T03', N'L009', CAST(0x0000AF9C00685EC0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P03', N'T04', N'L009', CAST(0x0000AFC1006B1DE0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN02', N'P03', N'T04', N'L009', CAST(0x0000AFA000EA8EE0 AS DateTime), CAST(98.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P03', N'T04', N'L003', CAST(0x0000AFD800C5C100 AS DateTime), CAST(80.000 AS Decimal(12, 3)), 0)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN03', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC1E53 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN04', N'P03', N'T04', N'L001', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN04', N'P03', N'T04', N'L001', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN04', N'P03', N'T04', N'L001', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN04', N'P03', N'T04', N'L001', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN04', N'P03', N'T04', N'L001', CAST(0x0000B0E400AC541F AS DateTime), CAST(80.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN05', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN05', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN05', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN05', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN05', N'P01', N'T05', N'L001', CAST(0x0000B0E400AC6E3D AS DateTime), CAST(85.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN06', N'P02', N'T03', N'L002', CAST(0x0000B0E400AC8592 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN07', N'P01', N'T04', N'L004', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN07', N'P01', N'T04', N'L004', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN07', N'P01', N'T04', N'L004', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN07', N'P01', N'T04', N'L004', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN07', N'P01', N'T04', N'L004', CAST(0x0000B0E400ACCA29 AS DateTime), CAST(150.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN08', N'P01', N'T04', N'L051', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN08', N'P01', N'T04', N'L051', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN08', N'P01', N'T04', N'L051', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN08', N'P01', N'T04', N'L051', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN08', N'P01', N'T04', N'L051', CAST(0x0000B0E400ACF12A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN09', N'P03', N'T05', N'L051', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN09', N'P03', N'T05', N'L051', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN09', N'P03', N'T05', N'L051', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN09', N'P03', N'T05', N'L051', CAST(0x0000B0E400AD128D AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN10', N'P02', N'T05', N'L052', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN10', N'P02', N'T05', N'L052', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN10', N'P02', N'T05', N'L052', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN10', N'P02', N'T05', N'L052', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN10', N'P02', N'T05', N'L052', CAST(0x0000B0E400AD33F3 AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN11', N'P03', N'T01', N'L005', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN11', N'P03', N'T01', N'L005', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN11', N'P03', N'T01', N'L005', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN11', N'P03', N'T01', N'L005', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN11', N'P03', N'T01', N'L005', CAST(0x0000B0E400AD4E8A AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN12', N'P02', N'T04', N'L053', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN12', N'P02', N'T04', N'L053', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN12', N'P02', N'T04', N'L053', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN12', N'P02', N'T04', N'L053', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN12', N'P02', N'T04', N'L053', CAST(0x0000B0E400AD7B55 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN13', N'P01', N'T03', N'L054', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN13', N'P01', N'T03', N'L054', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN13', N'P01', N'T03', N'L054', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN13', N'P01', N'T03', N'L054', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN13', N'P01', N'T03', N'L054', CAST(0x0000B0E400AD93BE AS DateTime), CAST(120.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN14', N'P02', N'T05', N'L055', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN14', N'P02', N'T05', N'L055', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN14', N'P02', N'T05', N'L055', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN14', N'P02', N'T05', N'L055', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN14', N'P02', N'T05', N'L055', CAST(0x0000B0E400ADB48B AS DateTime), CAST(90.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN15', N'P01', N'T04', N'L056', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN15', N'P01', N'T04', N'L056', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN15', N'P01', N'T04', N'L056', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN15', N'P01', N'T04', N'L056', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)
INSERT [dbo].[Book] ([ISBN], [PublisherID], [TranslatorID], [LanguageID], [PublishDate], [Price], [Status]) VALUES (N'ISBN15', N'P01', N'T04', N'L056', CAST(0x0000B0E400ADD028 AS DateTime), CAST(100.000 AS Decimal(12, 3)), 1)


-- -------------------------------
-- Table 14: structure for BorrowBook
-- -------------------------------
CREATE TABLE [LoanSlip]
(
	[Id] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[AccountID] INT NULL,
	[ReaderID] NVARCHAR(5) NULL,

	FOREIGN KEY ([AccountID]) REFERENCES [Account](Id),
	FOREIGN KEY ([ReaderID]) REFERENCES [Reader](Id),
)

-- -------------------------------
-- Table 15: structure for LoanDetail
-- -------------------------------
CREATE TABLE LoanSlipDetail (
	[Id] int IDENTITY(1,1) not null primary key, 
    [LoanSlipID] NVARCHAR(5) NOT NULL, -- ID của phiếu mượn (khóa ngoại)
    [BookID] int NOT NULL, -- ID của ấn bản sách (khóa ngoại)
	[BorrowDate] DATETIME NULL,
	[DueDate] DATETIME NULL,
	[DepositPrice] DECIMAL(10, 2) NOT NULL,
	[RentalPrice] DECIMAL(10, 2) NOT NULL,
    
	foreign key ([LoanSlipID]) references [LoanSlip]([Id]),
	foreign key ([BookID]) references [Book]([Id]),
);


-- -------------------------------
-- Table 16: structure for PenaltyReason
-- -------------------------------
create table PenaltyReason (
	[Id] NVARCHAR(10) not null primary key,
	[Name] nvarchar(100) not null,
	[Price] decimal(12,3) not null,
)

insert into [PenaltyReason] ([Id], [Name], [Price])
values
	('PR1', N'Không có', 0.000),
	('PR2', N'Quá hạn trả sách', 5.000)

-- -------------------------------
-- Table 17: structure for LoanHistory
-- -------------------------------
create table HistoryLoanSlip (
	[Id] NVARCHAR(10) not null primary key,
	[LoanSlipID] NVARCHAR(5) NOT NULL,
	[PenaltyReasonID] NVARCHAR(10) not null,

	[LoanPaid] decimal(12,3) not null,
	[FineMoney] decimal(12,3) not null, -- tiền phạt tổng
	[TotalPaid] decimal(12,3) not null,

	[CreateAt] DateTime not null,		-- Ngày độc giả trả sách
	
	foreign key ([LoanSlipID]) references [LoanSlip]([Id]),
	foreign key ([PenaltyReasonID]) references [PenaltyReason]([Id])
)

-- -------------------------------
-- Table 18: structure for Statistical
-- -------------------------------
CREATE TABLE [Statistical]
(
	[nBorrowedBooks] INT NULL,
	[nUnborrowedBooks] INT NULL,
	[nISBNBooks] INT NULL,
	[nLoanSlips] INT NULL,
	[nHistoryLoanSlips] INT NULL,
	[nReaders] INT NULL,
)