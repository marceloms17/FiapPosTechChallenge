Feature: Admin Controller

  Scenario: Bloquear um usuário com sucesso
    Given que existe um usuário com ID válido
    When o administrador solicita o bloqueio do usuário
    Then o sistema deve retornar NoContent

  Scenario: Deletar um usuário com sucesso
    Given que existe um usuário com ID válido
    When o administrador solicita a exclusão do usuário
    Then o sistema deve retornar NoContent
