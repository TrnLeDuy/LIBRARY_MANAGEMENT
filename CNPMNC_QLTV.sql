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
	[ten_cuonsach] nvarchar(100),
	[tacgia] nvarchar(50),
	[namxuatban] smalldatetime,
	[nhaxuatban] nvarchar(100),
	[ma_loaisach] [char] (10) NOT NULL,
	[TinhTrang] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Mota] nvarchar(500),
	Hinhmota nvarchar(100)
) ON [PRIMARY]
GO

CREATE TABLE [DauSach] (
	[isbn] [int] NOT NULL ,
	[ten_dausach] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS ,
	[soluong] int null,
	[trangthai] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS not null
) ON [PRIMARY]
GO

CREATE TABLE [MuonTra] (
	[ma_phieumuontra] [int] IDENTITY (1,1) NOT NULL ,
	[ma_sinhvien] nvarchar(10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[MaNV] int check (MaNV like '[0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngayGio_muon] [smalldatetime] NOT NULL ,
	[ngay_hethan] [smalldatetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ChiTietMuonTra] (
	[isbn] [int] NOT NULL,
	[ma_phieumuontra] [int] NOT NULL ,
	[ma_cuonsach]  [char] (10) NOT NULL ,
	[ma_sinhvien] nvarchar(10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngayGio_tra] [smalldatetime] NULL ,
	[soluong] int NULL,
	[ghichu] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO


CREATE TABLE [PhieuPhat] (
	[ma_phieumuontra] [int] NOT NULL ,
	MaNV [int] check (MaNV like '[0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ma_sinhvien] [nvarchar] (10) check (ma_sinhvien like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	[ngay_lapphieu] smalldatetime,
	[nguyen_nhan] nvarchar(100),
	[Songay_quahan] int null,
	Tongtien money null,
	Sotienthu money null,
	Conlai money null
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
	Username [char] (20) NOT NULL UNIQUE,
	Password [char] (20),
	LoaiTK [nvarchar] (10)
) ON [PRIMARY]
GO

--------------------------------------------------------------


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

ALTER TABLE [MuonTra] WITH NOCHECK ADD 
	CONSTRAINT [PK_muontra] PRIMARY KEY  CLUSTERED 
	(
		[ma_phieumuontra]
	)  ON [PRIMARY]
GO

ALTER TABLE [SinhVien] WITH NOCHECK ADD 
	CONSTRAINT [PK_sinhvien] PRIMARY KEY  CLUSTERED 
	(
		[ma_sinhvien]
	)  ON [PRIMARY]
GO

ALTER TABLE [ChiTietMuonTra] WITH NOCHECK ADD 
	CONSTRAINT [PK_Chitietmuontra] PRIMARY KEY  CLUSTERED 
	(
		[isbn],
		[ma_phieumuontra],
		[ma_cuonsach]
	)  ON [PRIMARY] 
GO


ALTER TABLE [PhieuPhat] WITH NOCHECK ADD
	CONSTRAINT [PK_Phieuphat] PRIMARY KEY	CLUSTERED
	(
		[ma_phieumuontra]
	)	ON [PRIMARY]
GO


------------------------------------------------------------
GO
ALTER TABLE [dbo].[ChiTietMuonTra]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietMuonTra_CuonSach] FOREIGN KEY([isbn], [ma_cuonsach])
REFERENCES [dbo].[CuonSach] ([isbn], [ma_cuonsach])
GO
ALTER TABLE [dbo].[ChiTietMuonTra] CHECK CONSTRAINT [FK_ChiTietMuonTra_CuonSach]
GO
ALTER TABLE [dbo].[ChiTietMuonTra]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietMuonTra_MuonTra] FOREIGN KEY([ma_phieumuontra])
REFERENCES [dbo].[MuonTra] ([ma_phieumuontra])
GO
ALTER TABLE [dbo].[ChiTietMuonTra] CHECK CONSTRAINT [FK_ChiTietMuonTra_MuonTra]
GO
ALTER TABLE [dbo].[CuonSach]  WITH CHECK ADD  CONSTRAINT [FK_CuonSach_DauSach] FOREIGN KEY([isbn])
REFERENCES [dbo].[DauSach] ([isbn])
GO
ALTER TABLE [dbo].[CuonSach] CHECK CONSTRAINT [FK_CuonSach_DauSach]
GO
ALTER TABLE [dbo].[CuonSach]  WITH CHECK ADD  CONSTRAINT [FK_CuonSach_LoaiSach] FOREIGN KEY([ma_loaisach])
REFERENCES [dbo].[LoaiSach] ([ma_loaisach])
GO
ALTER TABLE [dbo].[CuonSach] CHECK CONSTRAINT [FK_CuonSach_LoaiSach]
GO
ALTER TABLE [dbo].[MuonTra]  WITH CHECK ADD  CONSTRAINT [FK_MuonTra_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[MuonTra] CHECK CONSTRAINT [FK_MuonTra_NhanVien]
GO
ALTER TABLE [dbo].[MuonTra]  WITH CHECK ADD  CONSTRAINT [FK_MuonTra_TheThuVien1] FOREIGN KEY([ma_sinhvien])
REFERENCES [dbo].[TheThuVien] ([ma_sinhvien])
GO
ALTER TABLE [dbo].[MuonTra] CHECK CONSTRAINT [FK_MuonTra_TheThuVien1]
GO
ALTER TABLE [dbo].[PhieuPhat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuPhat_MuonTra] FOREIGN KEY([ma_phieumuontra])
REFERENCES [dbo].[MuonTra] ([ma_phieumuontra])
GO
ALTER TABLE [dbo].[PhieuPhat] CHECK CONSTRAINT [FK_PhieuPhat_MuonTra]
GO
ALTER TABLE [dbo].[PhieuPhat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuPhat_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhieuPhat] CHECK CONSTRAINT [FK_PhieuPhat_NhanVien]
GO
ALTER TABLE [dbo].[PhieuPhat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuPhat_TheThuVien] FOREIGN KEY([ma_sinhvien])
REFERENCES [dbo].[TheThuVien] ([ma_sinhvien])
GO
ALTER TABLE [dbo].[PhieuPhat] CHECK CONSTRAINT [FK_PhieuPhat_TheThuVien]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [FK_TaiKhoan_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [FK_TaiKhoan_NhanVien]
GO
ALTER TABLE [dbo].[TheThuVien]  WITH CHECK ADD  CONSTRAINT [FK_TheThuVien_SinhVien] FOREIGN KEY([ma_sinhvien])
REFERENCES [dbo].[SinhVien] ([ma_sinhvien])
GO
ALTER TABLE [dbo].[TheThuVien] CHECK CONSTRAINT [FK_TheThuVien_SinhVien]
GO
ALTER TABLE [dbo].[ChiTietMuonTra]  WITH CHECK ADD CHECK  (([ma_sinhvien] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[MuonTra]  WITH CHECK ADD CHECK  (([ma_sinhvien] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[MuonTra]  WITH CHECK ADD CHECK  (([MaNV] like '[0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD CHECK  (([MaNV] like '[0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[PhieuPhat]  WITH CHECK ADD CHECK  (([ma_sinhvien] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[PhieuPhat]  WITH CHECK ADD CHECK  (([MaNV] like '[0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD CHECK  (([ma_sinhvien] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD CHECK  (([MaNV] like '[0-9][0-9][0-9][0-9][0-9]'))
GO
GO
ALTER TABLE [dbo].[TheThuVien]  WITH CHECK ADD CHECK  (([ma_sinhvien] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
