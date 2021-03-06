USE [master]
GO
/****** Object:  Database [InventoryRev]    Script Date: 03/31/2017 23:18:08 ******/
CREATE DATABASE [InventoryRev] ON  PRIMARY 
( NAME = N'InventoryRev', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSVRSP2\MSSQL\DATA\InventoryRev.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'InventoryRev_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSVRSP2\MSSQL\DATA\InventoryRev_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [InventoryRev] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryRev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryRev] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [InventoryRev] SET ANSI_NULLS OFF
GO
ALTER DATABASE [InventoryRev] SET ANSI_PADDING OFF
GO
ALTER DATABASE [InventoryRev] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [InventoryRev] SET ARITHABORT OFF
GO
ALTER DATABASE [InventoryRev] SET AUTO_CLOSE ON
GO
ALTER DATABASE [InventoryRev] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [InventoryRev] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [InventoryRev] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [InventoryRev] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [InventoryRev] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [InventoryRev] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [InventoryRev] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [InventoryRev] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [InventoryRev] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [InventoryRev] SET  DISABLE_BROKER
GO
ALTER DATABASE [InventoryRev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [InventoryRev] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [InventoryRev] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [InventoryRev] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [InventoryRev] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [InventoryRev] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [InventoryRev] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [InventoryRev] SET  READ_WRITE
GO
ALTER DATABASE [InventoryRev] SET RECOVERY SIMPLE
GO
ALTER DATABASE [InventoryRev] SET  MULTI_USER
GO
ALTER DATABASE [InventoryRev] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [InventoryRev] SET DB_CHAINING OFF
GO
USE [InventoryRev]
GO
/****** Object:  Table [dbo].[t_tugas_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_tugas_detail](
	[id_tugas_detail] [bigint] IDENTITY(1,1) NOT NULL,
	[id_tugas] [varchar](50) NOT NULL,
	[id_inventaris] [bigint] NULL,
	[qty] [int] NULL,
	[catatan_teknisi] [varchar](255) NULL,
	[catatan_kabag] [varchar](255) NULL,
	[tanggal_selesai] [datetime] NULL,
	[status_tugas] [int] NULL,
 CONSTRAINT [PK_t_tugas_detail] PRIMARY KEY CLUSTERED 
(
	[id_tugas_detail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_tugas]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_tugas](
	[id_tugas] [varchar](50) NOT NULL,
	[judul_tugas] [varchar](255) NULL,
	[created_date] [datetime] NULL,
	[id_user] [int] NULL,
	[id_teknisi] [int] NULL,
	[status_tugas] [int] NULL,
	[catatan_kabag] [varchar](1000) NULL,
	[catatan_teknisi] [varchar](1000) NULL,
	[tanggal_selesai] [datetime] NULL,
 CONSTRAINT [PK_t_penugasan] PRIMARY KEY CLUSTERED 
(
	[id_tugas] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 draft, 1 sent to petugas, 2. on progress, 3. half complete, 4. complete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_tugas', @level2type=N'COLUMN',@level2name=N'status_tugas'
GO
/****** Object:  Table [dbo].[t_service_keluar]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_service_keluar](
	[id_service_keluar] [varchar](255) NOT NULL,
	[id_pengajuan_service] [varchar](255) NULL,
	[tanggal_service] [datetime] NULL,
	[id_user] [int] NULL,
	[keterangan] [varchar](500) NULL,
	[judul] [varchar](50) NULL,
	[id_service] [varchar](50) NULL,
	[status] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_penugasan_inventaris_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_penugasan_inventaris_detail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_penugasan] [varchar](100) NOT NULL,
	[judul] [varchar](100) NOT NULL,
	[catatan] [text] NULL,
	[catatan_teknisi] [text] NULL,
	[qty] [int] NULL,
	[status] [tinyint] NULL,
	[created_date] [datetime] NULL,
	[creator] [int] NULL,
	[last_updated_date] [datetime] NULL,
	[updater] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 belum, 1 on progress, 2 selesai, 3 tidak bisa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_penugasan_inventaris_detail', @level2type=N'COLUMN',@level2name=N'status'
GO
/****** Object:  Table [dbo].[t_penugasan_inventaris]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_penugasan_inventaris](
	[id_penugasan] [varchar](100) NOT NULL,
	[qty] [int] NULL,
	[catatan] [varchar](1000) NULL,
	[catatan_teknisi] [varchar](1000) NULL,
	[status] [tinyint] NULL,
	[created_date] [datetime] NULL,
	[creator] [int] NULL,
	[last_updated_date] [datetime] NULL,
	[updater] [int] NULL,
	[tanggal_penugasan] [date] NULL,
	[id_user] [int] NULL,
	[judul] [varchar](100) NULL,
	[is_start] [tinyint] NULL,
 CONSTRAINT [PK_t_penugasan_inventaris_1] PRIMARY KEY CLUSTERED 
(
	[id_penugasan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 belum, 1 on progress, 2 selesai, 3 tidak bisa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_penugasan_inventaris', @level2type=N'COLUMN',@level2name=N'status'
GO
/****** Object:  Table [dbo].[t_pengajuan_service_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pengajuan_service_detail](
	[id_pengajuan_service_detail] [bigint] IDENTITY(1,1) NOT NULL,
	[id_pengajuan_service] [varchar](255) NULL,
	[id_inventaris] [int] NOT NULL,
	[qty] [int] NOT NULL,
	[catatan_kabag] [varchar](255) NULL,
	[biaya_service] [int] NULL,
	[id_user_kabag] [int] NULL,
	[status_pengajuan] [int] NULL,
	[qty_diterima] [int] NULL,
	[tanggal_diterima] [datetime] NULL,
	[created_date] [datetime] NOT NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
	[biaya_service_total] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pengajuan_service]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pengajuan_service](
	[id_pengajuan_service] [varchar](255) NOT NULL,
	[no_pengajuan_service] [varchar](255) NULL,
	[judul_pengajuan_service] [varchar](200) NULL,
	[catatan_kabag] [varchar](500) NULL,
	[catatan_pimpinan] [varchar](255) NULL,
	[tanggal] [datetime] NULL,
	[id_user] [int] NULL,
	[status_konfirmasi] [int] NULL,
	[status_pengajuan] [int] NULL,
	[status_prioritas] [int] NULL,
	[approval_date] [datetime] NULL,
	[created_date] [date] NULL,
	[id_user_pimpinan] [int] NULL,
	[id_lokasi_service] [int] NULL,
	[file_save] [varchar](250) NULL,
 CONSTRAINT [PK_t_service_keluar] PRIMARY KEY CLUSTERED 
(
	[id_pengajuan_service] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pengajuan_ref_type]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pengajuan_ref_type](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_ref] [int] NULL,
	[no_ref] [varchar](50) NOT NULL,
	[judul_ref] [varchar](50) NULL,
	[id_penugasan] [varchar](100) NOT NULL,
	[jenis] [varchar](15) NULL,
	[summary] [varchar](1000) NULL,
	[status_pekerjaan] [int] NULL,
	[updated_date] [datetime] NULL,
 CONSTRAINT [PK_t_pengajuan_ref_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pengajuan_inventaris_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pengajuan_inventaris_detail](
	[id_pengajuan_detail] [bigint] IDENTITY(1,1) NOT NULL,
	[id_pengajuan_inventaris] [varchar](255) NOT NULL,
	[id_inventaris] [int] NULL,
	[id_jenis_inventaris] [int] NULL,
	[qty] [int] NOT NULL,
	[catatan_kabag] [varchar](255) NULL,
	[status_pengajuan] [int] NULL,
	[harga_satuan] [int] NULL,
	[harga_total] [int] NULL,
	[qty_diterima] [int] NULL,
	[tanggal_diterima] [datetime] NULL,
	[created_date] [datetime] NOT NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_t_pengajuan_inventaris_detail] PRIMARY KEY CLUSTERED 
(
	[id_pengajuan_detail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pengajuan_inventaris]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pengajuan_inventaris](
	[id_pengajuan] [varchar](40) NOT NULL,
	[no_pengajuan] [varchar](50) NULL,
	[judul_pengajuan] [varchar](250) NULL,
	[catatan_kabag] [varchar](255) NULL,
	[catatan_pimpinan] [varchar](255) NULL,
	[id_user] [int] NULL,
	[status_konfirmasi] [int] NULL,
	[status_pengajuan] [int] NULL,
	[status_prioritas] [int] NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
	[approval_date] [datetime] NULL,
	[harga_total] [bigint] NULL,
	[file_save] [varchar](1000) NULL,
	[id_user_pimpinan] [int] NULL,
	[is_sudah_beli] [int] NULL,
 CONSTRAINT [PK_t_pengajuan_inventaris] PRIMARY KEY CLUSTERED 
(
	[id_pengajuan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_penerimaan_inventaris_nopengajuan]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_penerimaan_inventaris_nopengajuan](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_penerimaan] [varchar](100) NOT NULL,
	[id_inventaris] [bigint] NOT NULL,
	[catatan] [varchar](255) NULL,
	[id_user_kabag] [int] NULL,
	[qty_diterima] [int] NULL,
	[tanggal_diterima] [datetime] NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_penerimaan_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_penerimaan_detail](
	[id_penerimaan_detail] [bigint] IDENTITY(1,1) NOT NULL,
	[id_penerimaan] [varchar](100) NOT NULL,
	[id_inventaris] [bigint] NOT NULL,
	[qty_diterima] [int] NULL,
	[keterangan] [varchar](255) NULL,
	[tanggal_diterima] [datetime] NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
	[id_pengajuan_detail] [bigint] NULL,
	[qty_diajukan] [int] NULL,
	[catatan_pengajuan] [varchar](200) NULL,
	[id_ref] [varchar](30) NULL,
 CONSTRAINT [PK_t_penerimaan_inventaris_detail] PRIMARY KEY CLUSTERED 
(
	[id_penerimaan_detail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_penerimaan]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_penerimaan](
	[id_penerimaan] [varchar](50) NOT NULL,
	[no_penerimaan] [varchar](50) NULL,
	[id_user] [int] NULL,
	[id_pembelian] [varchar](50) NULL,
	[id_service_keluar] [varchar](50) NULL,
	[id_pengajuan] [varchar](50) NULL,
	[judul_penerimaan] [varchar](200) NULL,
	[id_distributor] [bigint] NULL,
	[tanggal_penerimaan] [datetime] NULL,
	[status_penerimaan] [int] NULL,
	[keterangan] [varchar](255) NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
	[tipe_ref] [int] NULL,
	[no_service_keluar] [varchar](50) NULL,
 CONSTRAINT [PK_t_penerimaan_inventaris] PRIMARY KEY CLUSTERED 
(
	[id_penerimaan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pencatatan_inventaris_detail]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pencatatan_inventaris_detail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_pencatatan] [bigint] NULL,
	[id_inventaris] [int] NULL,
	[id_jenis_inventaris] [bigint] NULL,
	[id_ruangan] [int] NULL,
	[catatan] [varchar](255) NULL,
	[tanggal_pencatatan] [datetime] NULL,
	[petugas] [int] NULL,
	[qty] [int] NULL,
	[qty_on_progress] [int] NULL,
	[tipe] [tinyint] NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [datetime] NULL,
	[updater] [varchar](20) NULL,
	[status_perbaikan] [int] NULL,
	[status_simpan] [int] NULL,
 CONSTRAINT [PK_t_history_kerusakan_2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 Rusak
2 Hilang
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_pencatatan_inventaris_detail', @level2type=N'COLUMN',@level2name=N'tipe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Rusak - Draft,
1 Sedang Diperbaiki,
2 Berhasil Diperbaiki,
3 Gagal Diperbaiki' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_pencatatan_inventaris_detail', @level2type=N'COLUMN',@level2name=N'status_perbaikan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Draft 1. Disimpan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_pencatatan_inventaris_detail', @level2type=N'COLUMN',@level2name=N'status_simpan'
GO
/****** Object:  Table [dbo].[t_pencatatan_inventaris]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pencatatan_inventaris](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[no_pencatatan] [varchar](30) NULL,
	[tanggal_pencatatan] [datetime] NULL,
	[id_ruangan] [int] NULL,
	[id_pencatat] [int] NULL,
	[created_date] [datetime] NULL,
	[creator] [varchar](50) NULL,
	[last_updated_date] [datetime] NULL,
	[updater] [varchar](20) NULL,
	[status] [int] NULL,
	[catatan] [varchar](200) NULL,
 CONSTRAINT [PK_t_pencatatan_inventaris] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pencatatan_inv_detail_list]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pencatatan_inv_detail_list](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[no_inventaris] [varchar](50) NULL,
	[status] [int] NULL,
	[id_inv_ruangan] [bigint] NULL,
	[updated_date] [datetime] NULL,
	[created_date] [datetime] NULL,
	[updater] [varchar](50) NULL,
	[creator] [varbinary](50) NULL,
 CONSTRAINT [PK_t_pencatatan_inv_detail_list] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_t_pencatatan_inv_detail_list] UNIQUE NONCLUSTERED 
(
	[no_inventaris] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_pembelian]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_pembelian](
	[id_pembelian] [varchar](255) NOT NULL,
	[id_pengajuan_inventaris] [varchar](255) NULL,
	[no_pembelian] [varchar](50) NULL,
	[tanggal_pembelian] [datetime] NULL,
	[id_user] [int] NULL,
	[keterangan] [varchar](500) NULL,
	[harga_total] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_t_pembelian_1] PRIMARY KEY CLUSTERED 
(
	[id_pembelian] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_inventaris_ruangan]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_inventaris_ruangan](
	[id_inventaris_ruangan] [bigint] IDENTITY(1,1) NOT NULL,
	[id_ruangan] [bigint] NOT NULL,
	[id_inventaris] [int] NOT NULL,
	[keterangan] [varchar](255) NULL,
	[id_barang] [bigint] NULL,
	[id_pengadaan] [bigint] NULL,
	[qty_total] [int] NULL,
	[qty_rusak] [int] NULL,
	[qty_terpakai] [int] NULL,
	[qty_hilang] [int] NULL,
	[gambar] [varchar](255) NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_t_inventaris_ruangan] PRIMARY KEY CLUSTERED 
(
	[id_inventaris_ruangan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_inventaris_history]    Script Date: 03/31/2017 23:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_inventaris_history](
	[id_inventaris] [int] NULL,
	[created_date] [datetime] NULL,
	[username] [varchar](50) NULL,
	[id_user] [int] NULL,
	[qty_tambah_kurang] [int] NULL,
	[qty_sebelum] [int] NULL,
	[qty_sesudah] [int] NULL,
	[jenis] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[UPSERT_ADJUSTMENT_INVENTARIS]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maulana Wahid A - Tatat Nuraeni>
-- Create date: <03 Oct 15>
-- Description:	<Stored Procedure To Save/Update Stock>
-- =============================================
CREATE PROCEDURE [dbo].[UPSERT_ADJUSTMENT_INVENTARIS] 
	-- Add the parameters for 
	@id_inventaris bigint,
	@jenis tinyint,
	@qty int, 
	@id_adjustment bigint = NULL
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    if(@jenis = 1)
    Begin
    
	UPDATE [t_stock_inventaris] SET qty_tersedia = qty_tersedia + @qty WHERE id_inventaris = @id_inventaris
	IF @@ROWCOUNT=0
    INSERT INTO [t_stock_inventaris](id_inventaris, qty_tersedia) values(@id_inventaris,@qty)
    end
    
    else if (@jenis=2)
    begin
    
    UPDATE [t_stock_inventaris] SET qty_tersedia = qty_tersedia - @qty WHERE id_inventaris = @id_inventaris
	IF @@ROWCOUNT=0
    INSERT INTO [t_stock_inventaris](id_inventaris, qty_tersedia) values(@id_inventaris,@qty)
    
    end
    
	INSERT INTO [t_adjustment](id_inventaris, jenis_adjustment, qty_adjustment) Output Inserted.id values(@id_inventaris,@jenis, @qty)
    
    SET @id_adjustment = SCOPE_IDENTITY()
    -- Add to Stock History, 1 for added from penerimaan and 2 added from adjustment
    INSERT [t_history_stock](id_inventaris,type_history, add_or_substract, qty, id_referensi) values(@id_inventaris, 2,@jenis,@qty, @id_adjustment)
END
GO
/****** Object:  Table [dbo].[m_user]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_user](
	[id_user] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[nama_lengkap] [varchar](50) NULL,
	[no_telp] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[deskripsi] [varchar](40) NULL,
	[id_role] [int] NULL,
	[role] [varchar](12) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_ruangan]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_ruangan](
	[id_ruangan] [int] IDENTITY(1,1) NOT NULL,
	[nama_ruangan] [varchar](50) NOT NULL,
	[id_lantai] [int] NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_ruangan] PRIMARY KEY CLUSTERED 
(
	[id_ruangan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_merk]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_merk](
	[id_merk] [int] IDENTITY(1,1) NOT NULL,
	[nama_merk] [varchar](50) NOT NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_merk] PRIMARY KEY CLUSTERED 
(
	[id_merk] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_lokasi_service]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_lokasi_service](
	[id_lokasi_service] [int] IDENTITY(1,1) NOT NULL,
	[nama_lokasi_service] [varchar](50) NULL,
	[alamat] [varchar](1000) NULL,
	[no_telp] [varchar](45) NULL,
	[email] [varchar](45) NULL,
	[website] [varchar](45) NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_lokasi_service] PRIMARY KEY CLUSTERED 
(
	[id_lokasi_service] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_lantai]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_lantai](
	[id_lantai] [int] IDENTITY(1,1) NOT NULL,
	[lokasi_lantai] [varchar](20) NULL,
	[id_gedung] [int] NULL,
	[keterangan] [varchar](50) NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_lokasi] PRIMARY KEY CLUSTERED 
(
	[id_lantai] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_jenis_inventaris]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_jenis_inventaris](
	[id_jenis_inventaris] [int] IDENTITY(1,1) NOT NULL,
	[nama_jenis_inventaris] [varchar](20) NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_jenis_inventaris] PRIMARY KEY CLUSTERED 
(
	[id_jenis_inventaris] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_inventaris]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_inventaris](
	[id_inventaris] [bigint] IDENTITY(1,1) NOT NULL,
	[nama_inventaris] [varchar](30) NOT NULL,
	[harga] [int] NULL,
	[keterangan] [varchar](50) NULL,
	[id_jenis_inventaris] [int] NULL,
	[id_distributor] [int] NULL,
	[id_merk] [int] NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
	[qty] [int] NULL,
	[qty_belum_terpakai] [int] NULL,
	[qty_dalam_ruangan] [int] NULL,
	[gambar] [varchar](255) NULL,
 CONSTRAINT [PK_m_inventaris] PRIMARY KEY CLUSTERED 
(
	[id_inventaris] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_gedung]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_gedung](
	[id_gedung] [int] IDENTITY(1,1) NOT NULL,
	[nama_gedung] [varchar](20) NOT NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_gedung] PRIMARY KEY CLUSTERED 
(
	[id_gedung] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[m_distributor]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[m_distributor](
	[id_distributor] [int] IDENTITY(1,1) NOT NULL,
	[nama_distributor] [varchar](50) NULL,
	[alamat] [varchar](1000) NULL,
	[no_telp] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[website] [varchar](50) NULL,
	[created_date] [varchar](20) NULL,
	[creator] [varchar](20) NULL,
	[last_updated_date] [varchar](20) NULL,
	[updater] [varchar](20) NULL,
 CONSTRAINT [PK_m_distributor] PRIMARY KEY CLUSTERED 
(
	[id_distributor] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[UPSERT_PENUGASAN_INVENTARIS_DETAIL]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maulana Wahid A - Tatat Nuraeni>
-- Create date: <03 Oct 15>
-- Description:	<Stored Procedure To Save/Update Stock>
-- =============================================
CREATE PROCEDURE [dbo].[UPSERT_PENUGASAN_INVENTARIS_DETAIL] 
	-- Add the parameters for 
	@id_penugasan int,
	@id_inventaris bigint,
	@qty_diperbaiki int,
	@updater int
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [t_penugasan_detail] SET qty_perbaikan = @qty_diperbaiki, status_perbaikan = 1, updater=@updater, last_updated_date = getdate() WHERE id_inventaris = @id_inventaris and id_penugasan = @id_penugasan
END
GO
/****** Object:  StoredProcedure [dbo].[UPSERT_INVENTARIS_RUANGAN]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPSERT_INVENTARIS_RUANGAN] 
	-- Add the parameters for 
	@id_ruangan bigint,
	@id_inventaris bigint,
	@qty_ditambah int
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @qty_sebelum int;
	
	set @qty_sebelum = 0;
	
	Select @qty_sebelum = qty_tersedia from t_inventaris_ruangan where id_ruangan = @id_ruangan and id_inventaris = @id_inventaris

    -- Insert statements for procedure here
	UPDATE [t_inventaris_ruangan] SET qty_tersedia = qty_tersedia + @qty_ditambah WHERE id_inventaris = @id_inventaris and id_ruangan = @id_ruangan	IF @@ROWCOUNT=0
    INSERT INTO [t_inventaris_ruangan](id_ruangan,id_inventaris, qty_tersedia) values(@id_ruangan,@id_inventaris,@qty_ditambah)
    -- Add to Stock History, 1 for added from penerimaan and 2 added from adjustment
    INSERT [t_history_inventaris_ruangan](id_inventaris,id_ruangan,qty_sebelum, qty_penambahan, qty_sesudah) 
    values(@id_inventaris, @id_ruangan,@qty_sebelum,@qty_ditambah, @qty_sebelum + @qty_ditambah)
	--update stock
	UPDATE [t_stock_inventaris] set qty_terpakai = qty_terpakai + @qty_ditambah, qty_tersedia = qty_tersedia - @qty_ditambah where id_inventaris = @id_inventaris

END
GO
/****** Object:  StoredProcedure [dbo].[UPSERT_CATATAN_INVENTARIS_DETAIL]    Script Date: 03/31/2017 23:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maulana Wahid A - Tatat Nuraeni>
-- Create date: <03 Oct 15>
-- Description:	<Stored Procedure To Save/Update Stock>
-- =============================================
CREATE PROCEDURE [dbo].[UPSERT_CATATAN_INVENTARIS_DETAIL] 
	-- Add the parameters for 
	@id_inventaris bigint,
	@tipe int,
	@id_ruangan int,
	@id_catatan bigint,
	@qty int,
	@catatan varchar(255)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [t_pencatatan_inventaris_detail] SET qty = @qty, tipe = @tipe, catatan = @catatan WHERE id_inventaris = @id_inventaris and id_pencatatan = @id_catatan
	IF @@ROWCOUNT=0
    INSERT INTO [t_pencatatan_inventaris_detail](id_inventaris,id_pencatatan,id_ruangan, qty, tipe, catatan) values(@id_inventaris,@id_catatan,@id_ruangan,@qty, @tipe, @catatan)
END
GO
/****** Object:  View [dbo].[v_service_keluar_det_report]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_service_keluar_det_report]
AS
SELECT DISTINCT 
                         p.id_pengajuan_service_detail, pin.id_pengajuan_service, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.biaya_service, p.biaya_service_total, 
                         inv.nama_inventaris
FROM            dbo.t_pengajuan_service_detail AS p INNER JOIN
                         dbo.t_pengajuan_service AS pin ON p.id_pengajuan_service = pin.id_pengajuan_service LEFT OUTER JOIN
                         dbo.m_inventaris AS inv ON p.id_inventaris = inv.id_inventaris
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[4] 2[33] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 260
               Bottom = 135
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 273
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pin"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service_keluar_det_report'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service_keluar_det_report'
GO
/****** Object:  View [dbo].[v_service_keluar]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_service_keluar]
AS
SELECT     a.id_service_keluar, a.id_pengajuan_service, a.judul, CONVERT(date, a.tanggal_service) AS tanggal_service, a.keterangan, b.no_pengajuan_service, 
                      CASE WHEN a.status = 0 THEN 'Dalam Process' WHEN a.status = 1 THEN 'Masih Dikerjakan' WHEN a.status = 2 THEN 'Selesai' WHEN a.status = 3 THEN 'Selesai Sebagian'
                       WHEN a.status = 4 THEN 'Sebagian Tidak dapat dikerjakan' WHEN a.status = 5 THEN 'Tidak dapat dikerjakan' END AS status
FROM         dbo.t_service_keluar AS a LEFT OUTER JOIN
                      dbo.t_pengajuan_service AS b ON a.id_pengajuan_service = b.id_pengajuan_service
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 272
               Bottom = 135
               Right = 490
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service_keluar'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service_keluar'
GO
/****** Object:  View [dbo].[v_service]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_service]
AS
SELECT        id_pengajuan_service, no_pengajuan_service, judul_pengajuan_service, catatan_kabag, catatan_pimpinan, id_user, status_konfirmasi, status_pengajuan, 
                         created_date, approval_date, status_prioritas, file_save,
                             (SELECT        nama_lengkap
                               FROM            dbo.m_user
                               WHERE        (id_user = pi.id_user_pimpinan)) AS pimpinan,
                             (SELECT        nama_lengkap
                               FROM            dbo.m_user AS m_user_1
                               WHERE        (id_user = pi.id_user)) AS kabag
FROM            dbo.t_pengajuan_service AS pi
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[21] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pi"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_service'
GO
/****** Object:  View [dbo].[v_ruangan]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ruangan]
AS
SELECT        r.id_ruangan, r.nama_ruangan, g.id_gedung, g.nama_gedung, l.id_lantai, l.lokasi_lantai
FROM            dbo.m_ruangan AS r INNER JOIN
                         dbo.m_lantai AS l ON r.id_lantai = l.id_lantai INNER JOIN
                         dbo.m_gedung AS g ON l.id_gedung = g.id_gedung
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 255
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 125
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ruangan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ruangan'
GO
/****** Object:  View [dbo].[v_pengajuan_report_detail]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_pengajuan_report_detail]
AS
SELECT        p.id_pengajuan_detail, p.id_pengajuan_inventaris, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.harga_satuan, p.harga_total, pin.no_pengajuan, 
                         inv.nama_inventaris
FROM            dbo.t_pengajuan_inventaris_detail AS p INNER JOIN
                         dbo.m_inventaris AS inv ON p.id_inventaris = inv.id_inventaris INNER JOIN
                         dbo.t_pengajuan_inventaris AS pin ON p.id_pengajuan_inventaris = pin.id_pengajuan
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 260
               Bottom = 135
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pin"
            Begin Extent = 
               Top = 6
               Left = 487
               Bottom = 135
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan_report_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan_report_detail'
GO
/****** Object:  View [dbo].[v_pengajuan_inventaris_detail]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_pengajuan_inventaris_detail]
AS
SELECT        p.id_pengajuan_detail, p.id_pengajuan_inventaris, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.harga_satuan, p.harga_total, pin.no_pengajuan, 
                         inv.nama_inventaris
FROM            dbo.t_pengajuan_inventaris_detail AS p INNER JOIN
                         dbo.m_inventaris AS inv ON p.id_inventaris = inv.id_inventaris INNER JOIN
                         dbo.t_pengajuan_inventaris AS pin ON p.id_pengajuan_inventaris = pin.id_pengajuan
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[27] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 260
               Bottom = 135
               Right = 444
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pin"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan_inventaris_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan_inventaris_detail'
GO
/****** Object:  View [dbo].[v_pengajuan]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_pengajuan]
AS
SELECT        id_pengajuan, no_pengajuan, judul_pengajuan, catatan_pimpinan, id_user_pimpinan, catatan_kabag, id_user, status_konfirmasi, created_date, creator, 
                         last_updated_date, updater, approval_date, harga_total, file_save,
                             (SELECT        nama_lengkap
                               FROM            dbo.m_user
                               WHERE        (id_user = pi.id_user_pimpinan)) AS pimpinan,
                             (SELECT        nama_lengkap
                               FROM            dbo.m_user AS m_user_1
                               WHERE        (id_user = pi.id_user)) AS kabag, status_prioritas
FROM            dbo.t_pengajuan_inventaris AS pi
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[10] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pi"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pengajuan'
GO
/****** Object:  View [dbo].[v_penerimaan_report_detail]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_penerimaan_report_detail]
AS
SELECT        i.nama_inventaris, i.id_inventaris, a.qty_diajukan, a.catatan_pengajuan, a.qty_diterima, a.keterangan, a.id_penerimaan
FROM            dbo.t_penerimaan_detail AS a INNER JOIN
                         dbo.m_inventaris AS i ON a.id_inventaris = i.id_inventaris
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[22] 2[22] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -288
         Left = 0
      End
      Begin Tables = 
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 326
               Bottom = 135
               Right = 515
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 294
               Left = 38
               Bottom = 423
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_penerimaan_report_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_penerimaan_report_detail'
GO
/****** Object:  View [dbo].[v_penerimaan_report]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_penerimaan_report]
AS
SELECT        TOP (100) PERCENT a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, a.tanggal_penerimaan, a.creator AS username, a.id_user, a.id_pembelian, 
                         b.no_pengajuan AS ref_pengajuan, b.created_date, a.status_penerimaan, a.keterangan
FROM            dbo.t_penerimaan AS a LEFT OUTER JOIN
                         dbo.t_pengajuan_inventaris AS b ON a.id_penerimaan = b.id_pengajuan LEFT OUTER JOIN
                         dbo.m_user AS c ON a.id_user = c.id_user
ORDER BY a.id_penerimaan DESC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[6] 2[51] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 273
               Bottom = 135
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 495
               Bottom = 135
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_penerimaan_report'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_penerimaan_report'
GO
/****** Object:  View [dbo].[v_pembelian_report]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_pembelian_report]
AS
SELECT     a.id_pembelian, a.harga_total, a.no_pembelian AS judul, b.id_pengajuan, CONVERT(date, a.tanggal_pembelian) AS tanggal, a.keterangan, a.no_pembelian, 
                      CASE WHEN status = 0 THEN 'Belum Dibeli' WHEN status = 1 THEN 'Dalam Proses' WHEN status = 2 THEN 'Sudah Dibeli' WHEN status = 3 THEN 'Tidak dibeli' END AS
                       status
FROM         dbo.t_pembelian AS a LEFT OUTER JOIN
                      dbo.t_pengajuan_inventaris AS b ON a.id_pengajuan_inventaris = b.id_pengajuan
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[4] 2[37] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 135
               Right = 430
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pembelian_report'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pembelian_report'
GO
/****** Object:  View [dbo].[v_pembelian_det_report]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_pembelian_det_report]
AS
SELECT        p.id_pengajuan_detail, p.id_pengajuan_inventaris, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.harga_satuan, p.harga_total, pin.no_pengajuan, 
                         inv.nama_inventaris
FROM            dbo.t_pengajuan_inventaris_detail AS p INNER JOIN
                         dbo.m_inventaris AS inv ON p.id_inventaris = inv.id_inventaris INNER JOIN
                         dbo.t_pengajuan_inventaris AS pin ON p.id_pengajuan_inventaris = pin.id_pengajuan
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 260
               Bottom = 135
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pin"
            Begin Extent = 
               Top = 6
               Left = 487
               Bottom = 135
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pembelian_det_report'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_pembelian_det_report'
GO
/****** Object:  View [dbo].[v_notif_pimpinan]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_notif_pimpinan]
AS
SELECT        id_pengajuan, 'Pengajuan Barang' AS tipe  FROM      [t_pengajuan_inventaris]
WHERE        status_konfirmasi= 0
UNION
SELECT        id_pengajuan_service, 'Pengajuan Service' AS tipe FROM            [t_pengajuan_service]
WHERE        status_konfirmasi = 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[23] 4[6] 2[48] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_notif_pimpinan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_notif_pimpinan'
GO
/****** Object:  View [dbo].[v_inventaris]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventaris]
AS
SELECT        inv.id_inventaris, inv.nama_inventaris, inv.qty, inv.harga, inv.keterangan, inv.gambar, j_inv.nama_jenis_inventaris AS jenis_inventaris, 
                         dis.nama_distributor AS distributor, m.nama_merk AS merk, inv.qty_belum_terpakai, inv.qty_dalam_ruangan
FROM            dbo.m_inventaris AS inv LEFT OUTER JOIN
                         dbo.m_jenis_inventaris AS j_inv ON inv.id_jenis_inventaris = j_inv.id_jenis_inventaris LEFT OUTER JOIN
                         dbo.m_distributor AS dis ON inv.id_distributor = dis.id_distributor LEFT OUTER JOIN
                         dbo.m_merk AS m ON inv.id_merk = m.id_merk
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[5] 2[36] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "j_inv"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 125
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dis"
            Begin Extent = 
               Top = 6
               Left = 474
               Bottom = 125
               Right = 654
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventaris'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventaris'
GO
/****** Object:  View [dbo].[v_inv_total]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inv_total]
AS
SELECT        TOP (100) PERCENT g.id_gedung, g.nama_gedung, l.id_lantai, l.lokasi_lantai, r.id_ruangan, r.nama_ruangan, ir.id_inventaris_ruangan, i.id_inventaris, 
                         i.nama_inventaris, ir.qty_terpakai, ir.qty_rusak, ir.qty_hilang, ir.qty_total, ir.keterangan
FROM            dbo.t_inventaris_ruangan AS ir INNER JOIN
                         dbo.m_inventaris AS i ON ir.id_inventaris = i.id_inventaris INNER JOIN
                         dbo.m_ruangan AS r ON ir.id_ruangan = r.id_ruangan INNER JOIN
                         dbo.m_lantai AS l ON l.id_lantai = r.id_lantai INNER JOIN
                         dbo.m_gedung AS g ON l.id_gedung = g.id_gedung
ORDER BY g.id_gedung, r.id_lantai
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[27] 4[24] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ir"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 125
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 365
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 6
               Left = 474
               Bottom = 135
               Right = 658
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_total'
GO
/****** Object:  View [dbo].[v_inv_rusak_hilang]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inv_rusak_hilang]
AS
SELECT        TOP (100) PERCENT g.id_gedung, g.nama_gedung, l.id_lantai, l.lokasi_lantai, r.id_ruangan, r.nama_ruangan, ir.id_inventaris_ruangan, i.id_inventaris, 
                         i.nama_inventaris, ir.qty_terpakai, ir.qty_rusak, ir.qty_hilang, ir.qty_total, ir.keterangan
FROM            dbo.t_inventaris_ruangan AS ir INNER JOIN
                         dbo.m_inventaris AS i ON ir.id_inventaris = i.id_inventaris INNER JOIN
                         dbo.m_ruangan AS r ON ir.id_ruangan = r.id_ruangan INNER JOIN
                         dbo.m_lantai AS l ON l.id_lantai = r.id_lantai INNER JOIN
                         dbo.m_gedung AS g ON l.id_gedung = g.id_gedung
WHERE        (ir.qty_rusak IS NULL) OR
                         (ir.qty_rusak <> 0) OR
                         (ir.qty_hilang IS NULL) OR
                         (ir.qty_hilang <> 0)
ORDER BY g.id_gedung, r.id_lantai
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[6] 2[45] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ir"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 260
               Bottom = 135
               Right = 444
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 482
               Bottom = 135
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 222
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 138
               Left = 260
               Bottom = 267
               Right = 444
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_rusak_hilang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_rusak_hilang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_rusak_hilang'
GO
/****** Object:  View [dbo].[v_inv_rusak]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inv_rusak]
AS
SELECT        TOP (100) PERCENT g.id_gedung, g.nama_gedung, l.id_lantai, l.lokasi_lantai, r.id_ruangan, r.nama_ruangan, ir.id_inventaris_ruangan, i.id_inventaris, 
                         i.nama_inventaris, ir.qty_terpakai, ir.qty_rusak, ir.qty_hilang, ir.qty_total, ir.keterangan
FROM            dbo.t_inventaris_ruangan AS ir INNER JOIN
                         dbo.m_inventaris AS i ON ir.id_inventaris = i.id_inventaris INNER JOIN
                         dbo.m_ruangan AS r ON ir.id_ruangan = r.id_ruangan INNER JOIN
                         dbo.m_lantai AS l ON l.id_lantai = r.id_lantai INNER JOIN
                         dbo.m_gedung AS g ON l.id_gedung = g.id_gedung
WHERE        (ir.qty_rusak IS NOT NULL) OR
                         (ir.qty_rusak <> 0)
ORDER BY l.id_gedung, r.id_lantai
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[27] 4[5] 2[50] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ir"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 125
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 365
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 6
               Left = 474
               Bottom = 135
               Right = 658
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_rusak'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_rusak'
GO
/****** Object:  View [dbo].[v_inv_hilang]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inv_hilang]
AS
SELECT        TOP (100) PERCENT g.id_gedung, g.nama_gedung, l.id_lantai, l.lokasi_lantai, r.id_ruangan, r.nama_ruangan, ir.id_inventaris_ruangan, i.id_inventaris, 
                         i.nama_inventaris, ir.qty_terpakai, ir.qty_rusak, ir.qty_hilang, ir.qty_total, ir.keterangan
FROM            dbo.t_inventaris_ruangan AS ir INNER JOIN
                         dbo.m_inventaris AS i ON ir.id_inventaris = i.id_inventaris INNER JOIN
                         dbo.m_ruangan AS r ON ir.id_ruangan = r.id_ruangan INNER JOIN
                         dbo.m_lantai AS l ON l.id_lantai = r.id_lantai INNER JOIN
                         dbo.m_gedung AS g ON l.id_gedung = g.id_gedung
WHERE        (ir.qty_hilang IS NOT NULL) OR
                         (ir.qty_hilang <> 0)
ORDER BY l.id_gedung, r.id_lantai
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[6] 2[51] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ir"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 125
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 365
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 6
               Left = 474
               Bottom = 135
               Right = 658
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_hilang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_hilang'
GO
/****** Object:  View [dbo].[v_inv_detail]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inv_detail]
AS
SELECT        dbo.m_inventaris.nama_inventaris, dbo.m_inventaris.harga, dbo.m_inventaris.keterangan, dbo.m_distributor.nama_distributor, dbo.m_distributor.alamat, 
                         dbo.m_jenis_inventaris.nama_jenis_inventaris, dbo.m_merk.nama_merk
FROM            dbo.m_inventaris LEFT OUTER JOIN
                         dbo.m_distributor ON dbo.m_inventaris.id_distributor = dbo.m_distributor.id_distributor LEFT OUTER JOIN
                         dbo.m_jenis_inventaris ON dbo.m_inventaris.id_jenis_inventaris = dbo.m_jenis_inventaris.id_jenis_inventaris LEFT OUTER JOIN
                         dbo.m_merk ON dbo.m_inventaris.id_merk = dbo.m_merk.id_merk
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[6] 2[52] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "m_inventaris"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 169
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "m_distributor"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m_jenis_inventaris"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "m_merk"
            Begin Extent = 
               Top = 174
               Left = 256
               Bottom = 293
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inv_detail'
GO
/****** Object:  StoredProcedure [dbo].[UPSERT_STOCK_INVENTARIS]    Script Date: 03/31/2017 23:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPSERT_STOCK_INVENTARIS] 
	-- Add the parameters for 
	@id_penerimaan bigint,
	@id_inventaris bigint,
	@qty_diterima int,
   @module varchar(30),
	@reason varchar(30),
	@username varchar(30)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @qty_sebelum int;
	declare @qty_sesudah int;

	select @qty_sebelum = qty from m_inventaris where id = @id_inventaris;

	set @qty_sesudah = @qty_sebelum + @qty_diterima;

    -- Old Stock
	---UPDATE [t_stock_inventaris] SET qty_tersedia = qty_tersedia + @qty_diterima WHERE id_inventaris = @id_inventaris	IF @@ROWCOUNT=0
    --INSERT INTO [t_stock_inventaris](id_inventaris, qty_tersedia) values(@id_inventaris,@qty_diterima)
    -- Add to Stock History, 1 for added from penerimaan and 2 added from adjustment
    --INSERT [t_history_stock](id_inventaris,type_history, add_or_substract, qty, id_referensi) values(@id_inventaris, 1,1,@qty_diterima, @id_penerimaan)
	
	UPDATE [m_inventaris] SET qty = @qty_sesudah WHERE id = @id_inventaris	
    --INSERT INTO [t_stock_inventaris](id_inventaris, qty_tersedia) values(@id_inventaris,@qty_diterima)
    -- Add to Stock History, 1 for added from penerimaan and 2 added from adjustment
    
	INSERT [t_history_inventaris](id_inventaris,qty_sebelum, qty_sesudah, qty_add_minus, add_or_minus, module, reason, username, id_module)
	 values(@id_inventaris,@qty_sebelum ,@qty_sesudah,@qty_diterima,1, @module, @reason, @username, @id_penerimaan)


END
GO
/****** Object:  Default [DF_t_penugasan_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_tugas] ADD  CONSTRAINT [DF_t_penugasan_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_penugasan_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_tugas] ADD  CONSTRAINT [DF_t_penugasan_status]  DEFAULT ((0)) FOR [status_tugas]
GO
/****** Object:  Default [DF_t_service_keluar_status_1]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_service_keluar] ADD  CONSTRAINT [DF_t_service_keluar_status_1]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_t_penugasan_lain_detail_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_penugasan_inventaris_detail] ADD  CONSTRAINT [DF_t_penugasan_lain_detail_status]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_t_penugasan_lain_detail_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_penugasan_inventaris_detail] ADD  CONSTRAINT [DF_t_penugasan_lain_detail_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_penugasan_inventaris_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_penugasan_inventaris] ADD  CONSTRAINT [DF_t_penugasan_inventaris_status]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_t_penugasan_inventaris_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_penugasan_inventaris] ADD  CONSTRAINT [DF_t_penugasan_inventaris_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_penugasan_inventaris_is_start]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_penugasan_inventaris] ADD  CONSTRAINT [DF_t_penugasan_inventaris_is_start]  DEFAULT ((0)) FOR [is_start]
GO
/****** Object:  Default [DF_t_service_keluar_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pengajuan_service] ADD  CONSTRAINT [DF_t_service_keluar_status]  DEFAULT ((0)) FOR [status_konfirmasi]
GO
/****** Object:  Default [DF_t_pengajuan_service_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pengajuan_service] ADD  CONSTRAINT [DF_t_pengajuan_service_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_pengajuan_inventaris_detail_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pengajuan_inventaris_detail] ADD  CONSTRAINT [DF_t_pengajuan_inventaris_detail_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_pengajuan_inventaris_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pengajuan_inventaris] ADD  CONSTRAINT [DF_t_pengajuan_inventaris_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_pencatatan_inventaris_detail_created_date]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pencatatan_inventaris_detail] ADD  CONSTRAINT [DF_t_pencatatan_inventaris_detail_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  Default [DF_t_pencatatan_inventaris_detail_status_perbaikan]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pencatatan_inventaris_detail] ADD  CONSTRAINT [DF_t_pencatatan_inventaris_detail_status_perbaikan]  DEFAULT ((0)) FOR [status_perbaikan]
GO
/****** Object:  Default [DF_t_pencatatan_inventaris_detail_status_simpan]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pencatatan_inventaris_detail] ADD  CONSTRAINT [DF_t_pencatatan_inventaris_detail_status_simpan]  DEFAULT ((0)) FOR [status_simpan]
GO
/****** Object:  Default [DF_t_pencatatan_inventaris_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pencatatan_inventaris] ADD  CONSTRAINT [DF_t_pencatatan_inventaris_status]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_t_pembelian_status]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_pembelian] ADD  CONSTRAINT [DF_t_pembelian_status]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_t_inventaris_ruangan_qty_total]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_inventaris_ruangan] ADD  CONSTRAINT [DF_t_inventaris_ruangan_qty_total]  DEFAULT ((0)) FOR [qty_total]
GO
/****** Object:  Default [DF_t_inventaris_ruangan_qty_rusak]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_inventaris_ruangan] ADD  CONSTRAINT [DF_t_inventaris_ruangan_qty_rusak]  DEFAULT ((0)) FOR [qty_rusak]
GO
/****** Object:  Default [DF_t_inventaris_ruangan_qty_terpakai]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_inventaris_ruangan] ADD  CONSTRAINT [DF_t_inventaris_ruangan_qty_terpakai]  DEFAULT ((0)) FOR [qty_terpakai]
GO
/****** Object:  Default [DF_t_inventaris_ruangan_qty_hilang]    Script Date: 03/31/2017 23:18:10 ******/
ALTER TABLE [dbo].[t_inventaris_ruangan] ADD  CONSTRAINT [DF_t_inventaris_ruangan_qty_hilang]  DEFAULT ((0)) FOR [qty_hilang]
GO
/****** Object:  Default [DF_m_inventaris_qty]    Script Date: 03/31/2017 23:18:23 ******/
ALTER TABLE [dbo].[m_inventaris] ADD  CONSTRAINT [DF_m_inventaris_qty]  DEFAULT ((0)) FOR [qty]
GO
