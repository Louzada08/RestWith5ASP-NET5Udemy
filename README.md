# RestWith5ASP-NET5Udemy
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
