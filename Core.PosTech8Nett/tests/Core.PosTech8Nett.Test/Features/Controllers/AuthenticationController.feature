Feature: Authentication Controller

  Scenario: Login com usuário válido
    Given que existe um usuário com e-mail e senha válidos
    When o usuário realiza login
    Then o sistema deve retornar um token

  Scenario: Login com e-mail inexistente
    Given que o e-mail informado não está cadastrado
    When o usuário realiza login
    Then o sistema deve retornar Unauthorized

  Scenario: Login com senha incorreta
    Given que o e-mail está correto mas a senha está errada
    When o usuário realiza login
    Then o sistema deve retornar Unauthorized
