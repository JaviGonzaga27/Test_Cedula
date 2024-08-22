Feature: Leer Cliente

Scenario: Leer datos de un Cliente existente
    Given Tenemos un cliente en la BDD con los siguientes datos
    | Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado |
    | 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | 1      |
    When Buscamos el cliente por su Cedula "123456"
    Then El resultado debe ser
    | Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado |
    | 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | 1      |