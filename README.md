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
