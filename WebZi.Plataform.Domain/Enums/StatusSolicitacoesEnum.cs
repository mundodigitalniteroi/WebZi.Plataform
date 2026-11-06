namespace WebZi.Plataform.Domain.Enums;

public enum StatusSolicitacoesEnum : byte
{
    SOLICITADA = 1,
    ACEITA = 2,
    CANCELADA = 3,
    A_CAMINHO = 4,
    NO_LOCAL = 5,
    CARREGADO = 6,
    EM_TRANSITO = 7,
    NO_DEPOSITO = 8,
    FINALIZADO = 9
}