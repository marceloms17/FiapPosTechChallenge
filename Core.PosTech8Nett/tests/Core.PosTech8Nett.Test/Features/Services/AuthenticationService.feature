Feature: Authentication Service

  Scenario: Login inválido
    Given um LoginRequest com email e senha inválidos
    When o serviço de autenticação é chamado
    Then o sistema deve lançar uma exceção de validação

  Scenario: Login com usuário inexistente
    Given um LoginRequest com usuário não cadastrado
    When o serviço de autenticação é chamado
    Then o resultado deve ser null

  Scenario: Login com senha incorreta
    Given um LoginRequest com senha incorreta
    When o serviço de autenticação é chamado
    Then o resultado deve ser null

  Scenario: Login válido
    Given um LoginRequest válido
    When o serviço de autenticação é chamado
    Then o sistema deve retornar um token
