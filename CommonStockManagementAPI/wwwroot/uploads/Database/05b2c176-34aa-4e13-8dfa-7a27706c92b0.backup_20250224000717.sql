-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2025-02-24 00:07:17
-- --------------------------------------
-- Server version 9.0.1 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of TblAuditTrails
-- 

DROP TABLE IF EXISTS `TblAuditTrails`;
CREATE TABLE IF NOT EXISTS `TblAuditTrails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EntityName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Action` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Details` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Timestamp` datetime(6) NOT NULL,
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblAuditTrails
-- 

/*!40000 ALTER TABLE `TblAuditTrails` DISABLE KEYS */;
INSERT INTO `TblAuditTrails`(`Id`,`EntityName`,`Action`,`Details`,`Timestamp`,`UserId`) VALUES(1,'EntityName','Update','Entity with ID f9f00bea-f20c-11ef-bfd5-0242ac110002 was updated.','2025-02-23 23:08:53.121560','UserId'),(2,'EntityName','Update','Entity with ID 10554a38-873a-43f4-9228-1f2ee136cd1b was updated.','2025-02-23 23:09:39.667959','UserId'),(3,'EntityName','Update','Entity with ID 10554a38-873a-43f4-9228-1f2ee136cd1b was updated.','2025-02-23 23:11:39.298722','UserId'),(4,'EntityName','Update','Entity with ID 10554a38-873a-43f4-9228-1f2ee136cd1b was updated.','2025-02-23 23:14:17.706770','UserId'),(5,'EntityName','Update','Entity with ID 10554a38-873a-43f4-9228-1f2ee136cd1b was updated.','2025-02-23 23:17:55.123323','UserId'),(6,'TblCompanyDetails','Insert','Insert Kms Mobile Kosgma  Name.','2025-02-23 23:23:19.693933','10554a38-873a-43f4-9228-1f2ee136cd1b'),(7,'TblStock_Locations','Insert','Insert Kosgma  Name.','2025-02-23 23:23:43.617901','10554a38-873a-43f4-9228-1f2ee136cd1b');
/*!40000 ALTER TABLE `TblAuditTrails` ENABLE KEYS */;

-- 
-- Definition of TblClients
-- 

DROP TABLE IF EXISTS `TblClients`;
CREATE TABLE IF NOT EXISTS `TblClients` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Area` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Nic` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Mobile` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Tel` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RegistrationDate` datetime(6) DEFAULT NULL,
  `Isdelete` tinyint(1) NOT NULL,
  `ImageURl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Dr` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Cr` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblClients
-- 

/*!40000 ALTER TABLE `TblClients` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblClients` ENABLE KEYS */;

-- 
-- Definition of TblCompanyDetails
-- 

DROP TABLE IF EXISTS `TblCompanyDetails`;
CREATE TABLE IF NOT EXISTS `TblCompanyDetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CompanyName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TelPhone1` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TelPhone2` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Isdelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblCompanyDetails
-- 

/*!40000 ALTER TABLE `TblCompanyDetails` DISABLE KEYS */;
INSERT INTO `TblCompanyDetails`(`Id`,`CompanyName`,`Address`,`TelPhone1`,`TelPhone2`,`Isdelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Kms Mobile Kosgma','Awissawella','0775549788','0775549788',0,'10554a38-873a-43f4-9228-1f2ee136cd1b',NULL,'2025-02-23 23:23:19.636813',NULL);
/*!40000 ALTER TABLE `TblCompanyDetails` ENABLE KEYS */;

-- 
-- Definition of TblDatabaseBackupHistory
-- 

DROP TABLE IF EXISTS `TblDatabaseBackupHistory`;
CREATE TABLE IF NOT EXISTS `TblDatabaseBackupHistory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `DateTime` datetime(6) NOT NULL,
  `DatabaseName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TagDiscription` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblDatabaseBackupHistory
-- 

/*!40000 ALTER TABLE `TblDatabaseBackupHistory` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblDatabaseBackupHistory` ENABLE KEYS */;

-- 
-- Definition of TblGINBodyTemp
-- 

DROP TABLE IF EXISTS `TblGINBodyTemp`;
CREATE TABLE IF NOT EXISTS `TblGINBodyTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemID` int NOT NULL,
  `GINNo` int NOT NULL,
  `GINBodyNo` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGINBodyTemp
-- 

/*!40000 ALTER TABLE `TblGINBodyTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGINBodyTemp` ENABLE KEYS */;

-- 
-- Definition of TblGRNBodyTemp
-- 

DROP TABLE IF EXISTS `TblGRNBodyTemp`;
CREATE TABLE IF NOT EXISTS `TblGRNBodyTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemID` int NOT NULL,
  `GRnNo` int NOT NULL,
  `GRnBodyNo` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Batch` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNBodyTemp
-- 

/*!40000 ALTER TABLE `TblGRNBodyTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGRNBodyTemp` ENABLE KEYS */;

-- 
-- Definition of TblItemBrandNames
-- 

DROP TABLE IF EXISTS `TblItemBrandNames`;
CREATE TABLE IF NOT EXISTS `TblItemBrandNames` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemBrandNames
-- 

/*!40000 ALTER TABLE `TblItemBrandNames` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemBrandNames` ENABLE KEYS */;

-- 
-- Definition of TblItemCategories
-- 

DROP TABLE IF EXISTS `TblItemCategories`;
CREATE TABLE IF NOT EXISTS `TblItemCategories` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemCategories
-- 

/*!40000 ALTER TABLE `TblItemCategories` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemCategories` ENABLE KEYS */;

-- 
-- Definition of TblItemModelTypes
-- 

DROP TABLE IF EXISTS `TblItemModelTypes`;
CREATE TABLE IF NOT EXISTS `TblItemModelTypes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemModelTypes
-- 

/*!40000 ALTER TABLE `TblItemModelTypes` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemModelTypes` ENABLE KEYS */;

-- 
-- Definition of TblItemRentalDetailsTemp
-- 

DROP TABLE IF EXISTS `TblItemRentalDetailsTemp`;
CREATE TABLE IF NOT EXISTS `TblItemRentalDetailsTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RentalItemBodyId` int DEFAULT NULL,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemID` int NOT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `DayCost` decimal(18,2) DEFAULT NULL,
  `TotalCost` decimal(18,2) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemRentalDetailsTemp
-- 

/*!40000 ALTER TABLE `TblItemRentalDetailsTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemRentalDetailsTemp` ENABLE KEYS */;

-- 
-- Definition of TblItemUnits
-- 

DROP TABLE IF EXISTS `TblItemUnits`;
CREATE TABLE IF NOT EXISTS `TblItemUnits` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemUnits
-- 

/*!40000 ALTER TABLE `TblItemUnits` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemUnits` ENABLE KEYS */;

-- 
-- Definition of TblPOSBodyTemp
-- 

DROP TABLE IF EXISTS `TblPOSBodyTemp`;
CREATE TABLE IF NOT EXISTS `TblPOSBodyTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSBodyTemp
-- 

/*!40000 ALTER TABLE `TblPOSBodyTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSBodyTemp` ENABLE KEYS */;

-- 
-- Definition of TblPOSReturnBodyTemp
-- 

DROP TABLE IF EXISTS `TblPOSReturnBodyTemp`;
CREATE TABLE IF NOT EXISTS `TblPOSReturnBodyTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemID` int NOT NULL,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `POSBodyKeyNo` int NOT NULL,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSReturnBodyTemp
-- 

/*!40000 ALTER TABLE `TblPOSReturnBodyTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSReturnBodyTemp` ENABLE KEYS */;

-- 
-- Definition of TblStockReturnNoteBodyTemp
-- 

DROP TABLE IF EXISTS `TblStockReturnNoteBodyTemp`;
CREATE TABLE IF NOT EXISTS `TblStockReturnNoteBodyTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemID` int NOT NULL,
  `SRnNo` int NOT NULL,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SRnBodyNo` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Batch` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStockReturnNoteBodyTemp
-- 

/*!40000 ALTER TABLE `TblStockReturnNoteBodyTemp` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblStockReturnNoteBodyTemp` ENABLE KEYS */;

-- 
-- Definition of TblStock_Location
-- 

DROP TABLE IF EXISTS `TblStock_Location`;
CREATE TABLE IF NOT EXISTS `TblStock_Location` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStock_Location
-- 

/*!40000 ALTER TABLE `TblStock_Location` DISABLE KEYS */;
INSERT INTO `TblStock_Location`(`ID`,`Name`,`Type`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Kosgma','Main',0,'10554a38-873a-43f4-9228-1f2ee136cd1b',NULL,'2025-02-23 23:23:43.553052',NULL);
/*!40000 ALTER TABLE `TblStock_Location` ENABLE KEYS */;

-- 
-- Definition of TblGINBody
-- 

DROP TABLE IF EXISTS `TblGINBody`;
CREATE TABLE IF NOT EXISTS `TblGINBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GINId` int DEFAULT NULL,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblGINBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblGINBody_GINId` (`GINId`),
  CONSTRAINT `FK_TblGINBody_TblGINHead_GINId` FOREIGN KEY (`GINId`) REFERENCES `TblGINHead` (`GINId`),
  CONSTRAINT `FK_TblGINBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGINBody
-- 

/*!40000 ALTER TABLE `TblGINBody` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGINBody` ENABLE KEYS */;

-- 
-- Definition of TblGINHead
-- 

DROP TABLE IF EXISTS `TblGINHead`;
CREATE TABLE IF NOT EXISTS `TblGINHead` (
  `GINId` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Created` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Total` double DEFAULT NULL,
  `Discount` double DEFAULT NULL,
  `Gross` double DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`GINId`),
  KEY `IX_TblGINHead_FKLocationId` (`FKLocationId`),
  CONSTRAINT `FK_TblGINHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGINHead
-- 

/*!40000 ALTER TABLE `TblGINHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGINHead` ENABLE KEYS */;

-- 
-- Definition of TblGRNBody
-- 

DROP TABLE IF EXISTS `TblGRNBody`;
CREATE TABLE IF NOT EXISTS `TblGRNBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Grnno` int DEFAULT NULL,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblGRNBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblGRNBody_Grnno` (`Grnno`),
  CONSTRAINT `FK_TblGRNBody_TblGRNHead_Grnno` FOREIGN KEY (`Grnno`) REFERENCES `TblGRNHead` (`Id`),
  CONSTRAINT `FK_TblGRNBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNBody
-- 

/*!40000 ALTER TABLE `TblGRNBody` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGRNBody` ENABLE KEYS */;

-- 
-- Definition of TblItemRentalDetails
-- 

DROP TABLE IF EXISTS `TblItemRentalDetails`;
CREATE TABLE IF NOT EXISTS `TblItemRentalDetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FKHeadId` int DEFAULT NULL,
  `ItemID` int NOT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `DayCost` decimal(18,2) DEFAULT NULL,
  `TotalCost` decimal(18,2) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblItemRentalDetails_FKHeadId` (`FKHeadId`),
  KEY `IX_TblItemRentalDetails_FKLocationId` (`FKLocationId`),
  CONSTRAINT `FK_TblItemRentalDetails_TblItemRentalHead_FKHeadId` FOREIGN KEY (`FKHeadId`) REFERENCES `TblItemRentalHead` (`Id`),
  CONSTRAINT `FK_TblItemRentalDetails_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemRentalDetails
-- 

/*!40000 ALTER TABLE `TblItemRentalDetails` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemRentalDetails` ENABLE KEYS */;

-- 
-- Definition of TblItemRentalHead
-- 

DROP TABLE IF EXISTS `TblItemRentalHead`;
CREATE TABLE IF NOT EXISTS `TblItemRentalHead` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SysDate` datetime(6) DEFAULT NULL,
  `FKClientId` int DEFAULT NULL,
  `RentalStartDate` datetime(6) DEFAULT NULL,
  `RentalEndDate` datetime(6) DEFAULT NULL,
  `Amount` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Gross` decimal(18,2) DEFAULT NULL,
  `AdvancePay` decimal(18,2) DEFAULT NULL,
  `Balance` decimal(18,2) DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsSettle` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblItemRentalHead_FKClientId` (`FKClientId`),
  KEY `IX_TblItemRentalHead_FKLocationId` (`FKLocationId`),
  CONSTRAINT `FK_TblItemRentalHead_TblClients_FKClientId` FOREIGN KEY (`FKClientId`) REFERENCES `TblClients` (`ID`),
  CONSTRAINT `FK_TblItemRentalHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemRentalHead
-- 

/*!40000 ALTER TABLE `TblItemRentalHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblItemRentalHead` ENABLE KEYS */;

-- 
-- Definition of TblPOSBody
-- 

DROP TABLE IF EXISTS `TblPOSBody`;
CREATE TABLE IF NOT EXISTS `TblPOSBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `POSNO` int DEFAULT NULL,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblPOSBody_POSNO` (`POSNO`),
  CONSTRAINT `FK_TblPOSBody_TblPOSHead_POSNO` FOREIGN KEY (`POSNO`) REFERENCES `TblPOSHead` (`Id`),
  CONSTRAINT `FK_TblPOSBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSBody
-- 

/*!40000 ALTER TABLE `TblPOSBody` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSBody` ENABLE KEYS */;

-- 
-- Definition of TblPOSHead
-- 

DROP TABLE IF EXISTS `TblPOSHead`;
CREATE TABLE IF NOT EXISTS `TblPOSHead` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Created` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FKClientId` int DEFAULT NULL,
  `Total` double DEFAULT NULL,
  `Discount` double DEFAULT NULL,
  `Gross` double DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSHead_FKClientId` (`FKClientId`),
  KEY `IX_TblPOSHead_FKLocationId` (`FKLocationId`),
  CONSTRAINT `FK_TblPOSHead_TblClients_FKClientId` FOREIGN KEY (`FKClientId`) REFERENCES `TblClients` (`ID`),
  CONSTRAINT `FK_TblPOSHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSHead
-- 

/*!40000 ALTER TABLE `TblPOSHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSHead` ENABLE KEYS */;

-- 
-- Definition of TblPOSReturnBody
-- 

DROP TABLE IF EXISTS `TblPOSReturnBody`;
CREATE TABLE IF NOT EXISTS `TblPOSReturnBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `POSReturnNO` int DEFAULT NULL,
  `POSBodyKeyNo` int NOT NULL,
  `POSInvoiceNO` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSReturnBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblPOSReturnBody_POSReturnNO` (`POSReturnNO`),
  CONSTRAINT `FK_TblPOSReturnBody_TblPOSReturnHead_POSReturnNO` FOREIGN KEY (`POSReturnNO`) REFERENCES `TblPOSReturnHead` (`Id`),
  CONSTRAINT `FK_TblPOSReturnBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSReturnBody
-- 

/*!40000 ALTER TABLE `TblPOSReturnBody` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSReturnBody` ENABLE KEYS */;

-- 
-- Definition of TblPOSReturnHead
-- 

DROP TABLE IF EXISTS `TblPOSReturnHead`;
CREATE TABLE IF NOT EXISTS `TblPOSReturnHead` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `POSInvoiceNO` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Created` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FKClientId` int DEFAULT NULL,
  `Total` double DEFAULT NULL,
  `Discount` double DEFAULT NULL,
  `Gross` double DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSReturnHead_FKClientId` (`FKClientId`),
  KEY `IX_TblPOSReturnHead_FKLocationId` (`FKLocationId`),
  CONSTRAINT `FK_TblPOSReturnHead_TblClients_FKClientId` FOREIGN KEY (`FKClientId`) REFERENCES `TblClients` (`ID`),
  CONSTRAINT `FK_TblPOSReturnHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSReturnHead
-- 

/*!40000 ALTER TABLE `TblPOSReturnHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPOSReturnHead` ENABLE KEYS */;

-- 
-- Definition of TblStock_Main
-- 

DROP TABLE IF EXISTS `TblStock_Main`;
CREATE TABLE IF NOT EXISTS `TblStock_Main` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemDescription` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `MaxLevel` decimal(18,2) DEFAULT NULL,
  `MinLevel` decimal(18,2) DEFAULT NULL,
  `ReorderLevel` decimal(18,2) DEFAULT NULL,
  `FkUnitId` int NOT NULL,
  `FkCategoryId` int NOT NULL,
  `LastPurchasePrice` decimal(18,2) DEFAULT NULL,
  `SellingPrice` decimal(18,2) DEFAULT NULL,
  `FkBrandId` int DEFAULT NULL,
  `FkModelTypeId` int DEFAULT NULL,
  `ImageUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_TblStock_Main_FkBrandId` (`FkBrandId`),
  KEY `IX_TblStock_Main_FkCategoryId` (`FkCategoryId`),
  KEY `IX_TblStock_Main_FkModelTypeId` (`FkModelTypeId`),
  KEY `IX_TblStock_Main_FkUnitId` (`FkUnitId`),
  CONSTRAINT `FK_TblStock_Main_TblItemBrandNames_FkBrandId` FOREIGN KEY (`FkBrandId`) REFERENCES `TblItemBrandNames` (`Id`),
  CONSTRAINT `FK_TblStock_Main_TblItemCategories_FkCategoryId` FOREIGN KEY (`FkCategoryId`) REFERENCES `TblItemCategories` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TblStock_Main_TblItemModelTypes_FkModelTypeId` FOREIGN KEY (`FkModelTypeId`) REFERENCES `TblItemModelTypes` (`Id`),
  CONSTRAINT `FK_TblStock_Main_TblItemUnits_FkUnitId` FOREIGN KEY (`FkUnitId`) REFERENCES `TblItemUnits` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStock_Main
-- 

/*!40000 ALTER TABLE `TblStock_Main` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblStock_Main` ENABLE KEYS */;

-- 
-- Definition of TblPriceBackups
-- 

DROP TABLE IF EXISTS `TblPriceBackups`;
CREATE TABLE IF NOT EXISTS `TblPriceBackups` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FkItemId` int NOT NULL,
  `FkCategoryId` int NOT NULL,
  `LastPurchasePrice` decimal(18,2) DEFAULT NULL,
  `LastSellingPrice` decimal(18,2) DEFAULT NULL,
  `NewPurchasePrice` decimal(18,2) DEFAULT NULL,
  `NewSellingPrice` decimal(18,2) DEFAULT NULL,
  `FkBrandId` int NOT NULL,
  `PriceChangeBackupDate` datetime(6) NOT NULL,
  `PercentageLastPurchasePrice` decimal(18,2) DEFAULT NULL,
  `PercentageSellingPrice` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPriceBackups_FkBrandId` (`FkBrandId`),
  KEY `IX_TblPriceBackups_FkCategoryId` (`FkCategoryId`),
  KEY `IX_TblPriceBackups_FkItemId` (`FkItemId`),
  CONSTRAINT `FK_TblPriceBackups_TblItemBrandNames_FkBrandId` FOREIGN KEY (`FkBrandId`) REFERENCES `TblItemBrandNames` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TblPriceBackups_TblItemCategories_FkCategoryId` FOREIGN KEY (`FkCategoryId`) REFERENCES `TblItemCategories` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TblPriceBackups_TblStock_Main_FkItemId` FOREIGN KEY (`FkItemId`) REFERENCES `TblStock_Main` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPriceBackups
-- 

/*!40000 ALTER TABLE `TblPriceBackups` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblPriceBackups` ENABLE KEYS */;

-- 
-- Definition of TblSupplier
-- 

DROP TABLE IF EXISTS `TblSupplier`;
CREATE TABLE IF NOT EXISTS `TblSupplier` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Company` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Contact` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Tel` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Fax` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Mobile` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreditorLedger` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `AdvanceCreditorLedger` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LedgerCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `ImageURl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `TblSupplierID` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_TblSupplier_TblSupplierID` (`TblSupplierID`),
  CONSTRAINT `FK_TblSupplier_TblSupplier_TblSupplierID` FOREIGN KEY (`TblSupplierID`) REFERENCES `TblSupplier` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblSupplier
-- 

/*!40000 ALTER TABLE `TblSupplier` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblSupplier` ENABLE KEYS */;

-- 
-- Definition of TblFixedAssets
-- 

DROP TABLE IF EXISTS `TblFixedAssets`;
CREATE TABLE IF NOT EXISTS `TblFixedAssets` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Category` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Model` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Make` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PerCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Serial` int NOT NULL,
  `GRNNo` int DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FKSupplier_ID` int DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  `PurchaseDate` datetime(6) DEFAULT NULL,
  `PurchasePrice` double DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` int DEFAULT NULL,
  `Cost` double DEFAULT NULL,
  `Warrent_ex` datetime(6) DEFAULT NULL,
  `Naration` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`ID`),
  KEY `IX_TblFixedAssets_FKLocationId` (`FKLocationId`),
  KEY `IX_TblFixedAssets_FKSupplier_ID` (`FKSupplier_ID`),
  CONSTRAINT `FK_TblFixedAssets_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`),
  CONSTRAINT `FK_TblFixedAssets_TblSupplier_FKSupplier_ID` FOREIGN KEY (`FKSupplier_ID`) REFERENCES `TblSupplier` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblFixedAssets
-- 

/*!40000 ALTER TABLE `TblFixedAssets` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblFixedAssets` ENABLE KEYS */;

-- 
-- Definition of TblGRNHead
-- 

DROP TABLE IF EXISTS `TblGRNHead`;
CREATE TABLE IF NOT EXISTS `TblGRNHead` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Pono` int NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `GRNType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Created` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FKSupplier_ID` int DEFAULT NULL,
  `Total` double DEFAULT NULL,
  `Discount` double DEFAULT NULL,
  `Gross` double DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblGRNHead_FKLocationId` (`FKLocationId`),
  KEY `IX_TblGRNHead_FKSupplier_ID` (`FKSupplier_ID`),
  CONSTRAINT `FK_TblGRNHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`),
  CONSTRAINT `FK_TblGRNHead_TblSupplier_FKSupplier_ID` FOREIGN KEY (`FKSupplier_ID`) REFERENCES `TblSupplier` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNHead
-- 

/*!40000 ALTER TABLE `TblGRNHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblGRNHead` ENABLE KEYS */;

-- 
-- Definition of TblStockReturnNoteHead
-- 

DROP TABLE IF EXISTS `TblStockReturnNoteHead`;
CREATE TABLE IF NOT EXISTS `TblStockReturnNoteHead` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SRNType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefInv` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Created` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FKSupplier_ID` int DEFAULT NULL,
  `Total` double DEFAULT NULL,
  `Discount` double DEFAULT NULL,
  `Gross` double DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_TblStockReturnNoteHead_FKLocationId` (`FKLocationId`),
  KEY `IX_TblStockReturnNoteHead_FKSupplier_ID` (`FKSupplier_ID`),
  CONSTRAINT `FK_TblStockReturnNoteHead_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`),
  CONSTRAINT `FK_TblStockReturnNoteHead_TblSupplier_FKSupplier_ID` FOREIGN KEY (`FKSupplier_ID`) REFERENCES `TblSupplier` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStockReturnNoteHead
-- 

/*!40000 ALTER TABLE `TblStockReturnNoteHead` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblStockReturnNoteHead` ENABLE KEYS */;

-- 
-- Definition of TblStockReturnNoteBody
-- 

DROP TABLE IF EXISTS `TblStockReturnNoteBody`;
CREATE TABLE IF NOT EXISTS `TblStockReturnNoteBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SRNno` int DEFAULT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `ItemID` int NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TblStockReturnNoteBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblStockReturnNoteBody_SRNno` (`SRNno`),
  CONSTRAINT `FK_TblStockReturnNoteBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`),
  CONSTRAINT `FK_TblStockReturnNoteBody_TblStockReturnNoteHead_SRNno` FOREIGN KEY (`SRNno`) REFERENCES `TblStockReturnNoteHead` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStockReturnNoteBody
-- 

/*!40000 ALTER TABLE `TblStockReturnNoteBody` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblStockReturnNoteBody` ENABLE KEYS */;

-- 
-- Definition of TblSupplierDueReturnValue
-- 

DROP TABLE IF EXISTS `TblSupplierDueReturnValue`;
CREATE TABLE IF NOT EXISTS `TblSupplierDueReturnValue` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `FksupplierID` int NOT NULL,
  `Date` datetime(6) NOT NULL,
  `Ref_invoice` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` double NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_TblSupplierDueReturnValue_FksupplierID` (`FksupplierID`),
  CONSTRAINT `FK_TblSupplierDueReturnValue_TblSupplier_FksupplierID` FOREIGN KEY (`FksupplierID`) REFERENCES `TblSupplier` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblSupplierDueReturnValue
-- 

/*!40000 ALTER TABLE `TblSupplierDueReturnValue` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblSupplierDueReturnValue` ENABLE KEYS */;

-- 
-- Definition of TblSupplierPayment
-- 

DROP TABLE IF EXISTS `TblSupplierPayment`;
CREATE TABLE IF NOT EXISTS `TblSupplierPayment` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `FKSupplierID` int NOT NULL,
  `Date` datetime(6) NOT NULL,
  `Ref_invoive` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `GRNNo` int NOT NULL,
  `Total` double NOT NULL,
  `Pay` double NOT NULL,
  `Balance` double NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_TblSupplierPayment_FKSupplierID` (`FKSupplierID`),
  CONSTRAINT `FK_TblSupplierPayment_TblSupplier_FKSupplierID` FOREIGN KEY (`FKSupplierID`) REFERENCES `TblSupplier` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblSupplierPayment
-- 

/*!40000 ALTER TABLE `TblSupplierPayment` DISABLE KEYS */;

/*!40000 ALTER TABLE `TblSupplierPayment` ENABLE KEYS */;

-- 
-- Definition of TblUsers
-- 

DROP TABLE IF EXISTS `TblUsers`;
CREATE TABLE IF NOT EXISTS `TblUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastUpdatedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastUpdatedDateTime` datetime(6) NOT NULL,
  `ModifiedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ModifiedDateTime` datetime(6) NOT NULL,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Join_date` datetime(6) DEFAULT NULL,
  `Designation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `NIC_no` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastLoginDate` datetime(6) NOT NULL,
  `AcceptTerms` tinyint(1) NOT NULL,
  `Employee_Number` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ImageURl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblUsers
-- 

/*!40000 ALTER TABLE `TblUsers` DISABLE KEYS */;
INSERT INTO `TblUsers`(`Id`,`FirstName`,`LastName`,`LastUpdatedBy`,`LastUpdatedDateTime`,`ModifiedBy`,`ModifiedDateTime`,`Address`,`Join_date`,`Designation`,`NIC_no`,`LastLoginDate`,`AcceptTerms`,`Employee_Number`,`ImageURl`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) VALUES('10554a38-873a-43f4-9228-1f2ee136cd1b','Madushan','Chamara','System','2025-02-23 17:38:21.000000','System','2025-02-23 17:38:21.000000','adsdsadasdsdasdsa','2023-07-25 00:00:00.000000','sfdsfsdfdf','942740603v','2025-02-23 23:17:55.249933',1,NULL,'6bbb103d-bc64-406e-a53d-87017444b618.png','madushan','MADUSHAN','shanchamara@gmail.com','SHANCHAMARA@GMAIL.COM',1,'AQAAAAIAAYagAAAAEDqMOwxScgScXczOukAmcyjU1NusYR3YF6cUYipkP1sT8b3uz7hXcMV44zUgJrQkow==','securitystamp789','e8d1ad2a-424d-4453-8092-8b1749263329','775549788',1,0,'2025-02-22 18:26:37.882731',0,0);
/*!40000 ALTER TABLE `TblUsers` ENABLE KEYS */;

-- 
-- Definition of RefreshToken
-- 

DROP TABLE IF EXISTS `RefreshToken`;
CREATE TABLE IF NOT EXISTS `RefreshToken` (
  `AppUserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Id` int NOT NULL AUTO_INCREMENT,
  `Token` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Expires` datetime(6) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Revoked` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`,`AppUserId`),
  KEY `FK_RefreshToken_TblUsers_AppUserId` (`AppUserId`),
  CONSTRAINT `FK_RefreshToken_TblUsers_AppUserId` FOREIGN KEY (`AppUserId`) REFERENCES `TblUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table RefreshToken
-- 

/*!40000 ALTER TABLE `RefreshToken` DISABLE KEYS */;
INSERT INTO `RefreshToken`(`AppUserId`,`Id`,`Token`,`Expires`,`Created`,`Revoked`) VALUES('10554a38-873a-43f4-9228-1f2ee136cd1b',1,'SzuzA7rBgLBDjinXfMnN3IraePP8/Ds3JWIWMm5ZTfE=','2025-02-24 17:44:17.966835','2025-02-23 17:44:17.966959',NULL);
/*!40000 ALTER TABLE `RefreshToken` ENABLE KEYS */;

-- 
-- Definition of Tbl_Role
-- 

DROP TABLE IF EXISTS `Tbl_Role`;
CREATE TABLE IF NOT EXISTS `Tbl_Role` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_Role
-- 

/*!40000 ALTER TABLE `Tbl_Role` DISABLE KEYS */;
INSERT INTO `Tbl_Role`(`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`) VALUES('36c9d5b8-e498-4969-bad4-96a4aef6dd00','Admin','ADMIN','b4bdef3a-30ad-44c7-9f18-5c3a4d1167e1'),('36c9d5b8-e498-4969-kuiq-96a4aef6dd00','Manager','MANAGER','b4bdef3a-30ad-44c7-9f18-5c3a4d1167e1'),('bf8f6d4e-86cb-483a-9b74-e9d80733077f','User','USER','81f756e6-77d7-4982-9864-ca2321ffc562');
/*!40000 ALTER TABLE `Tbl_Role` ENABLE KEYS */;

-- 
-- Definition of Tbl_Role_Claims
-- 

DROP TABLE IF EXISTS `Tbl_Role_Claims`;
CREATE TABLE IF NOT EXISTS `Tbl_Role_Claims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Tbl_Role_Claims_RoleId` (`RoleId`),
  CONSTRAINT `FK_Tbl_Role_Claims_Tbl_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Tbl_Role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_Role_Claims
-- 

/*!40000 ALTER TABLE `Tbl_Role_Claims` DISABLE KEYS */;

/*!40000 ALTER TABLE `Tbl_Role_Claims` ENABLE KEYS */;

-- 
-- Definition of Tbl_User_Claims
-- 

DROP TABLE IF EXISTS `Tbl_User_Claims`;
CREATE TABLE IF NOT EXISTS `Tbl_User_Claims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Tbl_User_Claims_UserId` (`UserId`),
  CONSTRAINT `FK_Tbl_User_Claims_TblUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `TblUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_User_Claims
-- 

/*!40000 ALTER TABLE `Tbl_User_Claims` DISABLE KEYS */;

/*!40000 ALTER TABLE `Tbl_User_Claims` ENABLE KEYS */;

-- 
-- Definition of Tbl_User_Login
-- 

DROP TABLE IF EXISTS `Tbl_User_Login`;
CREATE TABLE IF NOT EXISTS `Tbl_User_Login` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_Tbl_User_Login_UserId` (`UserId`),
  CONSTRAINT `FK_Tbl_User_Login_TblUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `TblUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_User_Login
-- 

/*!40000 ALTER TABLE `Tbl_User_Login` DISABLE KEYS */;

/*!40000 ALTER TABLE `Tbl_User_Login` ENABLE KEYS */;

-- 
-- Definition of Tbl_User_Role
-- 

DROP TABLE IF EXISTS `Tbl_User_Role`;
CREATE TABLE IF NOT EXISTS `Tbl_User_Role` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_Tbl_User_Role_RoleId` (`RoleId`),
  CONSTRAINT `FK_Tbl_User_Role_Tbl_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Tbl_Role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Tbl_User_Role_TblUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `TblUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_User_Role
-- 

/*!40000 ALTER TABLE `Tbl_User_Role` DISABLE KEYS */;
INSERT INTO `Tbl_User_Role`(`UserId`,`RoleId`) VALUES('10554a38-873a-43f4-9228-1f2ee136cd1b','36c9d5b8-e498-4969-bad4-96a4aef6dd00');
/*!40000 ALTER TABLE `Tbl_User_Role` ENABLE KEYS */;

-- 
-- Definition of Tbl_User_Token
-- 

DROP TABLE IF EXISTS `Tbl_User_Token`;
CREATE TABLE IF NOT EXISTS `Tbl_User_Token` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_Tbl_User_Token_TblUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `TblUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tbl_User_Token
-- 

/*!40000 ALTER TABLE `Tbl_User_Token` DISABLE KEYS */;

/*!40000 ALTER TABLE `Tbl_User_Token` ENABLE KEYS */;

-- 
-- Definition of Tblemailsetting
-- 

DROP TABLE IF EXISTS `Tblemailsetting`;
CREATE TABLE IF NOT EXISTS `Tblemailsetting` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `host` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `port` int NOT NULL,
  `YourDomain` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Isdeleted` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `Isdelete` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table Tblemailsetting
-- 

/*!40000 ALTER TABLE `Tblemailsetting` DISABLE KEYS */;

/*!40000 ALTER TABLE `Tblemailsetting` ENABLE KEYS */;

-- 
-- Definition of __EFMigrationsHistory
-- 

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table __EFMigrationsHistory
-- 

/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory`(`MigrationId`,`ProductVersion`) VALUES('20250223171854_Initial','8.0.4');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2025-02-24 00:07:18
-- Total time: 0:0:0:1:146 (d:h:m:s:ms)
