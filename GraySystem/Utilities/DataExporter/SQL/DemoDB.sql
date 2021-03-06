SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RFS_TxType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RFS_TxType](
	[EnvironmentID] [nvarchar](20) NOT NULL,
	[Tx] [nchar](50) NULL,
	[TxType] [nchar](50) NULL,
	[WorkFlowID] [nchar](50) NOT NULL,
 CONSTRAINT [PK_RFS_TxType] PRIMARY KEY CLUSTERED 
(
	[EnvironmentID] ASC,
	[WorkFlowID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F0116]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F0116](
	[ALAN8] [float] NOT NULL,
	[ALEFTB] [numeric](18, 0) NOT NULL,
	[ALEFTF] [nchar](1) NULL,
	[ALADD1] [nchar](40) NULL,
	[ALADD2] [nchar](40) NULL,
	[ALADD3] [nchar](40) NULL,
	[ALADD4] [nchar](40) NULL,
	[ALADDZ] [nchar](12) NULL,
	[ALCTY1] [nchar](25) NULL,
	[ALCOUN] [nchar](25) NULL,
	[ALADDS] [nchar](3) NULL,
	[ALCRTE] [nchar](4) NULL,
	[ALBKML] [nchar](2) NULL,
	[ALCTR] [nchar](3) NULL,
	[ALUSER] [nchar](10) NULL,
	[ALPID] [nchar](10) NULL,
	[ALUPMJ] [numeric](18, 0) NULL,
	[ALJOBN] [nchar](10) NULL,
	[ALUPMT] [float] NULL,
	[ALSYNCS] [float] NULL,
	[ALCAAD] [float] NULL,
 CONSTRAINT [F0116_PK] PRIMARY KEY CLUSTERED 
(
	[ALAN8] ASC,
	[ALEFTB] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F0006]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F0006](
	[MCMCU] [nchar](12) NOT NULL,
	[MCSTYL] [nchar](2) NULL,
	[MCDC] [nchar](40) NULL,
	[MCLDM] [nchar](1) NULL,
	[MCCO] [nchar](5) NULL,
	[MCAN8] [float] NULL,
	[MCAN8O] [float] NULL,
	[MCCNTY] [nchar](3) NULL,
	[MCADDS] [nchar](3) NULL,
	[MCFMOD] [nchar](1) NULL,
	[MCDL01] [nchar](30) NULL,
	[MCDL02] [nchar](30) NULL,
	[MCDL03] [nchar](30) NULL,
	[MCDL04] [nchar](30) NULL,
	[MCRP01] [nchar](3) NULL,
	[MCRP02] [nchar](3) NULL,
	[MCRP03] [nchar](3) NULL,
	[MCRP04] [nchar](3) NULL,
	[MCRP05] [nchar](3) NULL,
	[MCRP06] [nchar](3) NULL,
	[MCRP07] [nchar](3) NULL,
	[MCRP08] [nchar](3) NULL,
	[MCRP09] [nchar](3) NULL,
	[MCRP10] [nchar](3) NULL,
	[MCRP11] [nchar](3) NULL,
	[MCRP12] [nchar](3) NULL,
	[MCRP13] [nchar](3) NULL,
	[MCRP14] [nchar](3) NULL,
	[MCRP15] [nchar](3) NULL,
	[MCRP16] [nchar](3) NULL,
	[MCRP17] [nchar](3) NULL,
	[MCRP18] [nchar](3) NULL,
	[MCRP19] [nchar](3) NULL,
	[MCRP20] [nchar](3) NULL,
	[MCRP21] [nchar](10) NULL,
	[MCRP22] [nchar](10) NULL,
	[MCRP23] [nchar](10) NULL,
	[MCRP24] [nchar](10) NULL,
	[MCRP25] [nchar](10) NULL,
	[MCRP26] [nchar](10) NULL,
	[MCRP27] [nchar](10) NULL,
	[MCRP28] [nchar](10) NULL,
	[MCRP29] [nchar](10) NULL,
	[MCRP30] [nchar](10) NULL,
	[MCTA] [nchar](10) NULL,
	[MCTXJS] [float] NULL,
	[MCTXA1] [nchar](10) NULL,
	[MCEXR1] [nchar](2) NULL,
	[MCTC01] [nchar](4) NULL,
	[MCTC02] [nchar](4) NULL,
	[MCTC03] [nchar](4) NULL,
	[MCTC04] [nchar](4) NULL,
	[MCTC05] [nchar](4) NULL,
	[MCTC06] [nchar](4) NULL,
	[MCTC07] [nchar](4) NULL,
	[MCTC08] [nchar](4) NULL,
	[MCTC09] [nchar](4) NULL,
	[MCTC10] [nchar](4) NULL,
	[MCND01] [nchar](1) NULL,
	[MCND02] [nchar](1) NULL,
	[MCND03] [nchar](1) NULL,
	[MCND04] [nchar](1) NULL,
	[MCND05] [nchar](1) NULL,
	[MCND06] [nchar](1) NULL,
	[MCND07] [nchar](1) NULL,
	[MCND08] [nchar](1) NULL,
	[MCND09] [nchar](1) NULL,
	[MCND10] [nchar](1) NULL,
	[MCCC01] [nchar](1) NULL,
	[MCCC02] [nchar](1) NULL,
	[MCCC03] [nchar](1) NULL,
	[MCCC04] [nchar](1) NULL,
	[MCCC05] [nchar](1) NULL,
	[MCCC06] [nchar](1) NULL,
	[MCCC07] [nchar](1) NULL,
	[MCCC08] [nchar](1) NULL,
	[MCCC09] [nchar](1) NULL,
	[MCCC10] [nchar](1) NULL,
	[MCPECC] [nchar](1) NULL,
	[MCALS] [nchar](1) NULL,
	[MCISS] [nchar](1) NULL,
	[MCGLBA] [nchar](8) NULL,
	[MCALCL] [nchar](2) NULL,
	[MCLMTH] [nchar](1) NULL,
	[MCLF] [float] NULL,
	[MCOBJ1] [nchar](6) NULL,
	[MCOBJ2] [nchar](6) NULL,
	[MCOBJ3] [nchar](6) NULL,
	[MCSUB1] [nchar](8) NULL,
	[MCTOU] [float] NULL,
	[MCSBLI] [nchar](1) NULL,
	[MCANPA] [float] NULL,
	[MCCT] [nchar](4) NULL,
	[MCCERT] [nchar](1) NULL,
	[MCMCUS] [nchar](12) NULL,
	[MCBTYP] [nchar](1) NULL,
	[MCPC] [float] NULL,
	[MCPCA] [float] NULL,
	[MCPCC] [float] NULL,
	[MCINTA] [nchar](4) NULL,
	[MCINTL] [nchar](4) NULL,
	[MCD1J] [numeric](18, 0) NULL,
	[MCD2J] [numeric](18, 0) NULL,
	[MCD3J] [numeric](18, 0) NULL,
	[MCD4J] [numeric](18, 0) NULL,
	[MCD5J] [numeric](18, 0) NULL,
	[MCD6J] [numeric](18, 0) NULL,
	[MCFPDJ] [numeric](18, 0) NULL,
	[MCCAC] [float] NULL,
	[MCPAC] [float] NULL,
	[MCEEO] [nchar](1) NULL,
	[MCERC] [nchar](2) NULL,
	[MCUSER] [nchar](10) NULL,
	[MCPID] [nchar](10) NULL,
	[MCUPMJ] [numeric](18, 0) NULL,
	[MCJOBN] [nchar](10) NULL,
	[MCUPMT] [float] NULL,
	[MCBPTP] [nchar](15) NULL,
	[MCAPSB] [nchar](1) NULL,
	[MCTSBU] [nchar](12) NULL,
 CONSTRAINT [F0006_PK] PRIMARY KEY CLUSTERED 
(
	[MCMCU] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F4101]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F4101](
	[IMITM] [float] NOT NULL,
	[IMLITM] [nchar](25) NULL,
	[IMAITM] [nchar](25) NULL,
	[IMDSC1] [nchar](30) NULL,
	[IMDSC2] [nchar](30) NULL,
	[IMSRTX] [nchar](30) NULL,
	[IMALN] [nchar](30) NULL,
	[IMSRP1] [nchar](3) NULL,
	[IMSRP2] [nchar](3) NULL,
	[IMSRP3] [nchar](3) NULL,
	[IMSRP4] [nchar](3) NULL,
	[IMSRP5] [nchar](3) NULL,
	[IMSRP6] [nchar](6) NULL,
	[IMSRP7] [nchar](6) NULL,
	[IMSRP8] [nchar](6) NULL,
	[IMSRP9] [nchar](6) NULL,
	[IMSRP0] [nchar](6) NULL,
	[IMPRP1] [nchar](3) NULL,
	[IMPRP2] [nchar](3) NULL,
	[IMPRP3] [nchar](3) NULL,
	[IMPRP4] [nchar](3) NULL,
	[IMPRP5] [nchar](3) NULL,
	[IMPRP6] [nchar](6) NULL,
	[IMPRP7] [nchar](6) NULL,
	[IMPRP8] [nchar](6) NULL,
	[IMPRP9] [nchar](6) NULL,
	[IMPRP0] [nchar](6) NULL,
	[IMCDCD] [nchar](15) NULL,
	[IMPDGR] [nchar](3) NULL,
	[IMDSGP] [nchar](3) NULL,
	[IMPRGR] [nchar](8) NULL,
	[IMRPRC] [nchar](8) NULL,
	[IMORPR] [nchar](8) NULL,
	[IMBUYR] [float] NULL,
	[IMDRAW] [nchar](20) NULL,
	[IMRVNO] [nchar](2) NULL,
	[IMDSZE] [nchar](1) NULL,
	[IMVCUD] [float] NULL,
	[IMCARS] [float] NULL,
	[IMCARP] [float] NULL,
	[IMSHCN] [nchar](3) NULL,
	[IMSHCM] [nchar](3) NULL,
	[IMUOM1] [nchar](2) NULL,
	[IMUOM2] [nchar](2) NULL,
	[IMUOM3] [nchar](2) NULL,
	[IMUOM4] [nchar](2) NULL,
	[IMUOM6] [nchar](2) NULL,
	[IMUOM8] [nchar](2) NULL,
	[IMUOM9] [nchar](2) NULL,
	[IMUWUM] [nchar](2) NULL,
	[IMUVM1] [nchar](2) NULL,
	[IMSUTM] [nchar](2) NULL,
	[IMUMVW] [nchar](1) NULL,
	[IMCYCL] [nchar](3) NULL,
	[IMGLPT] [nchar](4) NULL,
	[IMPLEV] [nchar](1) NULL,
	[IMPPLV] [nchar](1) NULL,
	[IMCLEV] [nchar](1) NULL,
	[IMPRPO] [nchar](1) NULL,
	[IMCKAV] [nchar](1) NULL,
	[IMBPFG] [nchar](1) NULL,
	[IMSRCE] [nchar](1) NULL,
	[IMOT1Y] [nchar](1) NULL,
	[IMOT2Y] [nchar](1) NULL,
	[IMSTDP] [float] NULL,
	[IMFRMP] [float] NULL,
	[IMTHRP] [float] NULL,
	[IMSTDG] [nchar](3) NULL,
	[IMFRGD] [nchar](3) NULL,
	[IMTHGD] [nchar](3) NULL,
	[IMCOTY] [nchar](1) NULL,
	[IMSTKT] [nchar](1) NULL,
	[IMLNTY] [nchar](2) NULL,
	[IMCONT] [nchar](1) NULL,
	[IMBACK] [nchar](1) NULL,
	[IMIFLA] [nchar](2) NULL,
	[IMTFLA] [nchar](2) NULL,
	[IMINMG] [nchar](10) NULL,
	[IMABCS] [nchar](1) NULL,
	[IMABCM] [nchar](1) NULL,
	[IMABCI] [nchar](1) NULL,
	[IMOVR] [nchar](1) NULL,
	[IMWARR] [nchar](8) NULL,
	[IMCMCG] [nchar](8) NULL,
	[IMSRNR] [nchar](1) NULL,
	[IMPMTH] [nchar](1) NULL,
	[IMFIFO] [nchar](1) NULL,
	[IMLOTS] [nchar](1) NULL,
	[IMSLD] [float] NULL,
	[IMANPL] [float] NULL,
	[IMMPST] [nchar](1) NULL,
	[IMPCTM] [float] NULL,
	[IMMMPC] [float] NULL,
	[IMPTSC] [nchar](2) NULL,
	[IMSNS] [nchar](1) NULL,
	[IMLTLV] [float] NULL,
	[IMLTMF] [float] NULL,
	[IMLTCM] [float] NULL,
	[IMOPC] [nchar](1) NULL,
	[IMOPV] [float] NULL,
	[IMACQ] [float] NULL,
	[IMMLQ] [float] NULL,
	[IMLTPU] [float] NULL,
	[IMMPSP] [nchar](1) NULL,
	[IMMRPP] [nchar](1) NULL,
	[IMITC] [nchar](1) NULL,
	[IMORDW] [nchar](1) NULL,
	[IMMTF1] [float] NULL,
	[IMMTF2] [float] NULL,
	[IMMTF3] [float] NULL,
	[IMMTF4] [float] NULL,
	[IMMTF5] [float] NULL,
	[IMEXPD] [float] NULL,
	[IMDEFD] [float] NULL,
	[IMSFLT] [float] NULL,
	[IMMAKE] [nchar](1) NULL,
	[IMCOBY] [nchar](1) NULL,
	[IMLLX] [float] NULL,
	[IMCMGL] [nchar](1) NULL,
	[IMCOMH] [float] NULL,
	[IMURCD] [nchar](2) NULL,
	[IMURDT] [numeric](18, 0) NULL,
	[IMURAT] [float] NULL,
	[IMURAB] [float] NULL,
	[IMURRF] [nchar](15) NULL,
	[IMUSER] [nchar](10) NULL,
	[IMPID] [nchar](10) NULL,
	[IMJOBN] [nchar](10) NULL,
	[IMUPMJ] [numeric](18, 0) NULL,
	[IMTDAY] [float] NULL,
	[IMUPCN] [nchar](13) NULL,
	[IMSCC0] [nchar](14) NULL,
	[IMUMUP] [nchar](2) NULL,
	[IMUMDF] [nchar](2) NULL,
	[IMUMS0] [nchar](2) NULL,
	[IMUMS1] [nchar](2) NULL,
	[IMUMS2] [nchar](2) NULL,
	[IMUMS3] [nchar](2) NULL,
	[IMUMS4] [nchar](2) NULL,
	[IMUMS5] [nchar](2) NULL,
	[IMUMS6] [nchar](2) NULL,
	[IMUMS7] [nchar](2) NULL,
	[IMUMS8] [nchar](2) NULL,
	[IMPOC] [nchar](1) NULL,
	[IMAVRT] [float] NULL,
	[IMEQTY] [nchar](5) NULL,
	[IMWTRQ] [nchar](1) NULL,
	[IMTMPL] [nchar](20) NULL,
	[IMSEG1] [nchar](10) NULL,
	[IMSEG2] [nchar](10) NULL,
	[IMSEG3] [nchar](10) NULL,
	[IMSEG4] [nchar](10) NULL,
	[IMSEG5] [nchar](10) NULL,
	[IMSEG6] [nchar](10) NULL,
	[IMSEG7] [nchar](10) NULL,
	[IMSEG8] [nchar](10) NULL,
	[IMSEG9] [nchar](10) NULL,
	[IMSEG0] [nchar](10) NULL,
	[IMMIC] [nchar](1) NULL,
	[IMAING] [nchar](1) NULL,
	[IMBBDD] [float] NULL,
	[IMCMDM] [nchar](1) NULL,
	[IMLECM] [nchar](1) NULL,
	[IMLEDD] [float] NULL,
	[IMPEFD] [float] NULL,
	[IMSBDD] [float] NULL,
	[IMU1DD] [float] NULL,
	[IMU2DD] [float] NULL,
	[IMU3DD] [float] NULL,
	[IMU4DD] [float] NULL,
	[IMU5DD] [float] NULL,
	[IMDLTL] [float] NULL,
	[IMDPPO] [nchar](1) NULL,
	[IMDUAL] [nchar](1) NULL,
	[IMXDCK] [nchar](1) NULL,
	[IMLAF] [nchar](1) NULL,
	[IMLTFM] [nchar](1) NULL,
	[IMRWLA] [nchar](1) NULL,
	[IMLNPA] [nchar](1) NULL,
	[IMLOTC] [nchar](3) NULL,
	[IMAPSC] [nchar](1) NULL,
	[IMAUOM] [nchar](9) NULL,
	[IMCONB] [nchar](1) NULL,
	[IMGCMP] [nchar](1) NULL,
	[IMPRI1] [float] NULL,
	[IMPRI2] [float] NULL,
	[IMASHL] [nchar](1) NULL,
	[IMVMINV] [nchar](1) NULL,
	[IMCMETH] [nchar](1) NULL,
	[IMEXPI] [nchar](1) NULL,
	[IMOPTH] [float] NULL,
	[IMCUTH] [float] NULL,
	[IMUMTH] [nchar](3) NULL,
	[IMLMFG] [nchar](1) NULL,
	[IMLINE] [nchar](12) NULL,
	[IMDFTPCT] [float] NULL,
	[IMKBIT] [nchar](1) NULL,
	[IMDFENDITM] [nchar](1) NULL,
	[IMKANEXLL] [nchar](1) NULL,
	[IMSCPSELL] [nchar](1) NULL,
 CONSTRAINT [F4101_PK] PRIMARY KEY CLUSTERED 
(
	[IMITM] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F4311]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F4311](
	[PDKCOO] [nchar](5) NOT NULL,
	[PDDOCO] [float] NOT NULL,
	[PDDCTO] [nchar](2) NOT NULL,
	[PDLNID] [float] NOT NULL,
	[PDSFXO] [nchar](3) NOT NULL,
	[PDMCU] [nchar](12) NULL,
	[PDSHAN] [float] NULL,
	[PDPDDJ] [numeric](18, 0) NULL,
	[PDPSDJ] [numeric](18, 0) NULL,
	[PDLITM] [nchar](25) NULL,
	[PDNXTR] [nchar](3) NULL,
	[PDUORG] [float] NULL,
	[PDUOM] [nchar](2) NULL,
 CONSTRAINT [F4311_PK] PRIMARY KEY CLUSTERED 
(
	[PDDOCO] ASC,
	[PDDCTO] ASC,
	[PDKCOO] ASC,
	[PDSFXO] ASC,
	[PDLNID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F0101]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F0101](
	[ABAN8] [float] NOT NULL,
	[ABALKY] [nchar](20) NULL,
	[ABTAX] [nchar](20) NULL,
	[ABALPH] [nchar](40) NULL,
	[ABDC] [nchar](40) NULL,
	[ABMCU] [nchar](12) NULL,
	[ABSIC] [nchar](10) NULL,
	[ABLNGP] [nchar](2) NULL,
	[ABAT1] [nchar](3) NULL,
	[ABCM] [nchar](2) NULL,
	[ABTAXC] [nchar](1) NULL,
	[ABAT2] [nchar](1) NULL,
	[ABAT3] [nchar](1) NULL,
	[ABAT4] [nchar](1) NULL,
	[ABAT5] [nchar](1) NULL,
	[ABATP] [nchar](1) NULL,
	[ABATR] [nchar](1) NULL,
	[ABATPR] [nchar](1) NULL,
	[ABAB3] [nchar](1) NULL,
	[ABATE] [nchar](1) NULL,
	[ABSBLI] [nchar](1) NULL,
	[ABEFTB] [numeric](18, 0) NULL,
	[ABAN81] [float] NULL,
	[ABAN82] [float] NULL,
	[ABAN83] [float] NULL,
	[ABAN84] [float] NULL,
	[ABAN86] [float] NULL,
	[ABAN85] [float] NULL,
	[ABAC01] [nchar](3) NULL,
	[ABAC02] [nchar](3) NULL,
	[ABAC03] [nchar](3) NULL,
	[ABAC04] [nchar](3) NULL,
	[ABAC05] [nchar](3) NULL,
	[ABAC06] [nchar](3) NULL,
	[ABAC07] [nchar](3) NULL,
	[ABAC08] [nchar](3) NULL,
	[ABAC09] [nchar](3) NULL,
	[ABAC10] [nchar](3) NULL,
	[ABAC11] [nchar](3) NULL,
	[ABAC12] [nchar](3) NULL,
	[ABAC13] [nchar](3) NULL,
	[ABAC14] [nchar](3) NULL,
	[ABAC15] [nchar](3) NULL,
	[ABAC16] [nchar](3) NULL,
	[ABAC17] [nchar](3) NULL,
	[ABAC18] [nchar](3) NULL,
	[ABAC19] [nchar](3) NULL,
	[ABAC20] [nchar](3) NULL,
	[ABAC21] [nchar](3) NULL,
	[ABAC22] [nchar](3) NULL,
	[ABAC23] [nchar](3) NULL,
	[ABAC24] [nchar](3) NULL,
	[ABAC25] [nchar](3) NULL,
	[ABAC26] [nchar](3) NULL,
	[ABAC27] [nchar](3) NULL,
	[ABAC28] [nchar](3) NULL,
	[ABAC29] [nchar](3) NULL,
	[ABAC30] [nchar](3) NULL,
	[ABGLBA] [nchar](8) NULL,
	[ABPTI] [float] NULL,
	[ABPDI] [numeric](18, 0) NULL,
	[ABMSGA] [nchar](1) NULL,
	[ABRMK] [nchar](30) NULL,
	[ABTXCT] [nchar](20) NULL,
	[ABTX2] [nchar](20) NULL,
	[ABALP1] [nchar](40) NULL,
	[ABURCD] [nchar](2) NULL,
	[ABURDT] [numeric](18, 0) NULL,
	[ABURAT] [float] NULL,
	[ABURAB] [float] NULL,
	[ABURRF] [nchar](15) NULL,
	[ABUSER] [nchar](10) NULL,
	[ABPID] [nchar](10) NULL,
	[ABUPMJ] [numeric](18, 0) NULL,
	[ABJOBN] [nchar](10) NULL,
	[ABUPMT] [float] NULL,
	[ABPRGF] [nchar](1) NULL,
	[ABSCCLTP] [nchar](2) NULL,
	[ABTICKER] [nchar](10) NULL,
	[ABEXCHG] [nchar](10) NULL,
	[ABDUNS] [nchar](13) NULL,
	[ABCLASS01] [nchar](3) NULL,
	[ABCLASS02] [nchar](3) NULL,
	[ABCLASS03] [nchar](3) NULL,
	[ABCLASS04] [nchar](3) NULL,
	[ABCLASS05] [nchar](3) NULL,
	[ABNOE] [float] NULL,
	[ABGROWTHR] [float] NULL,
	[ABYEARSTAR] [nchar](15) NULL,
	[ABAEMPGP] [nchar](5) NULL,
	[ABACTIN] [nchar](1) NULL,
	[ABREVRNG] [nchar](5) NULL,
	[ABSYNCS] [float] NULL,
	[ABPERRS] [float] NULL,
	[ABCAAD] [float] NULL,
 CONSTRAINT [F0101_PK] PRIMARY KEY CLUSTERED 
(
	[ABAN8] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F4211]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F4211](
	[SDKCOO] [nchar](5) NOT NULL,
	[SDDOCO] [float] NOT NULL,
	[SDDCTO] [nchar](2) NOT NULL,
	[SDLNID] [float] NOT NULL,
	[SDMCU] [nchar](12) NULL,
	[SDSHAN] [float] NULL,
	[SDPDDJ] [numeric](18, 0) NULL,
	[SDRSDJ] [numeric](18, 0) NULL,
	[SDLITM] [nchar](25) NULL,
	[SDNXTR] [nchar](3) NULL,
	[SDUORG] [float] NULL,
	[SDUOM] [nchar](2) NULL,
 CONSTRAINT [F4211_PK] PRIMARY KEY CLUSTERED 
(
	[SDDOCO] ASC,
	[SDDCTO] ASC,
	[SDKCOO] ASC,
	[SDLNID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F0005]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[F0005](
	[DRSY] [nchar](4) NOT NULL,
	[DRRT] [nchar](2) NOT NULL,
	[DRKY] [nchar](10) NOT NULL,
	[DRDL01] [nchar](30) NULL,
	[DRDL02] [nchar](30) NULL,
	[DRSPHD] [nchar](10) NULL,
	[DRUDCO] [nchar](1) NULL,
	[DRHRDC] [nchar](1) NULL,
	[DRUPMJ] [numeric](18, 0) NULL,
	[DRUPMT] [float] NULL,
 CONSTRAINT [F0005_PK] PRIMARY KEY CLUSTERED 
(
	[DRSY] ASC,
	[DRRT] ASC,
	[DRKY] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.xml_schema_collections c, sys.schemas s WHERE c.schema_id = s.schema_id AND (quotename(s.name) + '.' + quotename(c.name)) = N'[dbo].[TxOriginalSchema]')
CREATE XML SCHEMA COLLECTION [dbo].[TxOriginalSchema] AS N'<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified"><xsd:element name="ICSTransaction"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence><xsd:element name="DeviceID" type="xsd:string" minOccurs="0" /><xsd:element name="User" type="xsd:string" minOccurs="0" /><xsd:element name="SessionID" type="xsd:string" minOccurs="0" /><xsd:element name="ERPUser" type="xsd:string" minOccurs="0" /><xsd:element name="ERPPwd" type="xsd:string" minOccurs="0" /><xsd:element name="Start" type="xsd:string" minOccurs="0" /><xsd:element name="Sent" type="xsd:string" minOccurs="0" /><xsd:element name="Update" type="xsd:string" minOccurs="0" /><xsd:element name="CallerWaiting" type="xsd:string" minOccurs="0" /><xsd:element name="extras" type="xsd:string" minOccurs="0" /><xsd:element name="Transactions" minOccurs="0" maxOccurs="unbounded"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence><xsd:element name="Transaction" minOccurs="0" maxOccurs="unbounded"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence><xsd:element name="Tx" minOccurs="0" maxOccurs="unbounded"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence><xsd:element name="Fields" minOccurs="0" maxOccurs="unbounded"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence><xsd:element name="Field" minOccurs="0" maxOccurs="unbounded"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:sequence /><xsd:attribute name="name" type="xsd:string" /><xsd:attribute name="value" type="xsd:string" /></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:sequence></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:sequence><xsd:attribute name="Sequence" type="xsd:string" /><xsd:attribute name="name" type="xsd:string" /><xsd:attribute name="Boundary" type="xsd:string" /></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:sequence><xsd:attribute name="Boundary" type="xsd:string" /><xsd:attribute name="erp" type="xsd:string" /></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:sequence></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:sequence><xsd:attribute name="workflowid" type="xsd:string" /><xsd:attribute name="version" type="xsd:string" /><xsd:attribute name="flags" type="xsd:string" /><xsd:attribute name="environment" type="xsd:string" /><xsd:attribute name="txid" type="xsd:string" /><xsd:attribute name="txstatus" type="xsd:string" /></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element><xsd:element name="NewDataSet"><xsd:complexType><xsd:complexContent><xsd:restriction base="xsd:anyType"><xsd:choice maxOccurs="unbounded"><xsd:element ref="ICSTransaction" /></xsd:choice></xsd:restriction></xsd:complexContent></xsd:complexType></xsd:element></xsd:schema>'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RFS_Environment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RFS_Environment](
	[EnvironmentID] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RFS_Environment] PRIMARY KEY CLUSTERED 
(
	[EnvironmentID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RFS_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RFS_Users](
	[UserID] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RFS_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RFS_TxHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RFS_TxHeader](
	[TxID] [int] NOT NULL,
	[EnvironmentID] [nvarchar](20) NOT NULL,
	[TxStatus] [nchar](1) NOT NULL,
	[WorkFlowID] [nvarchar](50) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
	[TxOriginal] [xml](CONTENT [dbo].[TxOriginalSchema]) NOT NULL,
 CONSTRAINT [PK_RFS_TxHeader_1] PRIMARY KEY CLUSTERED 
(
	[TxID] ASC,
	[EnvironmentID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RFS_TxHeader_RFS_Environment]') AND parent_object_id = OBJECT_ID(N'[dbo].[RFS_TxHeader]'))
ALTER TABLE [dbo].[RFS_TxHeader]  WITH CHECK ADD  CONSTRAINT [FK_RFS_TxHeader_RFS_Environment] FOREIGN KEY([EnvironmentID])
REFERENCES [dbo].[RFS_Environment] ([EnvironmentID])
