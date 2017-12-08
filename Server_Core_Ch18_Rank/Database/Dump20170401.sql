
-- use girlaction;
-- use [자기가 만든 데이터베이스 이름];

DROP TABLE IF EXISTS `tb_upgrade_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_upgrade_info` (
  `upgrade_id` int(11) NOT NULL,
  `upgrade_type` varchar(100) DEFAULT NULL,
  `upgrade_level` int(11) DEFAULT NULL,
  `upgrade_amount` int(11) DEFAULT NULL,
  `upgrade_cost` int(11) DEFAULT NULL,
  PRIMARY KEY (`upgrade_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_upgrade_info`
--

LOCK TABLES `tb_upgrade_info` WRITE;
/*!40000 ALTER TABLE `tb_upgrade_info` DISABLE KEYS */;
INSERT INTO `tb_upgrade_info` VALUES (1,'Health',1,5,10),(2,'Health',2,5,10),(3,'Health',3,5,10),(101,'Damage',1,10,20),(102,'Damage',2,10,20),(103,'Damage',3,10,20),(201,'Defense',1,2,10),(202,'Defense',2,2,10),(203,'Defense',3,2,20),(301,'Speed',1,1,5),(302,'Speed',2,1,5),(303,'Speed',3,1,5);
/*!40000 ALTER TABLE `tb_upgrade_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_user`
--

DROP TABLE IF EXISTS `tb_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_user` (
  `user_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `facebook_user_id` varchar(200) DEFAULT '',
  `facebook_name` varchar(200) DEFAULT '',
  `facebook_photo_url` varchar(512) DEFAULT '',
  `point` int(11) DEFAULT '0',
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `access_token` varchar(200) DEFAULT '',
  `diamond` int(11) DEFAULT '0',
  `health` int(11) DEFAULT '1',
  `defense` int(11) DEFAULT '1',
  `damage` int(11) DEFAULT '1',
  `speed` int(11) DEFAULT '10',
  `health_level` int(11) DEFAULT '1',
  `defense_level` int(11) DEFAULT '1',
  `damage_level` int(11) DEFAULT '1',
  `speed_level` int(11) DEFAULT '1',
  `level` int(11) DEFAULT '1',
  `experience` int(11) DEFAULT '0',
  `deleted` tinyint(2) DEFAULT '0',
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_user`
--

LOCK TABLES `tb_user` WRITE;
/*!40000 ALTER TABLE `tb_user` DISABLE KEYS */;
INSERT INTO `tb_user` VALUES (1,'787878','Chris New Server','http://aaa.com',100,'2016-01-01 01:01:01','2016-01-01 01:01:01','123423432',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,'111','Chris','ddd',0,'2017-03-26 04:13:58',NULL,'9e28e8b0-e20b-4ab4-b530-f42dbf7177e0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(21,'123','Chris111','ddd',0,'2017-03-26 04:51:58',NULL,'193faa49-ea28-421c-b95d-f3d66b820967',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(31,'777','Chris777','ddd',0,'2017-03-26 04:54:44',NULL,'294be2ef-65b2-4502-885b-d8c5775e0992',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(41,'','Chris777','ddd',0,'2017-03-26 05:51:43',NULL,'ca350a73-d178-44a3-a678-b61278cd8d83',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(51,'10204997009661738','Chris Song','http://graph.facebook.com/10204997009661738/picture?type=square',0,'2017-03-26 06:03:15','2017-03-26 06:03:15','b1db897c-544d-4be6-8ae4-069acf756496',90,6,1,1,1,2,1,1,1,1,0,0);
/*!40000 ALTER TABLE `tb_user` ENABLE KEYS */;
UNLOCK TABLES;