USE [InventoryRev]
GO
/****** Object:  Table [dbo].[m_user]    Script Date: 04/16/2017 22:07:54 ******/
SET IDENTITY_INSERT [dbo].[m_user] ON
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (1, N'admin', N'24be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'Administrator', N'082219194323', N'admin@bprtik.go.id', N'Administrator', 1, NULL)
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (2, N'Koordinator BPRTIK', N'ac845ea10d442bbf26eab393a7e46edeb1c13c0b8b0fb8d54811cdf15c769', N'Koordinator BPRTIK', N'085774008279', N'pimpinan@bprtik.go.id', N'', 3, NULL)
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (3, N'Ketua Divisi', N'21f97d78461cad0411af4c8ce197774712ce22fd58869cd963fdb57eca078d8', N'Ketua Divisi', N'085885750887', N'kabag@bprtik.go.id', N'', 2, NULL)
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (4, N'Bagian Pencatatan', N'6f4ce0baf884a2ee6d9850d677fe9184a94538a2bd11a34e2648f3c8ea928e', N'Bagian Pencatatan', N'0834342', N'pencatat@bprtik.go.id', N'', 4, NULL)
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (6, N'teknisi2', N'e341ae3229c8e6fd133a396742adf429b61b4c6fe6918648e6048665111b4b4', N'Hatta', N'087545670', N'hatta@gmail.com', N'teknisi 2', 5, NULL)
INSERT [dbo].[m_user] ([id_user], [username], [password], [nama_lengkap], [no_telp], [email], [deskripsi], [id_role], [role]) VALUES (5, N'teknisi', N'89f4c09f710cd9ccbb2dbf74df4f22c93192a9127965b5b6afd74937bf957d', N'Teknisi', N'083434', N'teknisi@bprtik.go.id', N'', 5, NULL)
SET IDENTITY_INSERT [dbo].[m_user] OFF
