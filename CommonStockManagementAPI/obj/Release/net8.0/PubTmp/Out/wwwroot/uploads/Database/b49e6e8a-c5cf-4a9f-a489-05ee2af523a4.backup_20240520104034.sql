-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-05-20 10:40:38
-- --------------------------------------
-- Server version 8.0.35 Source distribution


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
) ENGINE=InnoDB AUTO_INCREMENT=160 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblAuditTrails
-- 

/*!40000 ALTER TABLE `TblAuditTrails` DISABLE KEYS */;
INSERT INTO `TblAuditTrails`(`Id`,`EntityName`,`Action`,`Details`,`Timestamp`,`UserId`) VALUES(1,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-23 00:23:33.721649','UserId'),(2,'TblSuppliers','Insert','Insert Sudukumudu  Name.','2024-04-23 00:26:14.579883','d5d765ba-b22f-4e9b-a399-30820a926e82'),(3,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-04-23 00:28:15.010854','d5d765ba-b22f-4e9b-a399-30820a926e82'),(4,'TblGRNHeads','Insert','Insert 32 ID.','2024-04-23 00:29:53.085919','d5d765ba-b22f-4e9b-a399-30820a926e82'),(5,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-23 00:31:24.428160','d5d765ba-b22f-4e9b-a399-30820a926e82'),(6,'TblClients','Insert','Insert Madushan Sfgd Name.','2024-04-23 00:57:42.010949','d5d765ba-b22f-4e9b-a399-30820a926e82'),(7,'TblClients','Insert','Insert Dfkjgfh Fghdfgyuy Name.','2024-04-23 09:38:03.347492','d5d765ba-b22f-4e9b-a399-30820a926e82'),(8,'TblItemRentalHeads','Insert','Insert 32 ID.','2024-04-24 11:51:34.843598','d5d765ba-b22f-4e9b-a399-30820a926e82'),(9,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-24 11:57:20.748961','d5d765ba-b22f-4e9b-a399-30820a926e82'),(10,'TblItemRentalHeads','Insert','Insert 32 ID.','2024-04-24 11:57:54.743663','d5d765ba-b22f-4e9b-a399-30820a926e82'),(11,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-24 17:38:17.535277','UserId'),(12,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-24 17:48:29.514262','UserId'),(13,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-24 19:09:53.282683','UserId'),(14,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-26 16:57:05.856252','UserId'),(15,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-26 19:24:36.171195','d5d765ba-b22f-4e9b-a399-30820a926e82'),(16,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-26 19:29:05.262559','d5d765ba-b22f-4e9b-a399-30820a926e82'),(17,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-26 20:36:05.931448','UserId'),(18,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-26 20:37:12.894936','d5d765ba-b22f-4e9b-a399-30820a926e82'),(19,'TblItemRentalDetailsTemp','Insert','Insert Water Pipe Fittings (20mm ) * (1/2\")   Code.','2024-04-26 20:55:48.469145','d5d765ba-b22f-4e9b-a399-30820a926e82'),(20,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-30 19:29:00.228063','UserId'),(21,'TblClients','Insert','Insert Madushan Chamara Name.','2024-04-30 20:00:39.145282','d5d765ba-b22f-4e9b-a399-30820a926e82'),(22,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-04-30 22:32:55.068210','UserId'),(23,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:41:46.027425','d5d765ba-b22f-4e9b-a399-30820a926e82'),(24,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:43:39.564982','d5d765ba-b22f-4e9b-a399-30820a926e82'),(25,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:45:40.105395','d5d765ba-b22f-4e9b-a399-30820a926e82'),(26,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:46:57.723451','d5d765ba-b22f-4e9b-a399-30820a926e82'),(27,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:50:22.533349','d5d765ba-b22f-4e9b-a399-30820a926e82'),(28,'TblClients','Update','Update Madushan Sfgd name.','2024-04-30 22:51:26.450818','d5d765ba-b22f-4e9b-a399-30820a926e82'),(29,'TblSuppliers','Update','Update Sudukumudu  name.','2024-04-30 22:53:06.165618','d5d765ba-b22f-4e9b-a399-30820a926e82'),(30,'TblGRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-02 14:01:33.689432','d5d765ba-b22f-4e9b-a399-30820a926e82'),(31,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-02 17:07:47.021077','UserId'),(32,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-02 17:11:12.972951','UserId'),(33,'TblStock_Mains','Update','Update Solvent Cement PVC 125g  name.','2024-05-04 10:32:21.807694','d5d765ba-b22f-4e9b-a399-30820a926e82'),(34,'TblStock_Mains','Update','Update Solvent Cement PVC 15g  name.','2024-05-04 10:33:20.766305','d5d765ba-b22f-4e9b-a399-30820a926e82'),(35,'TblStock_Mains','Update','Update Solvent Cement PVC 15g  name.','2024-05-04 10:43:37.858877','d5d765ba-b22f-4e9b-a399-30820a926e82'),(36,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-06 10:03:32.936254','UserId'),(37,'TblGRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-06 10:33:50.181166','d5d765ba-b22f-4e9b-a399-30820a926e82'),(38,'TblGRNBodyTemps','Delete','Delete this  2 name.','2024-05-06 10:50:03.724703','d5d765ba-b22f-4e9b-a399-30820a926e82'),(39,'TblGRNBodyTemps','Delete','Delete this  3 name.','2024-05-06 10:50:09.271994','d5d765ba-b22f-4e9b-a399-30820a926e82'),(40,'TblGRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-06 10:50:40.459892','d5d765ba-b22f-4e9b-a399-30820a926e82'),(41,'TblGRNBodyTemps','Delete','Delete this  4 name.','2024-05-06 11:29:27.834376','d5d765ba-b22f-4e9b-a399-30820a926e82'),(42,'TblGRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-06 11:29:48.928377','d5d765ba-b22f-4e9b-a399-30820a926e82'),(43,'TblItemBrandName','Update','Update S & lon  name.','2024-05-06 12:15:46.839844','d5d765ba-b22f-4e9b-a399-30820a926e82'),(44,'TblItemBrandName','Insert','Insert Gdfdf  Name.','2024-05-06 12:17:36.548165','d5d765ba-b22f-4e9b-a399-30820a926e82'),(45,'TblItemBrandName','Update','Update Gdfdf  name.','2024-05-06 12:20:21.207488','d5d765ba-b22f-4e9b-a399-30820a926e82'),(46,'TblStock_MainItem Category','Insert','Insert Fghb  Name.','2024-05-06 12:24:04.647187','d5d765ba-b22f-4e9b-a399-30820a926e82'),(47,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-06 12:46:08.565385','UserId'),(48,'TblGRNBodyTemps','Insert','Insert SC5  Code.','2024-05-06 15:41:28.954862','d5d765ba-b22f-4e9b-a399-30820a926e82'),(49,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-06 15:51:16.901800','d5d765ba-b22f-4e9b-a399-30820a926e82'),(50,'TblSRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-06 16:24:26.348058','d5d765ba-b22f-4e9b-a399-30820a926e82'),(51,'TblStock_Mains','Update','Update Water Pipe Fittings (20mm ) * (1/2\")   name.','2024-05-08 22:02:50.764244','d5d765ba-b22f-4e9b-a399-30820a926e82'),(52,'TblStock_Mains','Update','Update Water Pipe Fittings (20mm ) * (1/2\")   name.','2024-05-08 22:03:39.081500','d5d765ba-b22f-4e9b-a399-30820a926e82'),(53,'TblSRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-08 22:46:01.898008','d5d765ba-b22f-4e9b-a399-30820a926e82'),(54,'TblSRNBodyTemps','Delete','Delete this  2 name.','2024-05-08 22:53:36.775468','d5d765ba-b22f-4e9b-a399-30820a926e82'),(55,'TblSRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-08 22:54:38.817392','d5d765ba-b22f-4e9b-a399-30820a926e82'),(56,'TblSRNBodyTemps','Delete','Delete this  3 name.','2024-05-08 22:56:05.043530','d5d765ba-b22f-4e9b-a399-30820a926e82'),(57,'TblSRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-08 22:56:42.518661','d5d765ba-b22f-4e9b-a399-30820a926e82'),(58,'TblSRNHeads','Insert','Insert 32 ID.','2024-05-08 22:57:18.760356','d5d765ba-b22f-4e9b-a399-30820a926e82'),(59,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-12 20:01:00.026608','UserId'),(60,'TblSRNHeads','Delete','Delete this 1 name.','2024-05-12 20:14:17.538975',NULL),(61,'TblSRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-12 20:15:04.844509','d5d765ba-b22f-4e9b-a399-30820a926e82'),(62,'TblSRNBodyTemps','Insert','Insert SC5  Code.','2024-05-12 20:16:00.545034','d5d765ba-b22f-4e9b-a399-30820a926e82'),(63,'TblSRNBodyTemps','Delete','Delete this  6 name.','2024-05-12 20:16:12.839849','d5d765ba-b22f-4e9b-a399-30820a926e82'),(64,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-13 12:58:55.636763','UserId'),(65,'TblPOSBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-13 16:46:32.160180','d5d765ba-b22f-4e9b-a399-30820a926e82'),(66,'TblGRNHeads','Delete','Delete this 2 name.','2024-05-13 17:14:42.591962',NULL),(67,'TblGRNHeads','Delete','Delete this 1 name.','2024-05-13 17:14:48.308787',NULL),(68,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 17:15:15.140100','d5d765ba-b22f-4e9b-a399-30820a926e82'),(69,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 17:17:00.406122','d5d765ba-b22f-4e9b-a399-30820a926e82'),(70,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-13 17:23:28.077693','UserId'),(71,'TblGRNHeads','Update','Update 35 ID.','2024-05-13 17:53:24.406746','d5d765ba-b22f-4e9b-a399-30820a926e82'),(72,'TblPOSBodyTemps','Delete','Delete this  1 name.','2024-05-13 17:55:43.277508','d5d765ba-b22f-4e9b-a399-30820a926e82'),(73,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 18:04:52.338668','d5d765ba-b22f-4e9b-a399-30820a926e82'),(74,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 18:05:23.058930','d5d765ba-b22f-4e9b-a399-30820a926e82'),(75,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 18:09:14.927519','d5d765ba-b22f-4e9b-a399-30820a926e82'),(76,'TblPOSBodyTemps','Delete','Delete this  2 name.','2024-05-13 18:10:50.743972','d5d765ba-b22f-4e9b-a399-30820a926e82'),(77,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 18:11:06.915473','d5d765ba-b22f-4e9b-a399-30820a926e82'),(78,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 18:12:40.108494','d5d765ba-b22f-4e9b-a399-30820a926e82'),(79,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 18:13:18.042416','d5d765ba-b22f-4e9b-a399-30820a926e82'),(80,'TblPOSBodyTemps','Delete','Delete this  3 name.','2024-05-13 18:13:34.371722','d5d765ba-b22f-4e9b-a399-30820a926e82'),(81,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-13 18:27:31.156418','UserId'),(82,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 18:33:22.225695','d5d765ba-b22f-4e9b-a399-30820a926e82'),(83,'TblPOSHeads','Insert','Insert 32 ID.','2024-05-13 18:35:33.499363','d5d765ba-b22f-4e9b-a399-30820a926e82'),(84,'TblGRNHeads','Delete','Delete this 3 name.','2024-05-13 19:04:52.132717',NULL),(85,'TblGRNHeads','Delete','Delete this 4 name.','2024-05-13 19:06:32.883984',NULL),(86,'TblGRNHeads','Delete','Delete this 5 name.','2024-05-13 19:06:32.883927',NULL),(87,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:07:37.402562','d5d765ba-b22f-4e9b-a399-30820a926e82'),(88,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 19:08:07.917491','d5d765ba-b22f-4e9b-a399-30820a926e82'),(89,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:11:00.764231','d5d765ba-b22f-4e9b-a399-30820a926e82'),(90,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 19:11:25.751083','d5d765ba-b22f-4e9b-a399-30820a926e82'),(91,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:19:58.586477','d5d765ba-b22f-4e9b-a399-30820a926e82'),(92,'TblPOSHeads','Insert','Insert 32 ID.','2024-05-13 19:22:26.033450','d5d765ba-b22f-4e9b-a399-30820a926e82'),(93,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:27:47.580792','d5d765ba-b22f-4e9b-a399-30820a926e82'),(94,'TblPOSHeads','Insert','Insert 32 ID.','2024-05-13 19:28:14.608612','d5d765ba-b22f-4e9b-a399-30820a926e82'),(95,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:44:45.873017','d5d765ba-b22f-4e9b-a399-30820a926e82'),(96,'TblGRNBodyTemps','Delete','Delete this  19 name.','2024-05-13 19:47:13.299900','d5d765ba-b22f-4e9b-a399-30820a926e82'),(97,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 19:53:49.587162','d5d765ba-b22f-4e9b-a399-30820a926e82'),(98,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-13 20:12:52.318773','UserId'),(99,'TblGRNBodyTemps','Update','Update WPFS1  Code.','2024-05-13 20:16:15.522594','d5d765ba-b22f-4e9b-a399-30820a926e82'),(100,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-13 20:17:20.394876','d5d765ba-b22f-4e9b-a399-30820a926e82'),(101,'TblPOSBodyTemps','Insert','Insert WPFS1  Code.','2024-05-13 20:23:11.425983','d5d765ba-b22f-4e9b-a399-30820a926e82'),(102,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-14 08:29:15.001144','UserId'),(103,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-14 08:44:01.373229','UserId'),(104,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 09:16:01.622868','d5d765ba-b22f-4e9b-a399-30820a926e82'),(105,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-14 09:52:12.692108','d5d765ba-b22f-4e9b-a399-30820a926e82'),(106,'TblGRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 09:55:26.630931','d5d765ba-b22f-4e9b-a399-30820a926e82'),(107,'TblGRNHeads','Insert','Insert 32 ID.','2024-05-14 09:57:26.443763','d5d765ba-b22f-4e9b-a399-30820a926e82'),(108,'TblPOSReturnBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 11:55:00.466258','d5d765ba-b22f-4e9b-a399-30820a926e82'),(109,'TblPOSReturnBodyTemps','Delete','Delete this  1 name.','2024-05-14 11:56:10.445346','d5d765ba-b22f-4e9b-a399-30820a926e82'),(110,'TblPOSReturnBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 14:02:29.191335','d5d765ba-b22f-4e9b-a399-30820a926e82'),(111,'TblPOSReturnBodyTemps','Delete','Delete this  2 name.','2024-05-14 14:04:31.646077','d5d765ba-b22f-4e9b-a399-30820a926e82'),(112,'TblPOSReturnBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 14:05:57.042532','d5d765ba-b22f-4e9b-a399-30820a926e82'),(113,'TblGRNBodyTemps','Insert','Insert WP(PNT14PE)   Code.','2024-05-14 14:09:51.337779','d5d765ba-b22f-4e9b-a399-30820a926e82'),(114,'TblPOSReturnHeads','Insert','Insert 32 ID.','2024-05-14 14:19:11.650726','d5d765ba-b22f-4e9b-a399-30820a926e82'),(115,'TblGRNBodyTemps','Update','Update WP(PNT14PE)   Code.','2024-05-14 14:20:47.243555','d5d765ba-b22f-4e9b-a399-30820a926e82'),(116,'TblSRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-14 14:30:05.822519','d5d765ba-b22f-4e9b-a399-30820a926e82'),(117,'TblGRNBodyTemps','Insert','Insert SC5  Code.','2024-05-14 15:02:49.891073','d5d765ba-b22f-4e9b-a399-30820a926e82'),(118,'TblCompanyDetails','Insert','Insert Sudukumudu  Name.','2024-05-15 13:25:36.970406','d5d765ba-b22f-4e9b-a399-30820a926e82'),(119,'TblCompanyDetails','Update','Update Sudukumudu  name.','2024-05-15 13:50:28.902641','d5d765ba-b22f-4e9b-a399-30820a926e82'),(120,'TblCompanyDetails','Insert','Insert sgdf  Name.','2024-05-15 14:08:55.603355','d5d765ba-b22f-4e9b-a399-30820a926e82'),(121,'TblCompanyDetails','Insert','Insert vffdd  Name.','2024-05-15 14:10:20.184102','d5d765ba-b22f-4e9b-a399-30820a926e82'),(122,'TblCompanyDetails','Insert','Insert xcfgg  Name.','2024-05-15 14:10:49.454100','d5d765ba-b22f-4e9b-a399-30820a926e82'),(123,'TblCompanyDetails','Insert','Insert dadsxf  Name.','2024-05-15 14:28:41.825952','d5d765ba-b22f-4e9b-a399-30820a926e82'),(124,'TblCompanyDetails','Insert','Insert sdfds  Name.','2024-05-15 14:30:08.030635','d5d765ba-b22f-4e9b-a399-30820a926e82'),(125,'TblCompanyDetails','Update','Update dffghgh  name.','2024-05-15 14:37:37.731958','d5d765ba-b22f-4e9b-a399-30820a926e82'),(126,'TblCompanyDetails','Update','Update ghgfg  name.','2024-05-15 14:43:17.618938','d5d765ba-b22f-4e9b-a399-30820a926e82'),(127,'TblCompanyDetails','Update','Update ghgfg  name.','2024-05-15 14:45:14.800320','d5d765ba-b22f-4e9b-a399-30820a926e82'),(128,'TblCompanyDetails','Insert','Insert Sudukumududsd  Name.','2024-05-15 15:49:11.849207','d5d765ba-b22f-4e9b-a399-30820a926e82'),(129,'TblCompanyDetails','Insert','Insert Sudukumududsdgfgdfgfdg  Name.','2024-05-15 15:51:30.090561','d5d765ba-b22f-4e9b-a399-30820a926e82'),(130,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-16 10:01:15.833915','UserId'),(131,'TblCompanyDetails','Insert','Insert sdfjdsfuy  Name.','2024-05-16 13:34:07.480607','d5d765ba-b22f-4e9b-a399-30820a926e82'),(132,'TblStock_MainItem Category','Insert','Insert Dfkjgfh Fghdfgyuy  Name.','2024-05-16 17:40:19.338841','d5d765ba-b22f-4e9b-a399-30820a926e82'),(133,'TblStock_MainItem Category','Update','Update Dfkjgfh Fghdfgyuyv  name.','2024-05-16 17:40:49.682983','d5d765ba-b22f-4e9b-a399-30820a926e82'),(134,'TblStock_MainItem Category','Update','Update Nmnmnm  name.','2024-05-16 17:52:43.840897','d5d765ba-b22f-4e9b-a399-30820a926e82'),(135,'TblGRNBodyTemps','Delete','Delete this  24 name.','2024-05-16 20:43:14.247482','d5d765ba-b22f-4e9b-a399-30820a926e82'),(136,'TblSRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-17 14:59:28.226203','d5d765ba-b22f-4e9b-a399-30820a926e82'),(137,'TblSRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-17 15:19:39.882620','d5d765ba-b22f-4e9b-a399-30820a926e82'),(138,'TblSRNBodyTemps','Delete','Delete this  9 name.','2024-05-17 15:20:27.418119','d5d765ba-b22f-4e9b-a399-30820a926e82'),(139,'TblSRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-17 15:21:32.444668','d5d765ba-b22f-4e9b-a399-30820a926e82'),(140,'TblSRNBodyTemps','Delete','Delete this  10 name.','2024-05-17 15:22:32.159257','d5d765ba-b22f-4e9b-a399-30820a926e82'),(141,'TblSRNBodyTemps','Insert','Insert WPFS1  Code.','2024-05-17 15:25:59.090898','d5d765ba-b22f-4e9b-a399-30820a926e82'),(142,'TblSRNHeads','Insert','Insert 32 ID.','2024-05-17 15:32:00.162994','d5d765ba-b22f-4e9b-a399-30820a926e82'),(143,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-17 19:03:03.362200','UserId'),(144,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-17 20:34:18.055496','UserId'),(145,'TblStock_Mains','Update','Update Water Pipe PNt 14 (20mm ) * (1/2\") (SS)  name.','2024-05-18 12:12:09.900759','d5d765ba-b22f-4e9b-a399-30820a926e82'),(146,'TblStock_Mains','Update','Update Water Pipe PNt 14 (20mm ) * (1/2\") (SS)  name.','2024-05-18 12:28:22.494238','d5d765ba-b22f-4e9b-a399-30820a926e82'),(147,'TblStock_Mains','Insert','Insert Sdsads  Name.','2024-05-18 12:32:50.593439','d5d765ba-b22f-4e9b-a399-30820a926e82'),(148,'TblStock_Mains','Update','Update Sdsads  name.','2024-05-18 14:27:51.519176','d5d765ba-b22f-4e9b-a399-30820a926e82'),(149,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-19 09:13:25.105707','UserId'),(150,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-19 11:42:12.847307','UserId'),(151,'TblDatabaseBackupHistory','Insert','Insert text  Name.','2024-05-19 11:44:23.256831','d5d765ba-b22f-4e9b-a399-30820a926e82'),(152,'TblDatabaseBackupHistory','Insert','Insert text  Name.','2024-05-19 19:38:19.625994','d5d765ba-b22f-4e9b-a399-30820a926e82'),(153,'EntityName','Update','Entity with ID d5d765ba-b22f-4e9b-a399-30820a926e82 was updated.','2024-05-19 19:54:46.959579','UserId'),(154,'TblDatabaseBackupHistory','Insert','Insert text  Name.','2024-05-19 21:31:00.357613','d5d765ba-b22f-4e9b-a399-30820a926e82'),(155,'TblDatabaseBackupHistory','Insert','Insert text  Name.','2024-05-19 21:40:24.916666','d5d765ba-b22f-4e9b-a399-30820a926e82'),(156,'TblDatabaseBackupHistory','Insert','Insert text  Name.','2024-05-19 22:14:42.972902','d5d765ba-b22f-4e9b-a399-30820a926e82'),(157,'TblDatabaseBackupHistory','Insert','Insert Sdsda  Name.','2024-05-20 10:01:24.135472','d5d765ba-b22f-4e9b-a399-30820a926e82'),(158,'TblDatabaseBackupHistory','Insert','Insert Text  Name.','2024-05-20 10:33:51.342221','d5d765ba-b22f-4e9b-a399-30820a926e82'),(159,'TblDatabaseBackupHistory','Insert','Insert Dfgfg  Name.','2024-05-20 10:35:59.984306','d5d765ba-b22f-4e9b-a399-30820a926e82');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblClients
-- 

/*!40000 ALTER TABLE `TblClients` DISABLE KEYS */;
INSERT INTO `TblClients`(`ID`,`Title`,`FirstName`,`LastName`,`Address`,`Area`,`Email`,`Nic`,`Mobile`,`Tel`,`Type`,`RegistrationDate`,`Isdelete`,`ImageURl`,`Dr`,`Cr`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'mr','Madushan','Sfgd','jgdgfdgjh',NULL,NULL,NULL,'0742955460',NULL,NULL,'2024-04-23 00:00:00.000000',0,'Select.png',NULL,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-30 22:51:22.132780',NULL),(2,NULL,'Dfkjgfh','Fghdfgyuy','dfgirtygufybiuretiugyfb',NULL,NULL,NULL,'0742955460',NULL,NULL,'2024-04-23 00:00:00.000000',0,'Select.png',NULL,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-23 09:38:00.886842',NULL),(3,NULL,'Madushan','Chamara','42/10/2 hiripitiya,pannipitiya.',NULL,'sudukumudu421@gmail.com','20012896574d','0775549788',NULL,NULL,'2024-04-30 00:00:00.000000',0,'Select.png',NULL,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-30 20:00:32.221703',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=226 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblCompanyDetails
-- 

/*!40000 ALTER TABLE `TblCompanyDetails` DISABLE KEYS */;
INSERT INTO `TblCompanyDetails`(`Id`,`CompanyName`,`Address`,`TelPhone1`,`TelPhone2`,`Isdelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(2,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(3,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(4,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(5,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(6,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(7,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(8,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(9,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(10,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(11,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(12,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(13,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(14,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(15,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(16,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(17,'ishan5','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(18,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(19,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(20,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(21,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(22,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(23,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(24,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(25,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(26,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(27,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(28,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(29,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(30,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(31,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(32,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(33,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(34,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(35,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(36,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(37,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(38,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(39,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(40,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(41,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(42,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(43,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(44,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(45,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(46,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(47,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(48,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(49,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(50,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(51,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(52,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(53,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(54,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(55,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(56,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(57,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(58,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(59,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(60,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(61,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(62,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(63,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(64,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(65,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(66,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(67,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(68,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(69,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(70,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(71,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(72,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(73,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(74,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(75,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(76,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(77,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(78,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(79,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(80,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(81,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(82,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(83,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(84,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(85,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(86,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(87,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(88,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(89,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(90,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(91,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(92,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(93,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(94,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(95,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(96,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(97,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(98,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(99,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(100,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(101,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(102,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(103,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(104,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(105,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(106,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(107,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(108,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(109,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(110,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(111,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(112,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(113,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(114,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(115,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(116,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(117,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(118,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(119,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(120,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(121,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(122,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(123,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(124,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(125,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(126,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(127,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(128,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(129,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(130,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(131,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(132,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(133,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(134,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(135,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(136,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(137,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(138,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(139,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(140,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(141,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(142,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(143,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(144,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(145,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(146,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(147,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(148,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(149,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(150,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(151,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(152,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(153,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(154,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(155,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(156,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(157,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(158,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(159,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(160,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(161,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(162,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(163,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(164,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(165,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(166,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(167,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(168,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(169,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(170,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(171,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(172,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(173,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(174,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(175,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(176,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(177,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(178,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(179,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(180,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(181,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(182,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(183,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(184,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(185,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(186,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(187,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(188,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(189,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(190,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(191,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(192,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(193,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(194,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(195,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(196,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(197,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(198,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(199,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(200,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(201,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(202,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(203,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(204,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(205,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(206,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(207,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(208,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(209,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(210,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(211,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(212,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(213,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(214,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(215,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(216,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(217,'Sudukumudu','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 13:50:26.947692',NULL),(218,'sgdf','Dgdfgfdg','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:08:53.358917',NULL),(219,'vffdd','Gdfg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:15.033775',NULL),(220,'xcfgg','Dgfgfdg','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:10:46.621448',NULL),(221,'dadsxf','Ffdf','0742955460','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:28:39.546741',NULL),(222,'ghgfg','Dsfds','0742955460','0742955460',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 14:45:01.177841',NULL),(223,'Sudukumududsd','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:49:08.163005',NULL),(224,'Sudukumududsdgfgdfgfdg','42 /10/2 Hiripitiya, pannipitiya.','0776737222','0776737222',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-15 15:51:27.424337',NULL),(225,'sdfjdsfuy','42 /10/2 dsfdsfs, pannsdfsdfipitiya.','0776737222','0777947595',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-16 13:34:01.766195',NULL);
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
  `TagDiscription` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsDelete` tinyint(1) NOT NULL,
  `Edit_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Delete_By` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Edit_Date` datetime(6) NOT NULL,
  `Delete_Date` datetime(6) DEFAULT NULL,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblDatabaseBackupHistory
-- 

/*!40000 ALTER TABLE `TblDatabaseBackupHistory` DISABLE KEYS */;
INSERT INTO `TblDatabaseBackupHistory`(`ID`,`DateTime`,`DatabaseName`,`Reason`,`TagDiscription`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`,`UserName`) VALUES(7,'2024-05-20 10:33:47.000000','73b60e01-9b1e-4a96-8fae-b0286cb1ba7a.backup_20240520103237.sql','Text','Take a backup\n',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-20 10:33:47.490089',NULL,NULL),(8,'2024-05-20 10:35:58.000000','e3be2bb4-31fd-4253-9f83-830923f30510.backup_20240520103458.sql','Dfgfg','Dfgfdgf',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-20 10:35:58.111595',NULL,NULL);
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
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Batch` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNBodyTemp
-- 

/*!40000 ALTER TABLE `TblGRNBodyTemp` DISABLE KEYS */;
INSERT INTO `TblGRNBodyTemp`(`Id`,`ItemID`,`GRnNo`,`GRnBodyNo`,`Code`,`ItemName`,`Qty`,`FreeQty`,`UnitCost`,`Cost`,`Discount`,`Batch`,`Sellingprice`,`ExpDate`,`UserID`,`Amount`,`Qtypiece`,`UnitName`,`UnitSize`) VALUES(23,9,0,0,'WP(PNT14PE) ','Water Pipe PNt 14 (20mm ) * (1/2\") (SS)',12.50,0.00,570.00,1781.25,0.00,'',760.00,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',1781.25,3.13,'Meters','4');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemBrandNames
-- 

/*!40000 ALTER TABLE `TblItemBrandNames` DISABLE KEYS */;
INSERT INTO `TblItemBrandNames`(`Id`,`Name`,`Description`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'S & lon','S lon',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-06 12:15:43.717820',NULL),(2,'Gdfdf','Gdfgdg',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-06 12:20:16.907034',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemCategories
-- 

/*!40000 ALTER TABLE `TblItemCategories` DISABLE KEYS */;
INSERT INTO `TblItemCategories`(`Id`,`Name`,`Description`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Glue','Glue',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:36:23.895437',NULL),(2,'Water Pipe','Water Pipe',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:40:34.947433',NULL),(3,'Water Pipe Fittings','Water Pipe Fittings',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:12:02.280358',NULL),(4,'Fghb','Fgb',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-06 12:24:00.228419',NULL),(5,'Nmnmnm','Tyg',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-16 17:52:40.585640',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemModelTypes
-- 

/*!40000 ALTER TABLE `TblItemModelTypes` DISABLE KEYS */;
INSERT INTO `TblItemModelTypes`(`Id`,`Name`,`Description`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Solvent Cement PVC','Solvent Cement PVC',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:17:59.950451',NULL),(2,'Water Pipe PN T 14','Water Pipe PN T 14',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:43:20.791838',NULL),(3,'Water Pipe Fittings (Socket)','Water Pipe Fittings (Socket)',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:17:09.857577',NULL),(4,'Water Pipe Fittings (Valve Socket)','Water Pipe Fittings (Valve Socket)',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:17:43.162779',NULL),(5,'Water Pipe Fittings (Faucet Socket)','Water Pipe Fittings (Faucet Socket)',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:18:56.530508',NULL),(6,'Water Pipe Fittings (Elbow Socket)','Water Pipe Fittings (Elbow Socket)',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:19:12.462166',NULL),(7,'Water Pipe Fittings (Elbow)','Water Pipe Fittings (Elbow)',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:19:25.503190',NULL),(8,'Water Pipe Fittings (Elbow Tee)  ','Water Pipe Fittings (Elbow Tee)  ',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:19:42.663660',NULL),(9,'Water Pipe Fittings  (Bend)  ','Water Pipe Fittings  (Bend)  ',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:20:04.208430',NULL),(10,'Water Pipe Fittings  (Cap)  ','Water Pipe Fittings  (Cap)  ',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:20:23.017887',NULL),(11,'Water Pipe Fittings  (Saddle Clip)  ','Water Pipe Fittings  (Saddle Clip)  ',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:21:11.002727',NULL),(12,'Water Pipe Fittings  (Elbow 45)  ','Water Pipe Fittings  (Elbow 45)  ',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:21:57.492404',NULL);
/*!40000 ALTER TABLE `TblItemModelTypes` ENABLE KEYS */;

-- 
-- Definition of TblItemRentalDetailsTemp
-- 

DROP TABLE IF EXISTS `TblItemRentalDetailsTemp`;
CREATE TABLE IF NOT EXISTS `TblItemRentalDetailsTemp` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ItemID` int NOT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `DayCost` decimal(18,2) DEFAULT NULL,
  `TotalCost` decimal(18,2) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RentalItemBodyId` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemUnits
-- 

/*!40000 ALTER TABLE `TblItemUnits` DISABLE KEYS */;
INSERT INTO `TblItemUnits`(`Id`,`Name`,`Description`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Kg','Kg',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:14:23.659871',NULL),(2,'G grams','G grams',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:21:05.249214',NULL),(3,'Feet','Feet',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:42:20.561518',NULL),(4,'Meters','Meters',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:52:20.657410',NULL),(5,'Piece','Piece',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 15:15:40.943457',NULL);
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
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSBodyTemp
-- 

/*!40000 ALTER TABLE `TblPOSBodyTemp` DISABLE KEYS */;
INSERT INTO `TblPOSBodyTemp`(`Id`,`ItemID`,`Code`,`ItemName`,`Qty`,`FreeQty`,`UnitCost`,`Cost`,`Discount`,`Sellingprice`,`ExpDate`,`UserID`,`Amount`,`Qtypiece`,`UnitName`,`UnitSize`) VALUES(7,10,'WPFS1','Water Pipe Fittings (20mm ) * (1/2\") ',100.00,0.00,39.00,400.00,0.00,40.00,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',400.00,10.00,'Meters','10');
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
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
  `Qty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Batch` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Sellingprice` decimal(18,2) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `UserID` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Amount` decimal(18,2) DEFAULT NULL,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
INSERT INTO `TblStock_Location`(`ID`,`Name`,`Type`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Main','Main',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-22 10:35:44.999392',NULL);
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
  `Qty` decimal(18,2) DEFAULT NULL,
  `FreeQty` decimal(18,2) DEFAULT NULL,
  `UnitCost` decimal(18,2) DEFAULT NULL,
  `Cost` decimal(65,30) DEFAULT NULL,
  `Price` decimal(65,30) DEFAULT NULL,
  `ExpDate` datetime(6) DEFAULT NULL,
  `DisCount` decimal(18,2) DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_TblGRNBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblGRNBody_Grnno` (`Grnno`),
  CONSTRAINT `FK_TblGRNBody_TblGRNHead_Grnno` FOREIGN KEY (`Grnno`) REFERENCES `TblGRNHead` (`Id`),
  CONSTRAINT `FK_TblGRNBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNBody
-- 

/*!40000 ALTER TABLE `TblGRNBody` DISABLE KEYS */;
INSERT INTO `TblGRNBody`(`Id`,`Grnno`,`ItemID`,`Code`,`Qty`,`FreeQty`,`UnitCost`,`Cost`,`Price`,`ExpDate`,`DisCount`,`IsDelete`,`FKLocationId`,`UnitSize`,`Qtypiece`,`UnitName`) VALUES(9,7,10,'WPFS1',1000.00,0.00,39.00,3900.0000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,'10',100.00,'Meters'),(10,8,10,'WPFS1',1000.00,0.00,120.00,12000.000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,'10',100.00,'Meters'),(11,9,10,'WPFS1',1000.00,0.00,39.00,3900.0000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,'10',100.00,'Meters'),(12,10,10,'WPFS1',100.00,0.00,39.00,390.00000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,'10',10.00,'Meters');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemRentalDetails
-- 

/*!40000 ALTER TABLE `TblItemRentalDetails` DISABLE KEYS */;
INSERT INTO `TblItemRentalDetails`(`Id`,`FKHeadId`,`ItemID`,`Qty`,`DayCost`,`TotalCost`,`FKLocationId`) VALUES(1,1,10,1.00,39.00,39.00,1),(2,2,10,10.00,39.00,390.00,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblItemRentalHead
-- 

/*!40000 ALTER TABLE `TblItemRentalHead` DISABLE KEYS */;
INSERT INTO `TblItemRentalHead`(`Id`,`Type`,`SysDate`,`FKClientId`,`RentalStartDate`,`RentalEndDate`,`Amount`,`Discount`,`Gross`,`AdvancePay`,`Balance`,`Description`,`IsSettle`,`FKLocationId`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Cash','2024-04-24 11:51:30.949771',1,'2024-04-18 00:00:00.000000',NULL,39.00,NULL,39.00,2000.00,NULL,'jghfds',0,1,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-24 11:51:29.928024',NULL),(2,'Cash','2024-04-24 11:57:51.056080',2,'2024-04-26 00:00:00.000000',NULL,390.00,NULL,390.00,200.00,NULL,'hg',0,1,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-24 11:57:50.033992',NULL);
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
  `FKLocationId` int DEFAULT NULL,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblPOSBody_POSNO` (`POSNO`),
  CONSTRAINT `FK_TblPOSBody_TblPOSHead_POSNO` FOREIGN KEY (`POSNO`) REFERENCES `TblPOSHead` (`Id`),
  CONSTRAINT `FK_TblPOSBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSBody
-- 

/*!40000 ALTER TABLE `TblPOSBody` DISABLE KEYS */;
INSERT INTO `TblPOSBody`(`Id`,`POSNO`,`ItemID`,`Code`,`Qty`,`FreeQty`,`UnitCost`,`Cost`,`Price`,`ExpDate`,`DisCount`,`IsDelete`,`FKLocationId`,`Qtypiece`,`UnitName`,`UnitSize`) VALUES(3,3,10,'WPFS1',100.00,0.00,39.00,520.00000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,10.00,'Meters','10');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSHead
-- 

/*!40000 ALTER TABLE `TblPOSHead` DISABLE KEYS */;
INSERT INTO `TblPOSHead`(`Id`,`Date`,`Type`,`Description`,`RefInv`,`Created`,`FKClientId`,`Total`,`Discount`,`Gross`,`IsDelete`,`FKLocationId`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(3,'2024-05-13 19:28:10.914632','Cash',NULL,NULL,'d5d765ba-b22f-4e9b-a399-30820a926e82',3,520,0,520,0,1,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-13 19:28:10.287737',NULL);
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
  `IsDelete` tinyint(1) NOT NULL,
  `FKLocationId` int DEFAULT NULL,
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_TblPOSReturnBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblPOSReturnBody_POSReturnNO` (`POSReturnNO`),
  CONSTRAINT `FK_TblPOSReturnBody_TblPOSReturnHead_POSReturnNO` FOREIGN KEY (`POSReturnNO`) REFERENCES `TblPOSReturnHead` (`Id`),
  CONSTRAINT `FK_TblPOSReturnBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSReturnBody
-- 

/*!40000 ALTER TABLE `TblPOSReturnBody` DISABLE KEYS */;
INSERT INTO `TblPOSReturnBody`(`Id`,`POSReturnNO`,`POSBodyKeyNo`,`POSInvoiceNO`,`ItemID`,`Code`,`Qty`,`FreeQty`,`UnitCost`,`Cost`,`Price`,`ExpDate`,`DisCount`,`IsDelete`,`FKLocationId`,`Qtypiece`,`UnitName`,`UnitSize`) VALUES(1,1,3,'INV0003',10,'WPFS1',1.00,0.00,39.00,5.2000000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,0.10,'Meters','10');
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblPOSReturnHead
-- 

/*!40000 ALTER TABLE `TblPOSReturnHead` DISABLE KEYS */;
INSERT INTO `TblPOSReturnHead`(`Id`,`Date`,`Type`,`RefInv`,`Description`,`POSInvoiceNO`,`Created`,`FKClientId`,`Total`,`Discount`,`Gross`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`,`FKLocationId`) VALUES(1,'2024-05-14 14:19:09.249024','Cash','INV0003',NULL,'INV0003','d5d765ba-b22f-4e9b-a399-30820a926e82',3,5.2,0,5.2,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-14 14:19:07.384659',NULL,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStock_Main
-- 

/*!40000 ALTER TABLE `TblStock_Main` DISABLE KEYS */;
INSERT INTO `TblStock_Main`(`ID`,`ItemName`,`ItemDescription`,`ItemCode`,`UnitSize`,`MaxLevel`,`MinLevel`,`ReorderLevel`,`FkUnitId`,`FkCategoryId`,`LastPurchasePrice`,`SellingPrice`,`FkBrandId`,`FkModelTypeId`,`ImageUrl`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`) VALUES(1,'Solvent Cement PVC 15g','Solvent Cement PVC 15g','SC1 ','1',25.00,12.00,10.00,4,2,111.00,148.00,1,8,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-04 10:43:34.978119',NULL),(2,'Solvent Cement PVC 25g','Solvent Cement PVC 25g','SC2','1',25.00,12.00,10.00,5,1,150.00,200.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:22:58.486459',NULL),(3,'Solvent Cement PVC 50g','Solvent Cement PVC 50g','SC3','1',25.00,12.00,10.00,5,1,281.25,375.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:24:09.270307',NULL),(4,'Solvent Cement PVC 75g','Solvent Cement PVC 75g','SC4','1',25.00,12.00,10.00,5,1,401.25,535.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:25:48.422619',NULL),(5,'Solvent Cement PVC 125g','Solvent Cement PVC 125g','SC5','1',25.00,12.00,10.00,5,3,660.00,880.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-04 10:32:18.980163',NULL),(6,'Solvent Cement PVC 250g','Solvent Cement PVC 250g','SC6','1',25.00,12.00,10.00,5,1,1237.50,1650.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:31:38.660050',NULL),(7,'Solvent Cement PVC 500g','Solvent Cement PVC 500g','SC7','1',25.00,12.00,10.00,5,1,2437.50,3250.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:33:28.568424',NULL),(8,'Water Pipe PNt 14 (20mm ) * (1/2) (PE)','Water Pipe PNt 14 (20mm ) * (1/2) (PE)','WP(PNT14PE) ','4',25.00,12.00,10.00,4,2,510.00,680.00,1,1,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-21 14:55:12.795442',NULL),(9,'Water Pipe PNt 14 (20mm ) * (1/2\") (SS)','Water Pipe PNt 14 (20mm ) * (1/2\") (SS)','9','4',25.00,12.00,10.00,4,2,570.00,760.00,1,2,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-18 12:28:14.117903',NULL),(10,'Water Pipe Fittings (20mm ) * (1/2\") ','Water Pipe Fittings (20mm ) * (1/2\") ','WPFS1','10',25.00,12.00,10.00,4,3,39.00,52.00,1,3,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-08 22:03:37.045829',NULL),(11,'Sdsads','adsddasdsdsd','11',NULL,25.00,12.00,10.00,3,4,6500.25,8562.25,1,10,'Select.png',0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-18 14:27:48.046266',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblSupplier
-- 

/*!40000 ALTER TABLE `TblSupplier` DISABLE KEYS */;
INSERT INTO `TblSupplier`(`ID`,`Company`,`Contact`,`Tel`,`Fax`,`Email`,`Mobile`,`Address`,`CreditorLedger`,`AdvanceCreditorLedger`,`Type`,`LedgerCode`,`IsDelete`,`ImageURl`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`,`TblSupplierID`) VALUES(1,'Sudukumudu','Anura',NULL,NULL,NULL,'0776737222','42 /10/2 Hiripitiya, pannipitiya.',NULL,NULL,'Original Equipment Manufacturers (OEM)',NULL,0,'Select.png','d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-04-30 22:53:01.935263',NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblGRNHead
-- 

/*!40000 ALTER TABLE `TblGRNHead` DISABLE KEYS */;
INSERT INTO `TblGRNHead`(`Id`,`Date`,`Pono`,`Type`,`Description`,`GRNType`,`RefInv`,`Created`,`FKSupplier_ID`,`Total`,`Discount`,`Gross`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`,`FKLocationId`) VALUES(7,'2024-05-13 19:11:22.882407',0,'Cash','','Complete Good Received Note','asz','d5d765ba-b22f-4e9b-a399-30820a926e82',1,3900,0,3900,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-13 19:11:22.268505',NULL,1),(8,'2024-05-13 20:17:17.117537',0,'Cash','','Complete Good Received Note','INV00013','d5d765ba-b22f-4e9b-a399-30820a926e82',1,12000,0,12000,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-13 20:17:16.312288',NULL,1),(9,'2024-05-14 09:52:09.204928',0,'Cash','','Complete Good Received Note',' nvbnvbnvbnvb','d5d765ba-b22f-4e9b-a399-30820a926e82',1,3900,0,3900,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-14 09:52:08.408198',NULL,1),(10,'2024-05-14 09:57:23.371003',0,'Cash','','Complete Good Received Note','sdfghj','d5d765ba-b22f-4e9b-a399-30820a926e82',1,390,0,390,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-14 09:57:22.755625',NULL,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStockReturnNoteHead
-- 

/*!40000 ALTER TABLE `TblStockReturnNoteHead` DISABLE KEYS */;
INSERT INTO `TblStockReturnNoteHead`(`ID`,`Date`,`Type`,`Description`,`SRNType`,`RefInv`,`Created`,`FKSupplier_ID`,`Total`,`Discount`,`Gross`,`IsDelete`,`Edit_By`,`Delete_By`,`Edit_Date`,`Delete_Date`,`FKLocationId`) VALUES(1,'2024-05-08 22:57:15.483812','Cash','dfgh','Wrong Item Shipped Return Note','jhdjhdfjgh','d5d765ba-b22f-4e9b-a399-30820a926e82',1,5100,0,5100,1,'d5d765ba-b22f-4e9b-a399-30820a926e82','d5d765ba-b22f-4e9b-a399-30820a926e82','2024-05-08 22:57:14.481193','2024-05-12 20:14:13.300062',1),(2,'2024-05-17 15:31:56.186901','Cash','fghfds','Defective Goods Return Note','sdfghj','d5d765ba-b22f-4e9b-a399-30820a926e82',1,78,0,78,0,'d5d765ba-b22f-4e9b-a399-30820a926e82',NULL,'2024-05-17 15:31:55.169632',NULL,1);
/*!40000 ALTER TABLE `TblStockReturnNoteHead` ENABLE KEYS */;

-- 
-- Definition of TblStockReturnNoteBody
-- 

DROP TABLE IF EXISTS `TblStockReturnNoteBody`;
CREATE TABLE IF NOT EXISTS `TblStockReturnNoteBody` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SRNno` int DEFAULT NULL,
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
  `Qtypiece` decimal(18,2) DEFAULT NULL,
  `UnitName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UnitSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_TblStockReturnNoteBody_FKLocationId` (`FKLocationId`),
  KEY `IX_TblStockReturnNoteBody_SRNno` (`SRNno`),
  CONSTRAINT `FK_TblStockReturnNoteBody_TblStock_Location_FKLocationId` FOREIGN KEY (`FKLocationId`) REFERENCES `TblStock_Location` (`ID`),
  CONSTRAINT `FK_TblStockReturnNoteBody_TblStockReturnNoteHead_SRNno` FOREIGN KEY (`SRNno`) REFERENCES `TblStockReturnNoteHead` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table TblStockReturnNoteBody
-- 

/*!40000 ALTER TABLE `TblStockReturnNoteBody` DISABLE KEYS */;
INSERT INTO `TblStockReturnNoteBody`(`Id`,`SRNno`,`ItemID`,`Code`,`Qty`,`UnitCost`,`Cost`,`Price`,`ExpDate`,`DisCount`,`IsDelete`,`FKLocationId`,`Qtypiece`,`UnitName`,`UnitSize`) VALUES(1,1,8,'WP(PNT14PE) ',40.00,510.00,5100.0000000000000000000000000,680.00000000000000000000000000,NULL,0.00,0,1,NULL,NULL,NULL),(2,2,10,'WPFS1',20.00,39.00,78.000000000000000000000000000,52.000000000000000000000000000,NULL,0.00,0,1,NULL,NULL,NULL);
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
INSERT INTO `TblUsers`(`Id`,`FirstName`,`LastName`,`LastUpdatedBy`,`LastUpdatedDateTime`,`ModifiedBy`,`ModifiedDateTime`,`Address`,`Join_date`,`Designation`,`NIC_no`,`LastLoginDate`,`AcceptTerms`,`Employee_Number`,`ImageURl`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) VALUES('d5d765ba-b22f-4e9b-a399-30820a926e82','Madushan','Chamara',NULL,'2024-02-24 23:28:53.825417',NULL,'0001-01-01 00:00:00.000000','adsdsadasdsdasdsa','2023-07-25 00:00:00.000000','sfdsfsdfdf','942740603v','2024-05-19 19:54:53.919617',1,NULL,'13defffa-2b04-4e2a-9599-1003610bdabf.jpg','shanchamara@gmail.com','SHANCHAMARA@GMAIL.COM','shanchamara@gmail.com','SHANCHAMARA@GMAIL.COM',0,'AQAAAAIAAYagAAAAEBILV+kjV/4bkmSQBbgS4ZNt9RKgg70HSeiLKgkh1e6cbOYuOKBt/YX8IuLW3Cyw3w==','LT4HZAPPLJWHZHBJB6SBLHDEGS6XJ7UR','5bd133c3-b273-4a6d-b9e4-6b300ea3a937','775549788',0,0,'2024-03-17 13:57:33.985666',0,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table RefreshToken
-- 

/*!40000 ALTER TABLE `RefreshToken` DISABLE KEYS */;
INSERT INTO `RefreshToken`(`AppUserId`,`Id`,`Token`,`Expires`,`Created`,`Revoked`) VALUES('d5d765ba-b22f-4e9b-a399-30820a926e82',1,'t7+6uVLZiOo+ctkCbtZMm/6mYe8HowybHIp+a4Q19fg=','2024-04-23 18:53:46.301817','2024-04-22 18:53:46.301998',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',2,'9loGudTwi2KjipYdSzySq2Wvmfiii2KorZSwhIERQoU=','2024-04-25 12:08:26.138396','2024-04-24 12:08:26.138586',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',3,'k/qfCUeKL/8t9g+bKG51QyqbY++lK+QK2WepGLFAVf0=','2024-04-27 11:27:17.138849','2024-04-26 11:27:17.139022',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',4,'bq6law/oaFP/NNpHQWoVoPWpbfiLu4pgK3c/YDofcN0=','2024-05-01 13:59:10.882916','2024-04-30 13:59:10.883036',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',5,'JfT7VohGCnLPyoZhb8YJp98TyeB5I4rTQ6P4sc2IwKs=','2024-05-03 11:37:56.642662','2024-05-02 11:37:56.642805',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',6,'epZdXIpeaOrYiogQpao7FflthYDlCK7ikjSkqAA5RLA=','2024-05-07 04:33:42.195898','2024-05-06 04:33:42.196099',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',7,'LHThR8FrZ3oqDuIjC9UgHfXYSqFzASE6614xgqkjWP4=','2024-05-13 14:31:10.081531','2024-05-12 14:31:10.081705',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',8,'GFDejQcZXLcIXLILqcG3rMfC90S3ERX0DGioi4PNuZc=','2024-05-14 14:43:00.715614','2024-05-13 14:43:00.715886',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',9,'vdswjc7gmBvWXj/CJvNoe5OpRaBzqepw9jePat5nhpM=','2024-05-17 04:31:25.472004','2024-05-16 04:31:25.472162',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',10,'Pm3F+fa2eWV96VkhhZ9odkCkJvkmJHfqcu1lv36SViE=','2024-05-18 13:33:12.478629','2024-05-17 13:33:12.478716',NULL),('d5d765ba-b22f-4e9b-a399-30820a926e82',11,'TgJv/GdtBPNtazjH58+JDFnw7+ZdnvdlMBBu8EO6Ftk=','2024-05-20 03:43:33.505064','2024-05-19 03:43:33.505190',NULL);
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
INSERT INTO `Tbl_User_Role`(`UserId`,`RoleId`) VALUES('d5d765ba-b22f-4e9b-a399-30820a926e82','36c9d5b8-e498-4969-bad4-96a4aef6dd00');
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
INSERT INTO `__EFMigrationsHistory`(`MigrationId`,`ProductVersion`) VALUES('20240422183538_initail','8.0.4'),('20240426151305_changeRentalItem','8.0.4'),('20240506051236_editGrnTempTable','8.0.4'),('20240506060256_editGrnbodyTable','8.0.4'),('20240506060538_editGrnbodyTable1','8.0.4'),('20240506100113_clientedit','8.0.4'),('20240508171919_srn','8.0.4'),('20240513112619_itemposchange','8.0.4'),('20240514034209_editgrntable','8.0.4'),('20240514082739_editgrntable1','8.0.4'),('20240515065537_initialCompanyTable','8.0.4'),('20240518142220_initialTblDatabaseBackupHistory','8.0.4'),('20240518143905_initialTblDatabaseBackupHistory1','8.0.4'),('20240518153150_initialTblDatabaseBackupHistory2','8.0.4');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2024-05-20 10:41:38
-- Total time: 0:0:0:59:376 (d:h:m:s:ms)
