using System.ComponentModel;

namespace RestWithASPNET.Domain.Entities;

public enum UserRolesEnum
{
    [Description("Backoffice Administrador")]
    Administrator = 1,
    [Description("Backoffice Analista Backoffice")]
    VendasAnalyst = 2,
    [Description("Vendedores")]
    Seller = 3,
    [Description("Backoffice Analista de compras")]
    PurchaseAnalyst = 4,
    [Description("Backoffice Analista Financeiro")]
    FinancialAnalyst = 5,
    [Description("Backoffice Gerente de Loja")]
    StoreManager = 6,
    [Description("Backoffice Analista de Operacoes")]
    OperationsAnalyst = 7,
}