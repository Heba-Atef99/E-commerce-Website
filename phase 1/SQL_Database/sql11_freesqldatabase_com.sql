-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql11.freesqldatabase.com
-- Generation Time: Dec 10, 2021 at 09:04 PM
-- Server version: 5.5.62-0ubuntu0.14.04.1
-- PHP Version: 7.0.33-0ubuntu0.16.04.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sql11457074`
--
CREATE DATABASE IF NOT EXISTS `sql11457074` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `sql11457074`;

-- --------------------------------------------------------

--
-- Table structure for table `ACCOUNT`
--

CREATE TABLE `ACCOUNT` (
  `Id` int(11) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Pass` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `ACCOUNT`
--

INSERT INTO `ACCOUNT` (`Id`, `Email`, `Pass`) VALUES
(1, 'ahmed@gmail.com', '123'),
(2, 'darin@gmail.com', '123'),
(3, 'nermin@gmail.com', '123'),
(10, 'nour@gmail.com', 'nour1'),
(11, 'mariam@gmail.com', '$2b$10$b/H5TslrabHNI2lrDFCPdOkvmj.JFIII568uwETTApRzwg2YnNQJ.'),
(12, 'omar@gmail.com', 'omar1'),
(13, 'mary@gmail.com', '$2b$10$prU92xqdTsJoo5jyVo/MbOanjYN3JtZNDYHdwL4PnaYiMKSZtHhLK'),
(14, 'nehal@gmail.com', '$2b$10$U/Yhq8xEd0O9ng2C2J1WHuhZwKuXUAnvP05wFOI0R4PmmJTsJ6fYi'),
(15, 'nehal99@gmail.com', '$2b$10$ALiFUGjCzWzu4AQg3MILfe.9O7yeDSQTiWu/pwvkQzOAnvWmEhqR6'),
(16, 'nehall@gmail.com', '$2b$10$eXzjSHkhUkd0S6hZ0KMPb.AG/6I1iwN2EvliEH5KdlEL2phYBZIue'),
(17, 'asmaa@gmail.com', '$2b$10$49DIc1rKoSGMoS9R2IyZJuCnGxJ9Z.8/uvgdcU/LxbJ9fOKS.b7Sy'),
(18, 'ellen@gmail.com', '$2b$10$XWHUonIIwKlOkD1SxeaYZeSUL3Cl.8vY1E3lKcjd8xLybNJVU2WF6'),
(19, 'mohamed@gmail.com', '$2b$10$M7bN4tqSD8d7ihv0SL7DzObUQMGgexAk5mjUxY6ZSacx.67xVEwAW'),
(20, 'mai@gmail.com', '$2b$10$WByA/hAnqqsEP13ii2u8q.CZt/f12EDb5l/mXdEicMsBGUmfrLT3q'),
(21, 'kareem@gmail.com', '$2b$10$ZLKYuyJEDbsIuTyKTrzgPuOc3osVokxRlvx5VvK5/b5PvVZ7e.EMe');

-- --------------------------------------------------------

--
-- Table structure for table `CART`
--

CREATE TABLE `CART` (
  `Id` int(11) NOT NULL,
  `ITEM_Id` int(11) NOT NULL,
  `Account_Id` int(11) NOT NULL,
  `ITEM_Count` int(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `CART`
--

INSERT INTO `CART` (`Id`, `ITEM_Id`, `Account_Id`, `ITEM_Count`) VALUES
(2, 1, 2, 1),
(4, 5, 2, 2),
(6, 1, 2, 1),
(8, 1, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `ITEM`
--

CREATE TABLE `ITEM` (
  `Id` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Type` int(11) NOT NULL,
  `Price` int(9) NOT NULL,
  `Image` varchar(150) DEFAULT '/avatar/default.png',
  `Date` date NOT NULL,
  `Original_Count` int(9) NOT NULL,
  `Available_Count` int(9) NOT NULL,
  `Description` varchar(250) NOT NULL,
  `Owner_Account_Id` int(11) NOT NULL,
  `Status` int(1) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `ITEM`
--

INSERT INTO `ITEM` (`Id`, `Name`, `Type`, `Price`, `Image`, `Date`, `Original_Count`, `Available_Count`, `Description`, `Owner_Account_Id`, `Status`) VALUES
(4, 'Silk Carpet', 6, 450, '', '2021-12-06', 10, 10, '60*60', 13, 1),
(6, 'Dell', 2, 23000, '/Laptops/i7/Dell', '2021-12-08', 6, 6, 'Core i7 Ram 16GB', 3, 1);

-- --------------------------------------------------------

--
-- Table structure for table `PROMOTED_ITEM`
--

CREATE TABLE `PROMOTED_ITEM` (
  `Id` int(11) NOT NULL,
  `Item_Id` int(11) NOT NULL,
  `Promoted_Account_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `PURCHASED_ITEM`
--

CREATE TABLE `PURCHASED_ITEM` (
  `Id` int(11) NOT NULL,
  `Item_Id` int(11) NOT NULL,
  `Buyer_Account_Id` int(11) NOT NULL,
  `Purchased_Count` int(9) NOT NULL,
  `Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `TRANSACTION_HISTORY`
--

CREATE TABLE `TRANSACTION_HISTORY` (
  `Id` int(11) NOT NULL,
  `Receiver_Id` int(11) NOT NULL,
  `Money` int(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TRANSACTION_HISTORY`
--

INSERT INTO `TRANSACTION_HISTORY` (`Id`, `Receiver_Id`, `Money`) VALUES
(1, 2, 14000);

-- --------------------------------------------------------

--
-- Table structure for table `TYPE`
--

CREATE TABLE `TYPE` (
  `Id` int(11) NOT NULL,
  `Type` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TYPE`
--

INSERT INTO `TYPE` (`Id`, `Type`) VALUES
(2, 'Electronics'),
(4, 'Furniture'),
(6, 'Carpets'),
(8, 'Curtains');

-- --------------------------------------------------------

--
-- Table structure for table `USER`
--

CREATE TABLE `USER` (
  `Id` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Phone` int(11) DEFAULT NULL,
  `Address` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `USER`
--

INSERT INTO `USER` (`Id`, `Name`, `Phone`, `Address`) VALUES
(2, 'Darin', 0, 'italy street'),
(4, 'Asmaa', 0, 'omar zaafan'),
(6, 'Ellen', 10105522, 'Cairo Egypt');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `ACCOUNT`
--
ALTER TABLE `ACCOUNT`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `CART`
--
ALTER TABLE `CART`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `ITEM`
--
ALTER TABLE `ITEM`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `PROMOTED_ITEM`
--
ALTER TABLE `PROMOTED_ITEM`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `PURCHASED_ITEM`
--
ALTER TABLE `PURCHASED_ITEM`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `TRANSACTION_HISTORY`
--
ALTER TABLE `TRANSACTION_HISTORY`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `TYPE`
--
ALTER TABLE `TYPE`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `USER`
--
ALTER TABLE `USER`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `ACCOUNT`
--
ALTER TABLE `ACCOUNT`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT for table `CART`
--
ALTER TABLE `CART`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `PROMOTED_ITEM`
--
ALTER TABLE `PROMOTED_ITEM`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `PURCHASED_ITEM`
--
ALTER TABLE `PURCHASED_ITEM`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `TRANSACTION_HISTORY`
--
ALTER TABLE `TRANSACTION_HISTORY`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
