using UnityEngine;

/// <summary>
/// Script responsavel por configurar os dados iniciais do jogo para modo desenvolvedor.
/// Este componente deve ser anexado a um Canvas chamado "HUD_Carregando".
/// </summary>
public class HUDCarregando : MonoBehaviour
{
    /// <summary>
    /// Metodo chamado pela Unity quando o objeto e inicializado.
    /// Configura os valores iniciais do banco de dados para modo desenvolvedor.
    /// </summary>
    private void Start()
    {
        // Define o nome do save como "desenvolvedor"
        BancoDeDados.Nome = "desenvolvedor";

        // Define a quantidade de dinheiro inicial para 200000
        BancoDeDados.Dinheiro = 200000;

        // Configura todos os niveis das construcoes para 3 (evolucao media)
        for (int i = 0; i < BancoDeDados.Nivel.Length; i++)
        {
            BancoDeDados.Nivel[i] = 3;
        }

        // Limpa e inicializa a lista de renda com o valor atual
        BancoDeDados.Renda.Clear();
        BancoDeDados.Renda.Add(BancoDeDados.Dinheiro);

        // Preenche todos os campos com milho (valor 1)
        for (int i = 0; i < BancoDeDados.Plantado.Length; i++)
        {
            BancoDeDados.Plantado[i] = 1;
        }

        // Preenche o inventario com quantidades medias (100 unidades cada)
        for (int i = 0; i < BancoDeDados.Produtos.Length; i++)
        {
            BancoDeDados.Produtos[i] = 100;
        }

        // Configura todos os estados das construcoes para 2 (Melhorado)
        for (int i = 0; i < BancoDeDados.Estado.Length; i++)
        {
            BancoDeDados.Estado[i] = 2;
        }

        // Configura todos os trabalhadores como saudaveis (valor 2)
        for (int i = 0; i < BancoDeDados.Custo.Length; i++)
        {
            BancoDeDados.Custo[i] = 2;
        }

        // Define a quantidade de desafios concluidos para 99
        BancoDeDados.Trilha = 99;

        Debug.Log("Configuracao de desenvolvedor carregada com sucesso!");
    }
}

