Feature: User Controller

  Scenario: Buscar usuário por e-mail inexistente
    Given que o e-mail informado não está cadastrado
    When o cliente requisita o usuário por e-mail
    Then o sistema deve retornar OK com null

  Scenario: Buscar usuário por nickname inexistente
    Given que o nickname informado não está cadastrado
    When o cliente requisita o usuário por nickname
    Then o sistema deve retornar OK com null

  Scenario: Buscar usuário por ID inexistente
    Given que o ID informado não está cadastrado
    When o cliente requisita o usuário por ID
    Then o sistema deve retornar OK com null
