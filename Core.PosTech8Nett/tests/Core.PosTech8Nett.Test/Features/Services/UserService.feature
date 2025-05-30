Feature: UserService
  Valida as funcionalidades do serviço de usuários, como bloqueio, exclusão, criação e buscas por ID, email e nickname

  Scenario: Bloqueio de usuário com sucesso
    Given que existe um usuário com ID válido e bloqueio habilitado
    When o serviço de usuário solicita o bloqueio
    Then o repositório deve ser chamado para bloquear o usuário

  Scenario: Exclusão de usuário com sucesso
    Given que existe um usuário com ID válido para exclusão
    When o serviço de usuário solicita a exclusão
    Then o repositório deve ser chamado para deletar o usuário

  Scenario: Criação de usuário com sucesso
    Given que os dados para criação do usuário são válidos
    When o serviço de usuário solicita a criação
    Then o repositório deve ser chamado para criar o usuário

  Scenario: Buscar usuário por e-mail
    Given que existe um usuário com o e-mail informado
    When o serviço de usuário requisita pelo e-mail
    Then o sistema deve retornar os dados do usuário pelo e-mail

  Scenario: Buscar usuário por ID
    Given que existe um usuário com o ID informado
    When o serviço de usuário requisita pelo ID
    Then o sistema deve retornar os dados do usuário pelo ID

  Scenario: Buscar usuário por nickname
    Given que existe um usuário com o nickname informado
    When o serviço de usuário requisita pelo nickname
    Then o sistema deve retornar os dados do usuário pelo nickname
