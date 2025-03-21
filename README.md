# Arquitetura - Estrutura
meu-projeto/
├── src/
│   ├── Domain/
│   │   ├── Entities/
│   │   │   ├── Produto.cs
│   │   │   └── Categoria.cs
│   │   ├── ValueObjects/
│   │   │   ├── Preco.cs
│   │   │   └── Estoque.cs
│   │   ├── Aggregates/
│   │   │   └── Produto.cs (Aggregate Root)
│   │   ├── DomainServices/
│   │   │   ├── CalculadoraDeDesconto.cs
│   │   │   └── ServicoDeEstoque.cs
│   │   ├── DomainEvents/
│   │   │   └── ProdutoEstoqueBaixoEvent.cs
│   ├── Application/ (Camada de Casos de Uso - Use Cases)
│   │   ├── ListarProdutos/
│   │   │   ├── ListarProdutos.cs
│   │   │   ├── ListarProdutosInputPort.cs
│   │   │   └── ListarProdutosOutputPort.cs
│   │   └── ...
│   ├── Infrastructure/ (FrameworksAndDrivers)
│   │   ├── Repositories/
│   │   │   └── ProdutoRepository.cs
│   │   ├── Gateways/
│   │   │   └── IProdutoRepository.cs  // Interface agora reside aqui
│   │   └── ...
│   ├── InterfaceAdapter/
│   │   ├── Presenters/
│   │   │   └── ListarProdutosPresenter.cs
│   │   ├── ViewModels/
│   │   │   └── ProdutoViewModel.cs
│   │   └── Mappers/
│   │       └── AutoMapperProfiles.cs
│   ├── WebAPI/
│   │   ├── Controllers/
│   │   │   └── ProdutosController.cs
│   │   ├── Filters/
│   │   │   └── ExceptionFilterAttribute.cs
│   │   └── Program.cs (Configuração da API)
└── ...

Defina esta arquitetura acima
## Definição da Arquitetura do Projeto

A arquitetura do seu projeto segue os princípios da **Clean Architecture** (também conhecida como Hexagonal Architecture ou Ports and Adapters) com influências de **Domain-Driven Design (DDD)**. O objetivo principal é criar um sistema desacoplado, testável, e fácil de manter, separando as preocupações e garantindo que a lógica de negócio central (o Domínio) seja independente de detalhes de implementação externos.

**Visão Geral das Camadas:**

1.  **Domain (Domínio):**
    *   **Responsabilidade:** Contém a lógica de negócio central da aplicação. Representa os conceitos do negócio, as regras e os processos. É a camada mais interna e independente.
    *   **Componentes:**
        *   **Entities (Entidades):** Representam os objetos do negócio com identidade (ex: `Produto`, `Categoria`).
        *   **ValueObjects (Objetos de Valor):** Representam características descritivas do negócio sem identidade própria (ex: `Preco`, `Estoque`). São imutáveis.
        *   **Aggregates (Agregados):** Agrupamentos de entidades e value objects que são tratados como uma unidade.  `Produto` é um Aggregate Root neste caso.
        *   **DomainServices (Serviços de Domínio):** Lógica de negócio complexa que não se encaixa naturalmente em uma entidade ou value object.
        *   **DomainEvents (Eventos de Domínio):**  Notificações que indicam que algo importante aconteceu no domínio.

2.  **Application (Aplicação):**
    *   **Responsabilidade:** Orquestra a lógica de negócio, coordenando as interações entre o Domínio e a Infraestrutura. Implementa os casos de uso da aplicação.
    *   **Componentes:**
        *   **Use Cases (Casos de Uso):** Representam as ações que os usuários podem realizar na aplicação (ex: `ListarProdutos`).  Implementados com padrões como Input Ports e Output Ports para desacoplamento.
        *   **Input Ports (Portas de Entrada):** Interfaces que definem como os casos de uso são acessados.
        *   **Output Ports (Portas de Saída):** Interfaces que definem as dependências que os casos de uso precisam (ex: repositórios).

3.  **Infrastructure (Infraestrutura):**
    *   **Responsabilidade:** Implementa os detalhes técnicos da aplicação, como acesso a dados, comunicação com serviços externos, e envio de e-mails.
    *   **Componentes:**
        *   **Repositories (Repositórios):** Abstraem o acesso aos dados, fornecendo uma interface para persistir e recuperar entidades.
        *   **Gateways (Interfaces de Repositório):**  Definem as interfaces que os repositórios devem implementar.  Isso permite trocar a implementação do banco de dados sem afetar o resto da aplicação.
        *   **Outros Serviços:**  Serviços para lidar com armazenamento de arquivos, envio de e-mails, etc.

4.  **InterfaceAdapter (Adaptador de Interface):**
    *   **Responsabilidade:** Converte os dados entre o formato do Domínio e o formato da camada de Apresentação (WebAPI).
    *   **Componentes:**
        *   **Presenters (Apresentadores):** Formatam os dados para serem exibidos na interface do usuário.
        *   **ViewModels (Modelos de Visualização):** Representam os dados que serão exibidos na interface do usuário.
        *   **Mappers (Mapeadores):** Convertem os dados entre as diferentes camadas (Domínio -> ViewModel, etc.).  AutoMapper é usado para simplificar esse processo.

5.  **WebAPI (Camada de Apresentação):**
    *   **Responsabilidade:** Fornece a interface para os usuários interagirem com a aplicação.
    *   **Componentes:**
        *   **Controllers (Controladores):** Recebem as requisições HTTP e as encaminham para os casos de uso na camada de Aplicação.
        *   **Filters (Filtros):**  Implementam lógica de tratamento de exceções e outras tarefas de pré e pós-processamento.
        *   **Program.cs:** Configuração da API (injeção de dependência, roteamento, etc.).

**Princípios Chave:**

*   **Dependency Rule (Regra da Dependência):** As dependências de código devem apontar para dentro. As camadas externas dependem das camadas internas, mas as camadas internas não dependem das camadas externas.
*   **Separation of Concerns (Separação de Preocupações):** Cada camada tem uma responsabilidade específica e bem definida.
*   **Testability (Testabilidade):** A arquitetura facilita a criação de testes unitários e de integração, pois as camadas são desacopladas e podem ser testadas isoladamente.
*   **Maintainability (Manutenibilidade):** A arquitetura facilita a manutenção e a evolução do sistema, pois as mudanças em uma camada têm pouco impacto nas outras camadas.

Esta arquitetura visa criar um sistema robusto, flexível e fácil de manter, permitindo que você adapte a aplicação às mudanças nos requisitos do negócio sem comprometer a sua integridade.

## O que são camadas e pastas?
**Camadas são conceitos arquiteturais, enquanto pastas são implementações físicas para organizar o código.**

Deixe-me detalhar:

*   **Camadas (Layers):** São divisões lógicas da sua aplicação, cada uma com uma responsabilidade específica. Elas definem a arquitetura geral do seu sistema. Exemplos: Camada de Apresentação (WebAPI), Camada de Aplicação (Casos de Uso), Camada de Domínio (Entidades, Value Objects, Domain Services), Camada de Infraestrutura (Acesso a Dados, Serviços Externos).  As camadas definem *o que* faz cada parte do sistema.
*   **Pastas (Folders):** São diretórios no seu sistema de arquivos que você usa para organizar os arquivos de código que implementam as camadas. Elas são uma forma de estruturar fisicamente o seu projeto para facilitar a manutenção e a colaboração. As pastas definem *onde* o código está localizado.

**Analogia:**

Imagine uma casa.

*   **Camadas:** São os cômodos da casa (sala, cozinha, quartos, banheiros). Cada cômodo tem uma função específica.
*   **Pastas:** São os armários, gavetas e prateleiras dentro de cada cômodo. Eles ajudam a organizar os objetos dentro de cada cômodo.

**Na prática:**

*   Uma única camada pode ser implementada em várias pastas. Por exemplo, a camada de Aplicação pode ter pastas para cada caso de uso (`ListarProdutos`, `CriarPedido`, etc.).
*   Uma única pasta pode conter código de várias camadas, mas isso geralmente não é recomendado, pois pode levar a uma arquitetura confusa e difícil de manter.

**No seu projeto:**

A estrutura que você definiu (com `src`, `Domain`, `Application`, `Infrastructure`, etc.) é uma forma de organizar as pastas para refletir as camadas da sua arquitetura. Cada pasta representa uma camada e contém os arquivos de código que implementam essa camada.

**Em resumo:**

Camadas são a arquitetura, pastas são a organização do código. As pastas são uma ferramenta para implementar e organizar as camadas de forma eficiente.
# Camada Domain/Service
Na camada `DomainServices`, de acordo com a arquitetura que você definiu, o recomendado é implementar lógicas de negócio complexas que envolvem múltiplas entidades de domínio ou que não se encaixam naturalmente em uma única entidade.  Esses serviços devem ser independentes de qualquer framework ou tecnologia específica (como Entity Framework Core ou ASP.NET Core).

**O que implementar na camada `DomainServices`:**

*   **Lógicas de Negócio Complexas:** Operações que requerem a coordenação de várias entidades de domínio para serem executadas corretamente. Por exemplo, um processo de checkout que envolve a verificação de estoque, o cálculo do preço total, a aplicação de descontos e a criação de pedidos.
*   **Regras de Negócio Globais:** Regras que se aplicam a todo o domínio e não são específicas de uma única entidade. Por exemplo, uma regra que define como os descontos são calculados com base no valor total da compra e no status do cliente.
*   **Operações que Envolvem Múltiplas Entidades:** Operações que precisam acessar e modificar dados de várias entidades de domínio para serem concluídas. Por exemplo, um serviço que transfere estoque de um produto para outro.
*   **Cálculos Complexos:** Cálculos que envolvem dados de várias entidades e requerem uma lógica complexa para serem realizados. Por exemplo, um serviço que calcula o custo total de um pedido, incluindo impostos, frete e descontos.
*   **Validações Complexas:** Validações que envolvem dados de várias entidades e requerem uma lógica complexa para serem realizadas. Por exemplo, uma validação que verifica se um cliente tem permissão para fazer um pedido com base em seu histórico de compras e status de crédito.

**Exemplos com base na sua estrutura:**

*   **`CalculadoraDeDesconto.cs`:**  Já está bem posicionado. Ele calcula descontos com base em regras de negócio que podem envolver o produto, a categoria, o cliente, etc.
*   **`ServicoDeEstoque.cs`:** Também está bem posicionado. Ele gerencia o estoque de produtos, verificando a disponibilidade, reservando itens e atualizando o estoque após uma compra.
*   **`ServicoDeRecomendacao.cs`:** (Exemplo) Um serviço que recomenda produtos aos clientes com base em seu histórico de compras, preferências e produtos similares.
*   **`ServicoDeProcessamentoDePedido.cs`:** (Exemplo) Um serviço que coordena todo o processo de criação de um pedido, desde a verificação de estoque até a geração da fatura.

**O que *não* implementar na camada `DomainServices`:**

*   **Acesso a dados:** A camada `DomainServices` não deve ter conhecimento de como os dados são armazenados ou acessados. Isso é responsabilidade da camada `Infrastructure`.
*   **Lógica de interface do usuário:** A camada `DomainServices` não deve ter conhecimento de como os dados são apresentados aos usuários. Isso é responsabilidade da camada `InterfaceAdapter`.
*   **Lógica de aplicação:** A camada `DomainServices` não deve ter conhecimento de como os casos de uso são implementados. Isso é responsabilidade da camada `Application`.

**Em resumo:**

A camada `DomainServices` é o lugar para implementar a lógica de negócio complexa que é central para o seu domínio. Ela deve ser independente de qualquer framework ou tecnologia específica e deve se concentrar em resolver problemas de negócio, não problemas técnicos.

# DbContext aplicação
O `DbContext` (do Entity Framework Core, por exemplo) deve ser colocado na camada **Infrastructure**.

**Justificativa:**

*   **Detalhes de Implementação:** O `DbContext` é uma classe que representa a conexão com o banco de dados e a forma como os dados são persistidos. Isso é um detalhe de implementação da camada de infraestrutura. O domínio (e as camadas superiores) não devem ter conhecimento de como os dados são armazenados.
*   **Dependência Externa:** O `DbContext` depende de um provedor de banco de dados específico (SQL Server, PostgreSQL, etc.). Colocá-lo em uma camada superior introduziria uma dependência desnecessária no domínio.
*   **Responsabilidade da Infraestrutura:** A camada `Infrastructure` é responsável por lidar com todos os detalhes de acesso a dados, incluindo a conexão com o banco de dados, a execução de consultas e a persistência de dados.

**Estrutura:**

Dentro da camada `Infrastructure`, você pode criar uma pasta chamada `Data` ou `Persistence` para organizar o `DbContext` e classes relacionadas. Por exemplo:

```
Infrastructure/
├── Data/
│   ├── ApplicationDbContext.cs  (Seu DbContext)
│   ├── Migrations/  (Pastas geradas pelo Entity Framework Core)
│   └── ...
├── Repositories/
│   └── ProdutoRepository.cs
├── Gateways/
│   └── IProdutoRepository.cs
└── ...
```

**Importante:**

*   O `DbContext` deve ser registrado como um serviço no seu contêiner de injeção de dependência (geralmente no `Program.cs` da sua WebAPI).
*   Os repositórios (como `ProdutoRepository.cs`) usarão o `DbContext` para acessar os dados do banco de dados.

Em resumo, o `DbContext` é um componente da infraestrutura e deve ser colocado lá para manter a separação de responsabilidades e a independência do domínio.
