# Aplicativo de Previsão do Tempo

## Visão Geral

Este repositório contém uma API em .NET que consome APIs externas de previsão do tempo, do site OpenWeatherMap, para exibir a previsão do tempo para uma cidade específica inserida pelo usuário. A aplicação é capaz de mostrar a previsão do tempo atual, além de previsões para os próximos 5 dias.

## Requisitos Funcionais

	1. *Endpoint para Consulta de Previsão do Tempo Atual:* A aplicação deve permitir ao usuário inserir o nome de uma cidade e, em seguida, exibir a previsão do tempo atual para essa cidade, incluindo temperatura, umidade, descrição do tempo (ensolarado, nublado, etc.), e velocidade do vento.
	2. *Endpoint para Previsão do Tempo Estendida:* Além da previsão atual, a aplicação deve oferecer uma opção para visualizar a previsão estendida para os próximos 5 dias, mostrando as condições diárias esperadas.
	3. *Endpoint para Histórico de Buscas:* A aplicação deve permitir que o histórico de buscas seja consultado, retornando o resumo das ultimas regiões visualizadas.

## Requisitos Não Funcionais

	1. *Configuração:* Permitir que a chave API necessária para a consulta da API externa seja configurada facilmente, sem necessidade de alterações no código.
	2. *Tratamento de Erros:* Implementar tratamento adequado de erros, incluindo erros de rede e erros retornados pela API externa.
	3. *Cache:* Implementar Cache com banco de dados SQL para que as consultas feitas sejam armazenadas e que sejam evitadas novas chamadas para API.
	4. *Testes unitários:* Implementar testes unitários com pelo menos 50% de cobertura.
	5. *Serviço em Background para limpar o cache:* Criar um serviço em background que limpe o cache a cada 1h.

## Decisões Técnicas

	1. *Implementação da arquitetura hexagonal:* As interações desta api são: banco de dados de persistência do histórico, banco de cache, consumo de apis externas e interface de api. Concluí que essas interações com serviços externos se encaixam bem como adaptadores da arquitetura hexagonal, e a implementação desta arquitetura abre espaço para adicionarmos outras interações sem muita dificuldade como exemplo: um adaptador para observabilidade.
	2. *Propriedade Limit da api geocoding:* Para sermos práticos, o endpoint da api externa que fornece os dados da latitude e longitude a partir do nome da cidade retorna apenas 1 ocorrência do nome digitado.
	3. *Intervalo da previsão extendida:* O endpoint que retorna os dados da previsão para os próximos 5 dias retorna a temperatura para cada 3h dos próximos 5 dias.
	4. *SQL Server InMemory:* Com a finalidade de abstrair esforços com infraestrutura de banco de dados e focar na criação da api decidi pelo uso de banco de dados InMemory. Desta forma, os dados persistidos possuem o escopo da execução desta api.

## Uso da Aplicação

Para testar a funcionalidade desta API, primeiro devemos clonar este repositório e escolher o projeto WeatherForecastApi como projeto de inicialização. Depois, devemos criar uma conta gratuita e obter uma chave de api no menu "My API keys" do site: https://home.openweathermap.org/users/sign_up. Em seguida, executamos a aplicação (que abrirá uma página do Swagger no navegador) e executamos as chamadas passando a apiKey obtida. Para acessar os logs, basta acessar a janela cmd que será aberta quando a api estiver executando.

## Funcionamento

### Tela inicial

<img src="assets/swagger_inicio.png">

Ao executar o procedimento acima, pode demorar anguns instantes até a chave de api criada estar disponível para ser usada em uma chamada na api. Nesse caso, ao tentar realizar a chamada, esse será o retorno:

<img src="assets/erro_chave_criando.png">

A solução para esse problema é aguardar alguns instantes até que o site OpenWeather disponibilize a chave.



### Exemplo de chamada para a consulta do clima atual

<img src="assets/current.png">

#### Exemplo de retorno:

```
	{
	  "cidade": "São Paulo",
	  "temperatura": 19.5,
	  "umidade": 92,
	  "descricao": "nublado",
	  "velocidade_do_vento": 3.6,
	  "unidades_de_medida": "Sistema Métrico"
	}
```

### Exemplo de chamada para a consulta da previsão para os próximos 5 dias

<img src="assets/forecast.png">

#### Exemplo de retorno:

```
    {
      "cidade": "São Paulo",
      "unidades_de_medida": "Sistema Métrico",
      "listaDePrevisoes": [
        {
          "data": "2024-04-08 12:00:00",
          "temperatura": 20.6,
          "umidade": 84,
          "descricao": "nublado",
          "velocidade_do_vento": 2.15
        },
        {
          "data": "2024-04-08 15:00:00",
          "temperatura": 25.5,
          "umidade": 60,
          "descricao": "nublado",
          "velocidade_do_vento": 2.3
        },
        {
          "data": "2024-04-08 18:00:00",
          "temperatura": 28.47,
          "umidade": 45,
          "descricao": "nublado",
          "velocidade_do_vento": 1.8
        },
        {
          "data": "2024-04-08 21:00:00",
          "temperatura": 26.26,
          "umidade": 60,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.39
        },
        {
          "data": "2024-04-09 00:00:00",
          "temperatura": 21.46,
          "umidade": 91,
          "descricao": "chuva forte",
          "velocidade_do_vento": 2.7
        },
        {
          "data": "2024-04-09 03:00:00",
          "temperatura": 20.8,
          "umidade": 87,
          "descricao": "chuva moderada",
          "velocidade_do_vento": 2.33
        },
        {
          "data": "2024-04-09 06:00:00",
          "temperatura": 20.36,
          "umidade": 90,
          "descricao": "nublado",
          "velocidade_do_vento": 2.98
        },
        {
          "data": "2024-04-09 09:00:00",
          "temperatura": 19.54,
          "umidade": 91,
          "descricao": "nublado",
          "velocidade_do_vento": 1.56
        },
        {
          "data": "2024-04-09 12:00:00",
          "temperatura": 22.49,
          "umidade": 73,
          "descricao": "nuvens dispersas",
          "velocidade_do_vento": 2
        },
        {
          "data": "2024-04-09 15:00:00",
          "temperatura": 26.15,
          "umidade": 63,
          "descricao": "chuva leve",
          "velocidade_do_vento": 2.34
        },
        {
          "data": "2024-04-09 18:00:00",
          "temperatura": 26.35,
          "umidade": 62,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.41
        },
        {
          "data": "2024-04-09 21:00:00",
          "temperatura": 24.47,
          "umidade": 76,
          "descricao": "chuva leve",
          "velocidade_do_vento": 0.95
        },
        {
          "data": "2024-04-10 00:00:00",
          "temperatura": 22.95,
          "umidade": 81,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.63
        },
        {
          "data": "2024-04-10 03:00:00",
          "temperatura": 21.68,
          "umidade": 83,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.61
        },
        {
          "data": "2024-04-10 06:00:00",
          "temperatura": 20.94,
          "umidade": 84,
          "descricao": "nuvens dispersas",
          "velocidade_do_vento": 1.49
        },
        {
          "data": "2024-04-10 09:00:00",
          "temperatura": 20.08,
          "umidade": 85,
          "descricao": "algumas nuvens",
          "velocidade_do_vento": 1.91
        },
        {
          "data": "2024-04-10 12:00:00",
          "temperatura": 23.32,
          "umidade": 72,
          "descricao": "algumas nuvens",
          "velocidade_do_vento": 1.9
        },
        {
          "data": "2024-04-10 15:00:00",
          "temperatura": 27.04,
          "umidade": 60,
          "descricao": "chuva leve",
          "velocidade_do_vento": 2.53
        },
        {
          "data": "2024-04-10 18:00:00",
          "temperatura": 28.15,
          "umidade": 54,
          "descricao": "chuva leve",
          "velocidade_do_vento": 3.52
        },
        {
          "data": "2024-04-10 21:00:00",
          "temperatura": 24.88,
          "umidade": 76,
          "descricao": "chuva leve",
          "velocidade_do_vento": 0.85
        },
        {
          "data": "2024-04-11 00:00:00",
          "temperatura": 23.91,
          "umidade": 79,
          "descricao": "nuvens dispersas",
          "velocidade_do_vento": 1.06
        },
        {
          "data": "2024-04-11 03:00:00",
          "temperatura": 22.52,
          "umidade": 81,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.29
        },
        {
          "data": "2024-04-11 06:00:00",
          "temperatura": 21.8,
          "umidade": 83,
          "descricao": "chuva leve",
          "velocidade_do_vento": 1.01
        },
        {
          "data": "2024-04-11 09:00:00",
          "temperatura": 20.98,
          "umidade": 86,
          "descricao": "nublado",
          "velocidade_do_vento": 0.75
        },
        {
          "data": "2024-04-11 12:00:00",
          "temperatura": 23.48,
          "umidade": 75,
          "descricao": "nublado",
          "velocidade_do_vento": 1.51
        },
        {
          "data": "2024-04-11 15:00:00",
          "temperatura": 26.01,
          "umidade": 67,
          "descricao": "chuva leve",
          "velocidade_do_vento": 3.24
        },
        {
          "data": "2024-04-11 18:00:00",
          "temperatura": 25.96,
          "umidade": 67,
          "descricao": "chuva leve",
          "velocidade_do_vento": 4.14
        },
        {
          "data": "2024-04-11 21:00:00",
          "temperatura": 22.91,
          "umidade": 80,
          "descricao": "chuva leve",
          "velocidade_do_vento": 4.38
        },
        {
          "data": "2024-04-12 00:00:00",
          "temperatura": 21.48,
          "umidade": 85,
          "descricao": "nublado",
          "velocidade_do_vento": 3.83
        },
        {
          "data": "2024-04-12 03:00:00",
          "temperatura": 20.94,
          "umidade": 86,
          "descricao": "nublado",
          "velocidade_do_vento": 4.04
        },
        {
          "data": "2024-04-12 06:00:00",
          "temperatura": 20.66,
          "umidade": 87,
          "descricao": "nublado",
          "velocidade_do_vento": 3.03
        },
        {
          "data": "2024-04-12 09:00:00",
          "temperatura": 20.18,
          "umidade": 89,
          "descricao": "nublado",
          "velocidade_do_vento": 2.88
        },
        {
          "data": "2024-04-12 12:00:00",
          "temperatura": 21.58,
          "umidade": 81,
          "descricao": "nublado",
          "velocidade_do_vento": 3.13
        },
        {
          "data": "2024-04-12 15:00:00",
          "temperatura": 25.26,
          "umidade": 64,
          "descricao": "nublado",
          "velocidade_do_vento": 3.66
        },
        {
          "data": "2024-04-12 18:00:00",
          "temperatura": 24.01,
          "umidade": 70,
          "descricao": "nublado",
          "velocidade_do_vento": 4.77
        },
        {
          "data": "2024-04-12 21:00:00",
          "temperatura": 21.85,
          "umidade": 81,
          "descricao": "nublado",
          "velocidade_do_vento": 4.97
        },
        {
          "data": "2024-04-13 00:00:00",
          "temperatura": 20.76,
          "umidade": 89,
          "descricao": "nublado",
          "velocidade_do_vento": 4.47
        },
        {
          "data": "2024-04-13 03:00:00",
          "temperatura": 20.55,
          "umidade": 91,
          "descricao": "chuva leve",
          "velocidade_do_vento": 3.58
        },
        {
          "data": "2024-04-13 06:00:00",
          "temperatura": 20.31,
          "umidade": 92,
          "descricao": "nublado",
          "velocidade_do_vento": 3.22
        },
        {
          "data": "2024-04-13 09:00:00",
          "temperatura": 20.25,
          "umidade": 91,
          "descricao": "chuva leve",
          "velocidade_do_vento": 2.86
        }
      ]
    }
```

### Exemplo de chamada para a consulta do histórico de buscas

<img src="assets/history.png">

#### Exemplo de retorno:

```
    [
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:32:52 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:33:57 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:34:07 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:34:18 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:35:31 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:35:48 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:35:50 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:35:53 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:36:08 -03:00"
      },
      {
        "cidade": "São Paulo",
        "data": "08/04/2024 06:40:14 -03:00"
      }
    ]
```

### Exemplo de consulta de logs

<img src="assets/logs.png">

*OBS:* Repare que as logs mostram o comportamento do cache e do serviço de limpeza do cache em 2º plano