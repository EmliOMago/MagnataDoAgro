using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controlador para o HUD do cardápio principal do jogo.
/// Gerencia a interação com os botões e navegação entre cenas.
/// </summary>
public class HUDCardapio : MonoBehaviour
{
    /// <summary>
    /// Botão para iniciar um novo jogo.
    /// </summary>
    [SerializeField] public Button botaoNovoJogo;

    /// <summary>
    /// Botão para carregar um jogo existente.
    /// </summary>
    [SerializeField] public Button botaoCarregarJogo;

    /// <summary>
    /// Botão para sair do jogo.
    /// </summary>
    [SerializeField] public Button botaoSair;

    /// <summary>
    /// Método chamado na inicialização do script.
    /// Configura os listeners dos botões.
    /// </summary>
    private void Start()
    {
        // Associa o método AoClicarNovoJogo ao evento de clique do botão Novo Jogo
        botaoNovoJogo.onClick.AddListener(AoClicarNovoJogo);

        // Associa o método AoClicarCarregarJogo ao evento de clique do botão Carregar Jogo
        botaoCarregarJogo.onClick.AddListener(AoClicarCarregarJogo);

        // Associa o método AoClicarSair ao evento de clique do botão Sair
        botaoSair.onClick.AddListener(AoClicarSair);
    }

    /// <summary>
    /// Carrega a cena de novo jogo quando o botão correspondente é clicado.
    /// </summary>
    private void AoClicarNovoJogo()
    {
        // Inicializa o banco de dados com valores padrão
        BancoDeDados.Inicializar();

        // Carrega a cena chamada "CardapioNovoJogo"
        SceneManager.LoadScene("mapaPrincipal");
    }

    /// <summary>
    /// Carrega a cena de seleção de save quando o botão correspondente é clicado.
    /// </summary>
    private void AoClicarCarregarJogo()
    {
        // Carrega a cena chamada "SelecionarJogoSalvo"
        SceneManager.LoadScene("SelecionarJogoSalvo");
    }

    /// <summary>
    /// Encerra a aplicação quando o botão correspondente é clicado.
    /// Funciona tanto no build final quanto no Editor da Unity.
    /// </summary>
    private void AoClicarSair()
    {
        // Fecha a aplicação em builds finais
        Application.Quit();

#if UNITY_EDITOR // Verifica se está executando no Editor da Unity
        // Desativa o modo de play no Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
