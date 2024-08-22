Feature: Eliminar Cliente

Scenario: Eliminar un Cliente existente
    Given Existe un cliente en la BDD con Cedula "123456"
    When Eliminamos el cliente con Cedula "123456"
    Then El cliente no debe existir en la BDD
    And Buscamos el cliente por su Cedula "123456"
    And El resultado de la búsqueda debe ser nulo