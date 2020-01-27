/* Create Database */
CREATE DATABASE master;

/* Use database */
USE master;

/* Create table */
CREATE TABLE Obliczenia
(
    ObliczeniaId INT NOT NULL IDENTITY PRIMARY KEY,
    X_double DECIMAL(10,2) NOT NULL,
    Y_double DECIMAL(10,2) NOT NULL,
    OPS_string VARCHAR(10) NOT NULL,
);

/* Insert data into table */
INSERT INTO Obliczenia
(
    X_double, [Y_double], [OPS_string]
)
VALUES
    (1.0, 3.5, 'dodaj'),
    (5.6, 8.9, 'odejmij'),
    (10.0, 21, 'pomnoz'),
    (88.3, 200, 'podziel');

CREATE view view_obliczenia 
    AS SELECT X_double,Y_double,OPS_string FROM Obliczenia


/* Use view */
SELECT * FROM view_obliczenia;