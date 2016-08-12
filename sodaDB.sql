CREATE DATABASE IF NOT EXISTS soda;
USE soda;

CREATE TABLE IF NOT EXISTS Sodas
(
    SodaID		INT UNIQUE AUTO_INCREMENT,
    SodaName		NVARCHAR(25),
    purchasFrom		NVARCHAR(25),
    Note		NVARCHAR(200)
);
CREATE TABLE IF NOT EXISTS Inventory
(
    id			INT UNIQUE AUTO_INCREMENT,
    sodaID		INT,
    cancount		INT,
    price		DECIMAL(7,2),
    crv			DECIMAL(7,2),
    worth		DECIMAL(7,2),
    net			DECIMAL(7,2),
    date		DATE
);
CREATE TABLE IF NOT EXISTS CashOut
(
    id			INT UNIQUE AUTO_INCREMENT,
    ernings		DECIMAL(7,2),
    date		DATE
);
CREATE TABLE IF NOT EXISTS Spendings
(
    id			INT UNIQUE AUTO_INCREMENT,
    price		DECIMAL(7,2),
    date		DATE
);
