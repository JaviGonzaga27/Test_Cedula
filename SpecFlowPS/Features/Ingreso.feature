Feature: Ingreso

A short summary of the feature

@tag1
Scenario: Ingreso del Cliente
	Given Seleccionar los Datos para ingresar en la BDD
	| Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado |
	| 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | 1      |
	When Ingresamos el registro a la BDD
	| Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado |
	| 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | 1      |
	Then Resultado del registro ingresado a la BDD
	| Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado |
	| 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | 1      |