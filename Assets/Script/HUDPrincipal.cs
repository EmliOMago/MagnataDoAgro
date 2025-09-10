using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controlador principal da HUD que gerencia a visibilidade de canvas filhos,
/// movimentacao de botoes e foco em pontos de interesse.
/// </summary>
public class HUDPrincipal : MonoBehaviour
{
    // Dicionario para armazenar os canvas filhos com seus nomes como chave
    private Dictionary<string, GameObject> canvasFilhos = new Dictionary<string, GameObject>();

    // Dicionario para armazenar os botoes com seus nomes como chave
    private Dictionary<string, GameObject> botoes = new Dictionary<string, GameObject>();

    // Dicionario para armazenar os pontos de interesse com seus nomes como chave
    private Dictionary<string, GameObject> pontosDeInteresse = new Dictionary<string, GameObject>();

    // Referencia para o GameObject PontoDeFoco que sera movido entre os pontos de interesse
    public GameObject pontoDeFoco;

    // Variavel para armazenar a posicao inicial do PontoDeFoco
    private Vector3 posicaoInicialPontoDeFoco;

    // Variavel para armazenar as posicoes originais dos botoes
    private Dictionary<string, Vector3> posicoesOriginaisBotoes = new Dictionary<string, Vector3>();

    // Variavel para armazenar o nome do canvas atualmente visivel
    private string canvasAtualVisivel = string.Empty;

    /// <summary>
    /// Metodo Start e chamado antes do primeiro frame update e inicializa todas as referencias.
    /// </summary>
    void Start()
    {
        // Busca e referencia todos os canvas filhos do HUD_Principal
        BuscarCanvasFilhos();

        // Busca e referencia todos os botoes com prefixo B_
        BuscarBotoes();

        // Busca e referencia todos os pontos de interesse com prefixo PI_
        BuscarPontosDeInteresse();

        // Armazena a posicao inicial do PontoDeFoco para poder retornar depois
        ArmazenarPosicaoInicialPontoDeFoco();

        // Armazena as posicoes originais de todos os botoes
        ArmazenarPosicoesOriginaisBotoes();

        // Configura os listeners de clique para todos os botoes
        ConfigurarListenersBotoes();

        // Inicialmente oculta todos os canvas filhos
        OcultarTodosCanvas();
    }

    /// <summary>
    /// Busca todos os canvas filhos do GameObject HUD_Principal e os armazena no dicionario.
    /// </summary>
    private void BuscarCanvasFilhos()
    {
        // Lista de nomes dos canvas que devem ser buscados
        string[] nomesCanvas = {
            "HUD_Cardapio", "HUD_Construcao", "HUD_Financas", "HUD_Mercado",
            "HUD_Oficina", "HUD_RH", "HUD_Plantio", "HUD_Desafios"
        };

        // Itera sobre cada nome de canvas na lista
        foreach (string nomeCanvas in nomesCanvas)
        {
            // Busca o canvas filho pelo nome
            Transform canvasFilho = transform.Find(nomeCanvas);

            // Verifica se o canvas foi encontrado
            if (canvasFilho != null)
            {
                // Adiciona o canvas ao dicionario com seu nome como chave
                canvasFilhos.Add(nomeCanvas, canvasFilho.gameObject);
            }
            else
            {
                // Log de aviso se o canvas nao for encontrado
                Debug.LogWarning("Canvas nao encontrado: " + nomeCanvas);
            }
        }
    }

    /// <summary>
    /// Busca todos os botoes com prefixo B_ na cena e os armazena no dicionario.
    /// </summary>
    private void BuscarBotoes()
    {
        // Lista de nomes dos botoes que devem ser buscados (prefixo B_ + nome do canvas sem HUD_)
        string[] nomesBotoes = {
            "B_Cardapio", "B_Construcao", "B_Financas", "B_Mercado",
            "B_Oficina", "B_RH", "B_Plantio", "B_Desafios"
        };

        // Itera sobre cada nome de botao na lista
        foreach (string nomeBotao in nomesBotoes)
        {
            // Busca o botao na cena pelo nome
            GameObject botao = GameObject.Find(nomeBotao);

            // Verifica se o botao foi encontrado
            if (botao != null)
            {
                // Adiciona o botao ao dicionario com seu nome como chave
                botoes.Add(nomeBotao, botao);
            }
            else
            {
                // Log de aviso se o botao nao for encontrado
                Debug.LogWarning("Botao nao encontrado: " + nomeBotao);
            }
        }
    }

    /// <summary>
    /// Busca todos os pontos de interesse com prefixo PI_ na cena e os armazena no dicionario.
    /// </summary>
    private void BuscarPontosDeInteresse()
    {
        // Lista de nomes dos pontos de interesse que devem ser buscados (prefixo PI_ + nome do canvas sem HUD_)
        string[] nomesPontosInteresse = {
            "PI_Cardapio", "PI_Construcao", "PI_Financas", "PI_Mercado",
            "PI_Oficina", "PI_RH", "PI_Plantio", "PI_Desafios"
        };

        // Itera sobre cada nome de ponto de interesse na lista
        foreach (string nomePI in nomesPontosInteresse)
        {
            // Busca o ponto de interesse na cena pelo nome
            GameObject pontoInteresse = GameObject.Find(nomePI);

            // Verifica se o ponto de interesse foi encontrado
            if (pontoInteresse != null)
            {
                // Adiciona o ponto de interesse ao dicionario com seu nome como chave
                pontosDeInteresse.Add(nomePI, pontoInteresse);
            }
            else
            {
                // Log de aviso se o ponto de interesse nao for encontrado
                Debug.LogWarning("Ponto de interesse nao encontrado: " + nomePI);
            }
        }

    }

    /// <summary>
    /// Armazena a posicao inicial do PontoDeFoco para poder retornar a ela posteriormente.
    /// </summary>
    private void ArmazenarPosicaoInicialPontoDeFoco()
    {
        // Verifica se o PontoDeFoco foi atribuido no Inspector
        if (pontoDeFoco != null)
        {
            // Armazena a posicao atual do PontoDeFoco como posicao inicial
            posicaoInicialPontoDeFoco = pontoDeFoco.transform.position;
        }
        else
        {
            // Log de erro se o PontoDeFoco nao foi atribuido
            Debug.Log("PontoDeFoco nao atribuido no Inspector!");
            GameObject foco = GameObject.Find("PontoDeFoco");
            pontoDeFoco = foco;
        }
    }

    /// <summary>
    /// Armazena as posicoes originais de todos os botoes para poder retornar a elas posteriormente.
    /// </summary>
    private void ArmazenarPosicoesOriginaisBotoes()
    {
        // Itera sobre todos os botoes no dicionario
        foreach (KeyValuePair<string, GameObject> parBotao in botoes)
        {
            // Armazena a posicao atual do botao como posicao original
            posicoesOriginaisBotoes.Add(parBotao.Key, parBotao.Value.transform.position);
        }
    }

    /// <summary>
    /// Configura os listeners de clique para todos os botoes encontrados.
    /// </summary>
    private void ConfigurarListenersBotoes()
    {
        // Itera sobre todos os botoes no dicionario
        foreach (KeyValuePair<string, GameObject> parBotao in botoes)
        {
            // Obtem o componente Button do botao
            UnityEngine.UI.Button botao = parBotao.Value.GetComponent<UnityEngine.UI.Button>();

            // Verifica se o botao tem componente Button
            if (botao != null)
            {
                // Adiciona um listener que chama o metodo AoClicarBotao quando o botao for clicado
                botao.onClick.AddListener(() => AoClicarBotao(parBotao.Key));
            }
        }
    }

    /// <summary>
    /// Metodo chamado quando qualquer botao e clicado.
    /// </summary>
    /// <param name="nomeBotao">Nome do botao que foi clicado.</param>
    private void AoClicarBotao(string nomeBotao)
    {
        // Converte o nome do botao para o nome do canvas correspondente (substitui "B_" por "HUD_")
        string nomeCanvas = nomeBotao.Replace("B_", "HUD_");

        // Verifica se o canvas clicado e o mesmo que ja esta visivel
        if (canvasAtualVisivel == nomeCanvas)
        {
            // Se for o mesmo, oculta todos os canvas
            OcultarTodosCanvas();
        }
        else
        {
            // Se for diferente, mostra apenas o canvas correspondente
            MostrarApenasUmCanvas(nomeCanvas);
        }

        // Atualiza a posicao dos botoes baseado na visibilidade dos canvas
        AtualizarPosicaoBotoes();

        // Move o PontoDeFoco para o ponto de interesse correspondente
        MoverPontoDeFoco(nomeCanvas.Replace("HUD_", "PI_"));
    }

    /// <summary>
    /// Oculta todos os canvas filhos do HUD_Principal.
    /// </summary>
    private void OcultarTodosCanvas()
    {
        // Itera sobre todos os canvas no dicionario
        foreach (KeyValuePair<string, GameObject> parCanvas in canvasFilhos)
        {
            // Desativa o GameObject do canvas (torna invisivel)
            parCanvas.Value.SetActive(false);
        }

        // Limpa a referencia do canvas atualmente visivel
        canvasAtualVisivel = string.Empty;
    }

    /// <summary>
    /// Mostra apenas um canvas especifico e oculta todos os outros.
    /// </summary>
    /// <param name="nomeCanvas">Nome do canvas que deve ser mostrado.</param>
    private void MostrarApenasUmCanvas(string nomeCanvas)
    {
        // Primeiro oculta todos os canvas
        OcultarTodosCanvas();

        // Verifica se o canvas solicitado existe no dicionario
        if (canvasFilhos.ContainsKey(nomeCanvas))
        {
            // Ativa o GameObject do canvas (torna visivel)
            canvasFilhos[nomeCanvas].SetActive(true);

            // Armazena o nome do canvas que agora esta visivel
            canvasAtualVisivel = nomeCanvas;
        }
        else
        {
            // Log de erro se o canvas nao for encontrado
            Debug.LogError("Canvas nao encontrado: " + nomeCanvas);
        }
    }

    /// <summary>
    /// Atualiza a posicao dos botoes baseado na visibilidade dos canvas.
    /// </summary>
    private void AtualizarPosicaoBotoes()
    {
        // Verifica se algum canvas esta visivel
        if (string.IsNullOrEmpty(canvasAtualVisivel))
        {
            // Se nenhum canvas estiver visivel, move os botoes para a lateral esquerda
            MoverBotoesParaLateralEsquerda();
        }
        else
        {
            // Se algum canvas estiver visivel, retorna os botoes para suas posicoes originais
            RetornarBotoesPosicaoOriginal();
        }
    }

    /// <summary>
    /// Move todos os botoes para a lateral esquerda da tela.
    /// </summary>
    private void MoverBotoesParaLateralEsquerda()
    {
        // Itera sobre todos os botoes no dicionario
        foreach (KeyValuePair<string, GameObject> parBotao in botoes)
        {
            // Obtem a posicao atual do botao
            Vector3 posicaoAtual = parBotao.Value.transform.position;

            // Move o botao para a lateral esquerda (mantem Y e Z, altera apenas X)
            parBotao.Value.transform.position = new Vector3(50f, posicaoAtual.y, posicaoAtual.z);
        }
    }

    /// <summary>
    /// Retorna todos os botoes para suas posicoes originais.
    /// </summary>
    private void RetornarBotoesPosicaoOriginal()
    {
        // Itera sobre todos os botoes no dicionario
        foreach (KeyValuePair<string, GameObject> parBotao in botoes)
        {
            // Verifica se a posicao original deste botao foi armazenada
            if (posicoesOriginaisBotoes.ContainsKey(parBotao.Key))
            {
                // Retorna o botao para sua posicao original
                parBotao.Value.transform.position = posicoesOriginaisBotoes[parBotao.Key];
            }
        }
    }

    /// <summary>
    /// Move o PontoDeFoco para o ponto de interesse correspondente ao canvas visivel.
    /// Se nenhum canvas estiver visivel, retorna o PontoDeFoco para a posicao inicial.
    /// </summary>
    /// <param name="nomePontoInteresse">Nome do ponto de interesse para onde mover.</param>
    private void MoverPontoDeFoco(string nomePontoInteresse)
    {
        // Verifica se o PontoDeFoco foi atribuido
        if (pontoDeFoco == null)
        {
            // Se nao foi atribuido, nao faz nada
            return;
        }

        // Verifica se nenhum canvas esta visivel
        if (string.IsNullOrEmpty(canvasAtualVisivel))
        {
            // Retorna o PontoDeFoco para a posicao inicial
            pontoDeFoco.transform.position = posicaoInicialPontoDeFoco;
            return;
        }

        // Verifica se o ponto de interesse existe no dicionario
        if (pontosDeInteresse.ContainsKey(nomePontoInteresse))
        {
            // Move o PontoDeFoco para a posicao do ponto de interesse
            pontoDeFoco.transform.position = pontosDeInteresse[nomePontoInteresse].transform.position;
        }
        else
        {
            // Log de aviso se o ponto de interesse nao for encontrado
            Debug.LogWarning("Ponto de interesse nao encontrado: " + nomePontoInteresse);

            // Retorna o PontoDeFoco para a posicao inicial como fallback
            pontoDeFoco.transform.position = posicaoInicialPontoDeFoco;
        }
    }
}