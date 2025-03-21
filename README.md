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
│   ├── Application/ (Camada de Casos de Uso - Use Cases)
│   │   ├── Commands/
│   │   │   ├──Handlers/
│   │   │           |── ProdutoCommandHandler.cs
│   │   ├── Queries/
│   │   └── Services/
│   │        ├── ListarProdutosService.cs
│   ├── Infrastructure/ (FrameworksAndDrivers)
│   │   ├── Repositories/
│   │   │   └── ProdutoRepository.cs
│   │   ├── Gateways/
│   │   │   └── IProdutoRepository.cs  // Interface reside aqui
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

## Definição da Arquitetura do Projeto
A arquitetura acima foi definida como uma variação de **Clean Architecture** (Arquitetura Limpa) com influências de **Domain-Driven Design (DDD)**. Vamos detalhar os principais aspectos:

**Principais Características:**

*   **Separação de Responsabilidades:** A arquitetura é fortemente baseada na separação de responsabilidades, com cada camada tendo uma função específica e bem definida.
*   **Independência de Frameworks:** O núcleo do domínio (camada `Domain`) é independente de qualquer framework ou tecnologia específica, como Entity Framework Core ou ASP.NET Core. Isso facilita a testabilidade e a manutenção.
*   **Fluxo de Dependência:** As dependências fluem em uma única direção: as camadas externas dependem das camadas internas, mas as camadas internas não dependem das camadas externas. Isso garante que as mudanças nas camadas externas não afetem o núcleo do domínio.
*   **Camadas:**
    *   **Domain (Domínio):** Contém as entidades, value objects, aggregates e domain services que representam os conceitos do negócio. É o coração da aplicação.
    *   **Application (Aplicação):** Contém os casos de uso (comandos e queries) que orquestram a lógica de negócio e coordenam as interações entre o domínio e a infraestrutura.
    *   **Infrastructure (Infraestrutura):** Contém os detalhes de implementação, como acesso a dados, serviços externos e comunicação com o sistema de arquivos.
    *   **InterfaceAdapter (Adaptador de Interface):** Contém os componentes que adaptam os dados do domínio para as necessidades específicas da interface de usuário (ViewModels, Presenters, Mappers).
    *   **WebAPI (Apresentação):** Contém a camada de apresentação, que expõe a API para os clientes.

**Padrões e Práticas:**

*   **DDD (Domain-Driven Design):** A estrutura de pastas dentro da camada `Domain` (Entities, ValueObjects, Aggregates, DomainServices) reflete os princípios do DDD.
*   **CQRS (Command Query Responsibility Segregation):** A separação entre `Commands` e `Queries` na camada `Application` sugere a adoção do padrão CQRS, que separa as operações de leitura (queries) das operações de escrita (comandos).
*   **Dependency Inversion Principle (DIP):** O uso de interfaces (como `IProdutoRepository`) na camada `Infrastructure` e a injeção de dependências na camada `Application` demonstram a aplicação do DIP.

**Em resumo:**

A arquitetura é uma implementação bem estruturada de Clean Architecture com influências de DDD e CQRS, projetada para criar aplicações escaláveis, testáveis e fáceis de manter. A separação clara de responsabilidades e a independência do domínio são os principais benefícios dessa abordagem.

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
