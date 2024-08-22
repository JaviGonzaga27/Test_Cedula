Feature: Editar Cliente

Scenario: Editar datos de un Cliente existente
    Given Existe un cliente en la BDD con los siguientes datos
    | Cedula | Apellidos | Nombres | FechaNacimiento | Mail            | Telefono  | Direccion | Estado | Saldo |
    | 123456 | Perez     | Juan    | 01/01/1990      | jperez@mail.com | 123456789 | Sangolqui | true   | 0     |
    When Editamos los datos del cliente con Cedula "123456"
    | Cedula | Apellidos | Nombres | FechaNacimiento | Mail                | Telefono  | Direccion | Estado | Saldo |
    | 123456 | Perez     | Juan    | 01/01/1990      | juan.perez@mail.com | 987654321 | Quito     | true   | 100   |
    Then El resultado de la búsqueda del cliente editado debe ser
    | Cedula | Apellidos | Nombres | FechaNacimiento | Mail                | Telefono  | Direccion | Estado | Saldo |
    | 123456 | Perez     | Juan    | 01/01/1990      | juan.perez@mail.com | 987654321 | Quito     | true   | 100   |