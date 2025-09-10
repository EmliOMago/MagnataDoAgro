using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controlador para o HUD do card�pio principal do jogo.
/// Gerencia a intera��o com os bot�es e navega��o entre cenas.
/// </summary>
public class HUDCardapio : MonoBehaviour
{
    /// <summary>
    /// Bot�o para iniciar um novo jogo.
    /// </summary>
    public Button botaoNovoJogo;

    /// <summary>
    /// Bot�o para carregar um jogo existente.
    /// </summary>
    public Button botaoCarregarJogo;

    /// <summary>
    /// Bot�o para sair do jogo.
    /// </summary>
    public Button botaoSair;

    /// <summary>
    /// M�todo chamado na inicializa��o do script.
    /// Configura os listeners dos bot�es.
    /// </summary>
    private void Start()
    {
        // Associa o m�todo AoClicarNovoJogo ao evento de clique do bot�o Novo Jogo
        botaoNovoJogo.onClick.AddListener(AoClicarNovoJogo);

        // Associa o m�todo AoClicarCarregarJogo ao evento de clique do bot�o Carregar Jogo
        botaoCarregarJogo.onClick.AddListener(AoClicarCarregarJogo);

        // Associa o m�todo AoClicarSair ao evento de clique do bot�o Sair
        botaoSair.onClick.AddListener(AoClicarSair);
    }

    /// <summary>
    /// Carrega a cena de novo jogo quando o bot�o correspondente � clicado.
    /// </summary>
    private void AoClicarNovoJogo()
    {
        // Inicializa o banco de dados com valores padr�o
        BancoDeDados.Inicializar();

        // Carrega a cena chamada "CardapioNovoJogo"
        SceneManager.LoadScene("CardapioNovoJogo");
    }

    /// <summary>
    /// Carrega a cena de sele��o de save quando o bot�o correspondente � clicado.
    /// </summary>
    private void AoClicarCarregarJogo()
    {
        // Carrega a cena chamada "SelecionarJogoSalvo"
        SceneManager.LoadScene("SelecionarJogoSalvo");
    }

    /// <summary>
    /// Encerra a aplica��o quando o bot�o correspondente � clicado.
    /// Funciona tanto no build final quanto no Editor da Unity.
    /// </summary>
    private void AoClicarSair()
    {
        // Verifica se est� executando no Editor da Unity
#if UNITY_EDITOR
        // Desativa o modo de play no Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Fecha a aplica��o em builds finais
            Application.Quit();
#endif
    }
}
