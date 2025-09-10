using System.Collections.Generic;

/// <summary>
/// Classe estatica que armazena e gerencia todos os dados do jogo.
/// Serve como banco de dados centralizado acessivel por todos os scripts.
/// </summary>
public static class BancoDeDados
{
    /// <summary>
    /// Nome do arquivo de save atual do jogador.
    /// </summary>
    public static string Nome = "SavePadrao";

    /// <summary>
    /// Quantidade atual de dinheiro do jogador (valores inteiros sem decimais).
    /// </summary>
    public static int Dinheiro = 0;

    /// <summary>
    /// Array que armazena os niveis atuais das cinco construcoes principais.
    /// Indices: 0-Financeira, 1-Oficina, 2-RecursosHumanos, 3-Plantio, 4-Moradia.
    /// </summary>
    public static int[] Nivel = new int[5] { 1, 1, 1, 1, 1 };

    /// <summary>
    /// Lista que registra a quantidade de dinheiro a cada dois minutos.
    /// Armazena os ultimos 60 registros (duas horas de dados).
    /// </summary>
    public static List<int> Renda = new List<int>();

    /// <summary>
    /// Array que indica o que esta plantado em cada um dos seis campos disponiveis.
    /// Valores possiveis: 0-Vazio, 1-Milho, 2-Ervilha, 3-Cana, 4-Amendoim, 5-Grama.
    /// </summary>
    public static int[] Plantado = new int[6] { 0, 0, 0, 0, 0, 0 };

    /// <summary>
    /// Array que armazena a quantidade de cada um dos cinco produtos no inventario.
    /// Indices: 0-Milho, 1-Ervilha, 2-Cana, 3-Amendoim, 4-Grama.
    /// </summary>
    public static int[] Produtos = new int[5] { 0, 0, 0, 0, 0 };

    /// <summary>
    /// Array que armazena o estado atual das quatro construcoes principais.
    /// Valores possiveis: 0-Danificado, 1-Operante, 2-Melhorado, 3-Otimizado.
    /// Indices: 0-Financeira, 1-RecursosHumanos, 2-Plantio, 3-Moradia.
    /// </summary>
    public static int[] Estado = new int[4] { 1, 1, 1, 1 };

    /// <summary>
    /// Array que armazena o estado dos 32 trabalhadores.
    /// Valores possiveis: 0-Acidentado, 1-Doente, 2-Saudavel, 3-Feliz, 4-Ausente.
    /// </summary>
    public static int[] Custo = new int[32];

    /// <summary>
    /// Contador de desafios concluidos pelo jogador.
    /// </summary>
    public static int Trilha = 0;

    /// <summary>
    /// Inicializa os valores padrao do banco de dados.
    /// Esta funcao deve ser chamada no inicio do jogo ou quando criar um novo save.
    /// </summary>
    public static void Inicializar()
    {
        Nome = "NovoJogador";
        Dinheiro = 1000;

        for (int i = 0; i < Nivel.Length; i++)
        {
            Nivel[i] = 1;
        }

        Renda.Clear();
        Renda.Add(Dinheiro);

        for (int i = 0; i < Plantado.Length; i++)
        {
            Plantado[i] = 0;
        }

        for (int i = 0; i < Produtos.Length; i++)
        {
            Produtos[i] = 0;
        }

        for (int i = 0; i < Estado.Length; i++)
        {
            Estado[i] = 1;
        }

        for (int i = 0; i < Custo.Length; i++)
        {
            Custo[i] = 2;
        }

        Trilha = 0;
    }

    /// <summary>
    /// Adiciona uma nova entrada no registro de renda a cada dois minutos.
    /// Mantem apenas as ultimas 60 entradas (duas horas de dados).
    /// </summary>
    /// <param name="valor">Valor de dinheiro a ser registrado no momento atual.</param>
    public static void RegistrarRenda(int valor)
    {
        Renda.Add(valor);

        if (Renda.Count > 60)
        {
            Renda.RemoveAt(0);
        }
    }

    /// <summary>
    /// Adiciona dinheiro ao valor atual e registra na renda.
    /// </summary>
    /// <param name="quantidade">Quantidade de dinheiro a ser adicionada (valor positivo).</param>
    public static void AdicionarDinheiro(int quantidade)
    {
        Dinheiro += quantidade;
        RegistrarRenda(Dinheiro);
    }

    /// <summary>
    /// Remove dinheiro do valor atual se houver saldo suficiente.
    /// </summary>
    /// <param name="quantidade">Quantidade de dinheiro a ser removida (valor positivo).</param>
    /// <returns>Verdadeiro se a transacao foi bem-sucedida, falso se saldo insuficiente.</returns>
    public static bool RemoverDinheiro(int quantidade)
    {
        if (Dinheiro >= quantidade)
        {
            Dinheiro -= quantidade;
            RegistrarRenda(Dinheiro);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Atualiza o nivel de uma construcao especifica.
    /// </summary>
    /// <param name="indiceConstrucao">Indice da construcao (0-4).</param>
    /// <param name="novoNivel">Novo nivel a ser definido.</param>
    public static void AtualizarNivelConstrucao(int indiceConstrucao, int novoNivel)
    {
        if (indiceConstrucao >= 0 && indiceConstrucao < Nivel.Length)
        {
            Nivel[indiceConstrucao] = novoNivel;
        }
    }

    /// <summary>
    /// Planta um novo tipo de cultivo em um campo especifico.
    /// </summary>
    /// <param name="indiceCampo">Indice do campo (0-5).</param>
    /// <param name="tipoPlanta">Tipo de planta (0-Vazio, 1-Milho, 2-Ervilha, 3-Cana, 4-Amendoim, 5-Grama).</param>
    public static void PlantarNoCampo(int indiceCampo, int tipoPlanta)
    {
        if (indiceCampo >= 0 && indiceCampo < Plantado.Length && tipoPlanta >= 0 && tipoPlanta <= 5)
        {
            Plantado[indiceCampo] = tipoPlanta;
        }
    }

    /// <summary>
    /// Adiciona produtos ao inventario.
    /// </summary>
    /// <param name="tipoProduto">Tipo de produto (0-Milho, 1-Ervilha, 2-Cana, 3-Amendoim, 4-Grama).</param>
    /// <param name="quantidade">Quantidade a ser adicionada.</param>
    public static void AdicionarProduto(int tipoProduto, int quantidade)
    {
        if (tipoProduto >= 0 && tipoProduto < Produtos.Length)
        {
            Produtos[tipoProduto] += quantidade;
        }
    }

    /// <summary>
    /// Remove produtos do inventario se houver quantidade suficiente.
    /// </summary>
    /// <param name="tipoProduto">Tipo de produto (0-Milho, 1-Ervilha, 2-Cana, 3-Amendoim, 4-Grama).</param>
    /// <param name="quantidade">Quantidade a ser removida.</param>
    /// <returns>Verdadeiro se a remocao foi bem-sucedida, falso se quantidade insuficiente.</returns>
    public static bool RemoverProduto(int tipoProduto, int quantidade)
    {
        if (tipoProduto >= 0 && tipoProduto < Produtos.Length && Produtos[tipoProduto] >= quantidade)
        {
            Produtos[tipoProduto] -= quantidade;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Atualiza o estado de uma construcao especifica.
    /// </summary>
    /// <param name="indiceConstrucao">Indice da construcao (0-Financeira, 1-RecursosHumanos, 2-Plantio, 3-Moradia).</param>
    /// <param name="novoEstado">Novo estado (0-Danificado, 1-Operante, 2-Melhorado, 3-Otimizado).</param>
    public static void AtualizarEstadoConstrucao(int indiceConstrucao, int novoEstado)
    {
        if (indiceConstrucao >= 0 && indiceConstrucao < Estado.Length && novoEstado >= 0 && novoEstado <= 3)
        {
            Estado[indiceConstrucao] = novoEstado;
        }
    }

    /// <summary>
    /// Atualiza o estado de um trabalhador especifico.
    /// </summary>
    /// <param name="indiceTrabalhador">Indice do trabalhador (0-31).</param>
    /// <param name="novoEstado">Novo estado (0-Acidentado, 1-Doente, 2-Saudavel, 3-Feliz, 4-Ausente).</param>
    public static void AtualizarEstadoTrabalhador(int indiceTrabalhador, int novoEstado)
    {
        if (indiceTrabalhador >= 0 && indiceTrabalhador < Custo.Length && novoEstado >= 0 && novoEstado <= 4)
        {
            Custo[indiceTrabalhador] = novoEstado;
        }
    }

    /// <summary>
    /// Incrementa o contador de desafios concluidos.
    /// </summary>
    public static void ConcluirDesafio()
    {
        Trilha++;
    }

    /// <summary>
    /// Obtem a quantidade total de trabalhadores com um estado especifico.
    /// </summary>
    /// <param name="estado">Estado a ser contado (0-Acidentado, 1-Doente, 2-Saudavel, 3-Feliz, 4-Ausente).</param>
    /// <returns>Quantidade de trabalhadores no estado especificado.</returns>
    public static int ContarTrabalhadoresPorEstado(int estado)
    {
        int count = 0;
        foreach (int estadoTrabalhador in Custo)
        {
            if (estadoTrabalhador == estado)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Obtem a quantidade total de campos plantados com um tipo especifico.
    /// </summary>
    /// <param name="tipoPlanta">Tipo de planta a ser contado (0-Vazio, 1-Milho, 2-Ervilha, 3-Cana, 4-Amendoim, 5-Grama).</param>
    /// <returns>Quantidade de campos com o tipo especificado.</returns>
    public static int ContarCamposPorTipo(int tipoPlanta)
    {
        int count = 0;
        foreach (int campo in Plantado)
        {
            if (campo == tipoPlanta)
            {
                count++;
            }
        }
        return count;
    }
}

