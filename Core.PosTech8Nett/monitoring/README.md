# Monitoramento com Prometheus e Grafana

Este documento descreve a implementação de monitoramento para o projeto Core.PosTech8Nett usando Prometheus e Grafana.

## Visão Geral

A solução de monitoramento implementada permite:

- Coletar métricas de performance da aplicação .NET
- Monitorar tempo de resposta, taxa de requisições e erros
- Visualizar dados de performance em dashboards personalizados
- Configurar alertas para condições críticas

## Componentes

### 1. Prometheus

O Prometheus é uma solução de monitoramento de código aberto que coleta e armazena métricas em uma série temporal, permitindo consultas, visualizações e alertas.

**Principais características implementadas:**
- Coleta de métricas da API .NET
- Armazenamento de métricas por tempo
- API de consulta para dashboards Grafana

### 2. Grafana

O Grafana é uma plataforma de visualização e análise que permite criar dashboards interativos para métricas coletadas.

**Principais características implementadas:**
- Dashboard para métricas da API .NET
- Visualização de requisições HTTP, tempo de resposta e erros
- Monitoramento de recursos (CPU, memória)

### 3. Integração com .NET

A API .NET foi instrumentada para expor métricas para o Prometheus usando os seguintes componentes:

- **prometheus-net**: Biblioteca principal para integração com Prometheus
- **prometheus-net.AspNetCore**: Middleware para coletar métricas HTTP
- **prometheus-net.DotNetRuntime**: Coletor de métricas do runtime .NET
- **MetricsMiddleware**: Middleware personalizado para métricas específicas da aplicação

## Métricas Implementadas

A implementação atual coleta as seguintes métricas:

### Métricas HTTP
- Taxa de requisições
- Tempo de resposta (histograma)
- Contagem de erros por código de status
- Tamanho das respostas

### Métricas de Runtime
- Uso de CPU
- Uso de memória
- Coleta de lixo (GC)

### Métricas Personalizadas
- Contagem de exceções não tratadas
- Métricas de negócio específicas da aplicação

## Configuração

### Docker Compose

O ambiente é configurado usando Docker Compose com os seguintes serviços:
- **core.postech8nett.api**: A API .NET principal
- **prometheus**: Servidor Prometheus para coleta de métricas
- **grafana**: Servidor Grafana para visualização de dashboards

### Volumes

Os seguintes volumes são configurados:
- **prometheus_data**: Armazena dados de séries temporais do Prometheus
- **grafana_data**: Armazena configurações e dashboards do Grafana

## Uso

Para iniciar o ambiente de monitoramento:

```bash
docker-compose up -d
```

### Acesso aos Serviços

Após iniciar os containers com `docker-compose up -d`, você pode acessar os serviços de monitoramento através dos seguintes endereços:

- **Prometheus**: 
  - URL: http://localhost:9090
  - Interface web para consultas de métricas e verificação de alvos

- **Grafana**: 
  - URL: http://localhost:3000 
  - Credenciais padrão: 
    - Usuário: `admin`
    - Senha: `admin`
  - Na primeira vez que você acessar, será solicitado que altere a senha padrão (recomendado)

- **Métricas da API**: 
  - URL: http://localhost:5000/metrics
  - Endpoint que expõe as métricas em formato Prometheus

### Como Testar a Implementação

Para testar completamente o seu ambiente de monitoramento, siga estes passos:

1. **Iniciar todos os serviços**:
   ```bash
   docker-compose up -d
   ```

2. **Verificar se o Prometheus está conectado à aplicação**:
   - Acesse http://localhost:9090/targets
   - Confirme que o alvo "core.postech8nett.api" está com status "UP"
   - Caso esteja "DOWN", verifique se a aplicação está rodando e se as configurações de rede estão corretas

3. **Explorar métricas no Prometheus**:
   - Acesse http://localhost:9090/graph
   - Experimente consultar algumas métricas como:
     - `http_requests_total` (total de requisições HTTP)
     - `http_request_duration_seconds` (duração das requisições)
     - `process_cpu_seconds_total` (uso de CPU)
     - `dotnet_gc_collections_total` (coletas de lixo)

4. **Gerar carga para testar as métricas**:
   - Faça várias chamadas à API usando ferramentas como Postman, curl ou o Swagger
   - Exemplos de comandos curl:
     ```bash
     # Faça 10 requisições para um endpoint
     for ($i=1; $i -le 10; $i++) { 
         Invoke-RestMethod -Uri http://localhost:5000/api/v1/endpoint 
         Write-Host "Request $i completed" 
     }
     ```

5. **Configurar dashboards no Grafana**:
   - Acesse http://localhost:3000 e faça login
   - Navegue até "Dashboards" > "Browse"
   - Os dashboards pré-configurados devem estar disponíveis
   - Caso não estejam, verifique a configuração da fonte de dados Prometheus

6. **Verificar a fonte de dados do Prometheus no Grafana**:
   - Navegue até "Configuration" > "Data sources"
   - Confirme que há uma fonte de dados Prometheus configurada
   - A URL deve ser `http://prometheus:9090`
   - Clique em "Save & Test" para verificar a conexão

7. **Personalizar dashboards**:
   - Adicione novos painéis aos dashboards existentes
   - Crie consultas PromQL para visualizar métricas específicas
   - Experimente diferentes visualizações (gráficos, medidores, tabelas)

8. **Verificar métricas personalizadas**:
   - Se você implementou métricas personalizadas, verifique se elas estão sendo coletadas
   - Acesse http://localhost:5000/metrics e procure por métricas com prefixos personalizados

### Resolução de Problemas

Se você encontrar problemas com o monitoramento:

1. **Prometheus não consegue acessar a aplicação**:
   - Verifique se a configuração `extra_hosts` está correta no docker-compose.yml
   - Confirme que o endpoint `/metrics` está acessível diretamente
   - Verifique logs do Prometheus: `docker logs prometheus`

2. **Métricas ausentes no Grafana**:
   - Verifique se a fonte de dados Prometheus está configurada corretamente
   - Confirme que as métricas existem no Prometheus antes de tentar visualizá-las
   - Verifique se a consulta PromQL está correta

3. **Aplicação não expõe métricas**:
   - Confirme que os pacotes Prometheus estão instalados corretamente
   - Verifique se o middleware Prometheus está registrado no pipeline HTTP
   - Reinicie a aplicação e verifique os logs
  - URL: http://localhost:5000/metrics
  - Endpoint que expõe as métricas em formato Prometheus
  - Útil para verificar se a API está expondo métricas corretamente

## Dashboards

### Dashboards Pré-configurados

O sistema vem pré-configurado com um dashboard padrão:
- **.NET API Dashboard**: Exibe métricas de requisições HTTP, performance e recursos

### Utilizando o Grafana

1. **Acessando Dashboards**:
   - Faça login no Grafana (http://localhost:3000)
   - No menu lateral esquerdo, clique em "Dashboards"
   - Selecione ".NET API Dashboard" para visualizar as métricas da aplicação

2. **Criando Novos Dashboards**:
   - No menu lateral esquerdo, clique em "Dashboards"
   - Clique no botão "New" e selecione "New Dashboard"
   - Clique em "Add visualization" para adicionar um novo painel
   - Selecione "Prometheus" como fonte de dados
   - Utilize a linguagem PromQL para criar consultas de métricas
   - Configure as opções de visualização conforme necessário
   - Salve o dashboard com um nome descritivo

3. **Explorando Métricas**:
   - No menu lateral esquerdo, clique em "Explore"
   - Selecione "Prometheus" como fonte de dados
   - Explore as métricas disponíveis usando consultas PromQL
   - Exemplos de consultas úteis:
     - `rate(http_requests_total[5m])` - Taxa de requisições nos últimos 5 minutos
     - `histogram_quantile(0.95, sum(rate(app_request_duration_seconds_bucket[5m])) by (le))` - 95º percentil do tempo de resposta
     - `process_resident_memory_bytes` - Uso de memória da aplicação

## Solução de Problemas

### Prometheus

- **Alvo indisponível**:
  - Verifique se a API está em execução e acessível
  - Confirme que o endpoint `/metrics` está respondendo corretamente
  - Verifique a configuração do arquivo `prometheus.yml` para garantir que o alvo está configurado corretamente

- **Métricas ausentes**:
  - Verifique se as métricas estão sendo expostas no endpoint `/metrics` da API
  - Confirme que os nomes das métricas no Prometheus correspondem aos expostos pela API
  - Verifique se o intervalo de scrape é adequado (muito longo pode fazer com que você perca métricas)

### Grafana

- **Fonte de dados não conectada**:
  - Verifique se o Prometheus está em execução e acessível
  - Confirme as configurações da fonte de dados no Grafana (URL do Prometheus)
  - Teste a conexão usando o botão "Save & Test" na configuração da fonte de dados

- **Dashboard não exibe dados**:
  - Verifique se a fonte de dados do Prometheus está funcionando
  - Confirme se as consultas PromQL estão corretas
  - Ajuste o intervalo de tempo para garantir que há dados disponíveis

### Docker

- **Conflito de portas**:
  - Se houver erros indicando que uma porta já está em uso, verifique se outro processo está usando essa porta
  - Você pode alterar as portas no arquivo `docker-compose.yml` se necessário

- **Containers não iniciam**:
  - Execute `docker-compose logs` para verificar os logs de erro
  - Verifique se há conflitos de nome ou rede com containers existentes
  - Tente parar e remover containers antigos com `docker stop $(docker ps -a -q)` e `docker rm $(docker ps -a -q)`

## Próximos Passos

- Implementar alertas para condições críticas
- Adicionar métricas específicas de negócio
- Configurar retenção de dados e backup
- Implementar visualizações avançadas no Grafana
- Configurar alertas por e-mail ou integração com sistemas de comunicação (Slack, Teams, etc.)
- Implementar visualizações avançadas no Grafana
