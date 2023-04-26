use master 
if exists(select * from sysdatabases where name='CNPM_QLTV') 
drop database CNPM_QLTV
GO
create database CNPM_QLTV
GO
use CNPM_QLTV
GO


CREATE TABLE [TheThuVien] (
	[ma_sinhvien] [nvarchar] (10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL ,
	[Hoten] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgaySinh] [smalldatetime],
	[Tinhtrangthe] [nvarchar] (10)
) ON [PRIMARY] 
GO

CREATE TABLE [LoaiSach] (
	[ma_loaisach] [char] (10) NOT NULL,
	[ten_loaisach] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [SinhVien] (
	[ma_sinhvien] [nvarchar] (10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[diachi] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[dienthoai] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[han_sd] [smalldatetime] NOT NULL 	
) ON [PRIMARY]
GO


CREATE TABLE [CuonSach] (
	[isbn] [int] NOT NULL ,
	[ma_cuonsach]  [char] (10) NOT NULL ,
	[TinhTrang] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [DangKy] (
	[isbn] [int] NOT NULL ,
	[ma_sinhvien] [nvarchar] (10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngaygio_dk] [smalldatetime] ,
	[ghichu] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS 
) ON [PRIMARY]
GO

CREATE TABLE [DauSach] (
	[isbn] [int] NOT NULL ,
	[ma_tuasach] [char] (10) NOT NULL ,
	[ngonngu] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS ,
	[bia] [char] (15) COLLATE SQL_Latin1_General_CP1_CI_AS  ,
	[trangthai] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS not null
) ON [PRIMARY]
GO

CREATE TABLE [Muon] (
	[isbn] [int] NOT NULL ,
	[ma_cuonsach]  [char] (10) NOT NULL ,
	[ma_sinhvien] nvarchar(10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngayGio_muon] [smalldatetime] NOT NULL ,
	[ngay_hethan] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [QuaTrinhMuon] (
	[isbn] [int] NOT NULL ,
	[ma_cuonsach]  [char] (10) NOT NULL ,
	[ngayGio_muon] [smalldatetime] NOT NULL ,
	[ma_sinhvien] nvarchar(10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngay_hethan] [smalldatetime] NULL ,
	[ngayGio_tra] [smalldatetime] NULL ,
	[tien_muon] [money] NULL ,
	[tien_datra] [money] NULL ,
	[tien_datcoc] [money] NULL ,
	[ghichu] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	MaNV int check (MaNV like '[0-9][0-9][0-9][0-9][0-9]') NOT NULL,
) ON [PRIMARY]
GO

CREATE TABLE [TuaSach] (
	[ma_tuasach] [char] (10) NOT NULL ,
	[ma_loaisach] [char] (10) NOT NULL,
	[TuaSach] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tacgia] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tomtat] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[namxuatban] [smalldatetime]
) ON [PRIMARY]
GO

CREATE TABLE NhanVien(
	MaNV [int] check (MaNV like '[0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	Hoten [nvarchar] (50),
	NgaySinh [smalldatetime],
	Gioitinh [nvarchar] (10),
	Email [nvarchar] (50) UNIQUE,
	SDT [char] (11),
	Diachi [nvarchar] (100)
) ON [PRIMARY]
GO

CREATE TABLE TaiKhoan(
	MaNV [int] check (MaNV like '[0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	Username [char] (20) UNIQUE,
	Password [char] (20),
	LoaiTK [nvarchar] (10)
) ON [PRIMARY]
GO

------------------------------------------------------------
--create PK
ALTER TABLE [NhanVien] WITH NOCHECK ADD
	CONSTRAINT [Pk_nhanvien] PRIMARY KEY  CLUSTERED
	(
		MaNV
	)	ON [PRIMARY]
GO

ALTER TABLE [LoaiSach] WITH NOCHECK ADD
	CONSTRAINT [Pk_loaisach] PRIMARY KEY  CLUSTERED
	 (
		ma_loaisach
	 ) ON [PRIMARY]
GO

ALTER TABLE [TaiKhoan] WITH NOCHECK ADD
	CONSTRAINT [Pk_taikhoan] PRIMARY KEY  CLUSTERED
	(
		MaNV
	)	ON [PRIMARY]
GO

ALTER TABLE [CuonSach] WITH NOCHECK ADD 
	CONSTRAINT [PK_cuonsach] PRIMARY KEY  CLUSTERED 
	(
		[isbn],
		[ma_cuonsach]
	)  ON [PRIMARY] 
GO

ALTER TABLE [DangKy] WITH NOCHECK ADD 
	CONSTRAINT [PK_dangky] PRIMARY KEY  CLUSTERED 
	(
		[isbn],
		[ma_sinhvien]
	)  ON [PRIMARY] 
GO

ALTER TABLE [DauSach] WITH NOCHECK ADD 
	CONSTRAINT [PK_dausach] PRIMARY KEY  CLUSTERED 
	(
		[isbn]
	)  ON [PRIMARY] 
GO

ALTER TABLE [TheThuVien] WITH NOCHECK ADD 
	CONSTRAINT [PK_TheThuVien] PRIMARY KEY  CLUSTERED 
	(
		[ma_sinhvien]
	)  ON [PRIMARY] 
GO

ALTER TABLE [Muon] WITH NOCHECK ADD 
	CONSTRAINT [PK_muon] PRIMARY KEY  CLUSTERED 
	(
		[isbn],
		[ma_cuonsach]
	)  ON [PRIMARY]
GO

ALTER TABLE [SinhVien] WITH NOCHECK ADD 
	CONSTRAINT [PK_sinhvien] PRIMARY KEY  CLUSTERED 
	(
		[ma_sinhvien]
	)  ON [PRIMARY]
GO

ALTER TABLE [QuaTrinhMuon] WITH NOCHECK ADD 
	CONSTRAINT [PK_QuaTrinhMuon] PRIMARY KEY  CLUSTERED 
	(
		[isbn],
		[ma_cuonsach],
		[ngayGio_muon]
	)  ON [PRIMARY] 
GO


ALTER TABLE [TuaSach] WITH NOCHECK ADD 
	CONSTRAINT [PK_tuasach] PRIMARY KEY  CLUSTERED 
	(
		[ma_tuasach]
	)  ON [PRIMARY] 
GO

------------------------------------------------------------
ALTER TABLE [TaiKhoan] ADD
	CONSTRAINT [FK_taikhoan_nhanvien] FOREIGN KEY
	(
		[MaNV]
	) REFERENCES [NhanVien](
		[MaNV]
		)
GO

--create FK
ALTER TABLE [CuonSach] ADD 
	CONSTRAINT [FK_cuonsach_dausach] FOREIGN KEY 
	(
		[isbn]
	) REFERENCES [DauSach] (
		[isbn]
	)
GO

ALTER TABLE [DangKy] ADD 
	CONSTRAINT [FK_dangky_dausach] FOREIGN KEY 
	(
		[isbn]
	) REFERENCES [DauSach] (
		[isbn]
	),
	CONSTRAINT [FK_dangky_sinhvien] FOREIGN KEY 
	(
		[ma_sinhvien]
	) REFERENCES [TheThuVien] (
		[ma_sinhvien]
	)
GO

alter table Muon add
	constraint fkMuon_SinhVien
	foreign key(ma_sinhvien)
	references TheThuVien(ma_sinhvien)
go

alter table QuaTrinhMuon add
	constraint fkQuaTrinhMuon_SinhVien
	foreign key(ma_sinhvien)
	references TheThuVien(ma_sinhvien)
go


ALTER TABLE [DauSach] ADD 
	CONSTRAINT [FK_dausach_tuasach] FOREIGN KEY 
	(
		[ma_tuasach]
	) REFERENCES [TuaSach] (
		[ma_tuasach]
	)
GO

ALTER TABLE [Muon] ADD 
	CONSTRAINT [FK_muon_cuonsach] FOREIGN KEY 
	(
		[isbn],
		[ma_cuonsach]
	) REFERENCES [CuonSach] (
		[isbn],
		[ma_cuonsach]
	)
GO

ALTER TABLE [SinhVien] ADD 
	CONSTRAINT [FK_sinhvien_TheThuVien] FOREIGN KEY 
	(
		[ma_sinhvien]
	) REFERENCES [TheThuVien] (
		[ma_sinhvien]
	)
GO

ALTER TABLE [QuaTrinhMuon] ADD 
	CONSTRAINT [FK_QuaTrinhMuon_cuonsach] FOREIGN KEY 
	(
		[isbn],
		[ma_cuonsach]
	) REFERENCES [CuonSach] (
		[isbn],
		[ma_cuonsach]
	)
GO


ALTER TABLE [QuaTrinhMuon] ADD 
	CONSTRAINT [FK_QuaTrinhMuon_nhanvien] FOREIGN KEY 
	(
		[MaNV]
	) REFERENCES [NhanVien] (
		[MaNV]
	)
GO

ALTER TABLE [TuaSach] ADD
	CONSTRAINT [FK_Tuasach_loaisach] FOREIGN KEY
	(
		[ma_loaisach]
	) REFERENCES [LoaiSach](
		[ma_loaisach]
)
GO
