-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql10.freesqldatabase.com
-- Generation Time: Dec 10, 2021 at 09:02 PM
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
-- Database: `sql10457067`
--

-- --------------------------------------------------------

--
-- Table structure for table `ACCOUNT`
--

CREATE TABLE `ACCOUNT` (
  `Id` int(11) NOT NULL,
  `User_Id` int(11) NOT NULL,
  `Balance` int(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `ACCOUNT`
--

INSERT INTO `ACCOUNT` (`Id`, `User_Id`, `Balance`) VALUES
(1, 1, 560),
(2, 2, 1000),
(3, 3, 2000),
(10, 13, 0),
(11, 15, 0),
(12, 17, 0),
(13, 19, 0),
(14, 21, 0),
(15, 23, 0),
(16, 25, 0),
(17, 4, 0),
(18, 6, 0),
(19, 27, 0),
(20, 29, 0),
(21, 31, 0);

-- --------------------------------------------------------

--
-- Table structure for table `CART`
--

CREATE TABLE `CART` (
  `Id` int(11) NOT NULL,
  `Item_Id` int(11) NOT NULL,
  `Account_Id` int(11) NOT NULL,
  `Item_Count` int(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `CART`
--

INSERT INTO `CART` (`Id`, `Item_Id`, `Account_Id`, `Item_Count`) VALUES
(1, 7, 1, 1),
(3, 19, 3, 1),
(5, 13, 3, 1),
(7, 29, 1, 0),
(9, 29, 1, 0),
(11, 27, 1, 0),
(13, 27, 1, 0),
(15, 39, 1, 1);

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
(1, 'NikeMen', 1, 800, '/Shoes/default.png', '2001-01-08', 5, 5, 'original', 1, 1),
(5, 'Chair', 4, 150, '/Chair/default.png', '2021-12-07', 10, 10, 'leather chair', 1, 1),
(7, 'Fish', 3, 50, '/Food/default.png', '2021-12-07', 10, 10, 'fresh fish', 2, 1),
(9, 'Chips', 3, 15, '/Food/default.png', '2021-12-07', 10, 10, 'salt chips', 3, 1),
(11, 'Silk curtain', 8, 250, '', '2021-12-07', 10, 10, 'red', 13, 1),
(13, 'Lenovo', 2, 14000, NULL, '2021-12-07', 10, 9, 'core i7', 2, 1),
(15, 'Crocs', 1, 1000, '/crocs/default.png', '0001-01-01', 5, 5, 'Original ', 14, 1),
(17, 'Crocs', 1, 800, NULL, '0001-01-01', 5, 5, 'Original', 1, 1),
(19, 'Sketcher', 1, 1800, NULL, '2021-12-07', 5, 5, 'Sale', 16, 1),
(23, 'Shrimps', 3, 150, NULL, '0001-01-01', 10, 10, 'Delecious', 17, 1),
(25, 'Nike', 1, 300, '/avatar/default.png', '2021-12-01', 5, 5, 'good', 3, 1),
(27, 'Croccs', 1, 550, '/croccs/kids', '2021-12-08', 16, 16, 'original', 2, 1),
(29, 'Boots', 1, 350, '/Clark/Boots', '2021-12-08', 13, 13, 'We have Black and Brown', 3, 1),
(31, 'Addidas', 1, 1200, '/Addidas/Sports', '2021-12-08', 2, 2, 'Sizes from 36 to 45', 15, 1),
(33, 'Hp', 2, 18000, '/avatar/default.png', '2021-12-09', 5, 5, 'Core i7, Ram 12GB', 1, 1),
(35, 'Heals', 1, 455, '/avatar/default.png', '0001-01-01', 7, 7, '7 cm height', 14, 1),
(37, 'Jeans', 5, 345, '/avatar/default.png', '0001-01-01', 10, 10, 'Straight leg', 14, 0),
(39, 'NikeMen', 1, 400, '/avatar/default.png', '0001-01-01', 5, 5, '', 14, 1),
(41, 'Steak', 3, 200, '/avatar/default.png', '0001-01-01', 5, 5, '', 14, 1);

-- --------------------------------------------------------

--
-- Table structure for table `PROMOTED_ITEM`
--

CREATE TABLE `PROMOTED_ITEM` (
  `Id` int(11) NOT NULL,
  `Item_Id` int(11) NOT NULL,
  `Promoted_Account_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `PROMOTED_ITEM`
--

INSERT INTO `PROMOTED_ITEM` (`Id`, `Item_Id`, `Promoted_Account_Id`) VALUES
(25, 5, 2),
(29, 1, 3),
(33, 29, 1),
(35, 11, 3),
(37, 23, 3);

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

--
-- Dumping data for table `PURCHASED_ITEM`
--

INSERT INTO `PURCHASED_ITEM` (`Id`, `Item_Id`, `Buyer_Account_Id`, `Purchased_Count`, `Date`) VALUES
(1, 13, 1, 1, '2021-12-01');

-- --------------------------------------------------------

--
-- Table structure for table `TRANSACTION_HISTORY`
--

CREATE TABLE `TRANSACTION_HISTORY` (
  `Id` int(11) NOT NULL,
  `Sender_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TRANSACTION_HISTORY`
--

INSERT INTO `TRANSACTION_HISTORY` (`Id`, `Sender_Id`) VALUES
(1, 1);

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
(1, 'Shoes'),
(3, 'Food'),
(5, 'Clothes'),
(7, 'Bag'),
(9, 'Toys');

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
(1, 'ahmed', 1018952331, 'el shrouk'),
(3, 'nermeen', 1018952321, 'cairo'),
(13, 'nour', 1018952322, 'cairo'),
(15, 'mariam', 1018952333, 'shobra'),
(17, 'omar', 1018952111, 'cairo'),
(19, 'mary', 1018952111, 'alex'),
(21, 'nehal', 1018952156, '4 italy street '),
(23, 'nehaltany', 1018852111, '4 italy street '),
(25, 'nehall', 1017952111, '4 italy street '),
(27, 'mohamed', 1010555, 'Cairo Egypt'),
(29, 'mai', 1550152668, 'egypt'),
(31, 'kareem', 111555444, 'Cairo Egypt');

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
--
-- AUTO_INCREMENT for table `PROMOTED_ITEM`
--
ALTER TABLE `PROMOTED_ITEM`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;
--
-- AUTO_INCREMENT for table `PURCHASED_ITEM`
--
ALTER TABLE `PURCHASED_ITEM`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT for table `TRANSACTION_HISTORY`
--
ALTER TABLE `TRANSACTION_HISTORY`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
