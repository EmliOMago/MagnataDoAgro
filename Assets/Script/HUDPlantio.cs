using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsavel por gerenciar a interface de usuario do sistema de plantio.
/// Controla a exibicao das imagens, botoes e menus de selecao de plantio.
/// </summary>
public class HUDPlantio : MonoBehaviour
{
    /// <summary>
    /// Referencia para o menu de selecao de plantio que sera ativado/desativado.
    /// </summary>
    public GameObject hudSelecionarPlantio;
    
    /// <summary>
    /// Referencia para o componente Animator que controla as animacoes da interface.
    /// </summary>
    public Animator animadorPlantio;
    
    /// <summary>
    /// Referencia para a imagem que exibe o nivel atual da construcao de plantio.
    /// </summary>
    public Image imagemNivelPlantio;
    
    /// <summary>
    /// Array de imagens que representam o estado atual de cada area de plantio (1 a 4).
    /// </summary>
    public Image[] imagensAreasPlantio = new Image[4];
    
    /// <summary>
    /// Array de sprites que representam os diferentes tipos de plantas disponiveis para plantio.
    /// Ordem: 0-Vazio, 1-Milho, 2-Ervilha, 3-Cana, 4-Amendoim, 5-Grama.
    /// </summary>
    public Sprite[] spritesPlantas;
    
    /// <summary>
    /// Array de sprites que representam os diferentes niveis da construcao de plantio.
    /// </summary>
    public Sprite[] spritesNiveis;
    
    /// <summary>
    /// Indice da area de plantio atualmente selecionada (-1 significa nenhuma selecionada).
    /// </summary>
    private int areaSelecionada = -1;
    
    /// <summary>
    /// Metodo chamado quando o objeto e inicializado.
    /// Configura o estado inicial da interface de usuario.
    /// </summary>
    void Start()
    {
        // Busca automaticamente todas as referencias aos GameObjects
        BuscarReferenciasAutomaticamente();
    
        // Garante que o menu de selecao de plantio esta oculto no inicio
        if (hudSelecionarPlantio != null)
        {
            hudSelecionarPlantio.SetActive(false);
        }
        
        // Atualiza a interface com os dados atuais do banco de dados
        AtualizarInterface();
    }
    
    /// <summary>
    /// Metodo chamado a cada frame.
    /// Verifica se o usuario clicou fora do menu de selecao para fecha-lo.
    /// </summary>
    void Update()
    {
        // Verifica se o menu de selecao esta ativo e se o usuario clicou com o botao esquerdo do mouse
        if (hudSelecionarPlantio != null && hudSelecionarPlantio.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Verifica se o clique nao foi em nenhum dos botoes do menu de selecao
            if (!VerificarCliqueEmBotaoMenu())
            {
                // Oculta o menu de selecao de plantio
                FecharMenuSelecao();
            }
        }
    }

    private void BuscarReferenciasAutomaticamente()
{
    // Busca o componente Animator no proprio objeto ou em seus filhos
    animadorPlantio = GetComponentInChildren<Animator>();
    
    // Busca o menu de selecao de plantio pelo nome entre os filhos
    Transform menuSelecao = transform.Find("hud_SelecionarPlantio");
    if (menuSelecao != null)
    {
        hudSelecionarPlantio = menuSelecao.gameObject;
    }
    
    // Busca a imagem do nivel da construcao de plantio pelo nome
    Transform nivelTransform = transform.Find("ImagemNivelPlantio");
    if (nivelTransform != null)
    {
        imagemNivelPlantio = nivelTransform.GetComponent<Image>();
    }
    
    // Inicializa o array de imagens das areas de plantio
    imagensAreasPlantio = new Image[4];
    
    // Busca as imagens das areas de plantio pelos nomes padrao
    for (int i = 0; i < 4; i++)
    {
        string nomeArea = "B_AreaDePlantio" + (i + 1);
        Transform areaTransform = transform.Find(nomeArea);
        
        if (areaTransform != null)
        {
            // Busca a imagem filha do botao (que mostra o que esta plantado)
            Transform imagemTransform = areaTransform.Find("Image");
            if (imagemTransform != null)
            {
                imagensAreasPlantio[i] = imagemTransform.GetComponent<Image>();
            }
        }
    }
    
    // Log para debug - verifica se todas as referencias foram encontradas
    Debug.Log("Referencias automaticas configuradas: " +
             "Animator=" + (animadorPlantio != null) + ", " +
             "MenuSelecao=" + (hudSelecionarPlantio != null) + ", " +
             "ImagemNivel=" + (imagemNivelPlantio != null) + ", " +
             "AreasPlantio=" + ContarReferenciasValidas(imagensAreasPlantio) + "/4");
}

/// <summary>
/// Conta quantas referencias validas existem em um array de componentes.
/// </summary>
/// <param name="array">Array de componentes a ser verificado.</param>
/// <returns>Quantidade de referencias nao nulas no array.</returns>
private int ContarReferenciasValidas(Component[] array)
{
    int count = 0;
    foreach (Component comp in array)
    {
        if (comp != null) count++;
    }
    return count;
}


    
    /// <summary>
    /// Atualiza toda a interface de usuario com os dados atuais do banco de dados.
    /// </summary>
    public void AtualizarInterface()
    {
        // Atualiza a imagem do nivel da construcao de plantio
        AtualizarNivelConstrucao();
        
        // Atualiza as imagens de todas as areas de plantio
        AtualizarAreasPlantio();
    }
    
    /// <summary>
    /// Atualiza a imagem que exibe o nivel da construcao de plantio.
    /// </summary>
    private void AtualizarNivelConstrucao()
    {
        // Verifica se a referencia para a imagem existe
        if (imagemNivelPlantio != null && spritesNiveis.Length > 0)
        {
            // Obtem o nivel atual da construcao de plantio do banco de dados (indice 3 no array Nivel)
            int nivel = BancoDeDados.Nivel[3];
            
            // Ajusta o indice para nao ultrapassar os limites do array de sprites
            int indiceSprite = Mathf.Clamp(nivel - 1, 0, spritesNiveis.Length - 1);
            
            // Define o sprite correspondente ao nivel atual
            imagemNivelPlantio.sprite = spritesNiveis[indiceSprite];
        }
    }
    
    /// <summary>
    /// Atualiza as imagens de todas as areas de plantio com base nos dados do banco.
    /// </summary>
    private void AtualizarAreasPlantio()
    {
        // Itera por todas as 4 areas de plantio visiveis na interface
        for (int i = 0; i < 4; i++)
        {
            // Verifica se a referencia para a imagem da area existe
            if (imagensAreasPlantio[i] != null && spritesPlantas.Length > 0)
            {
                // Obtem o tipo de planta atualmente plantada nesta area do banco de dados
                int tipoPlanta = BancoDeDados.Plantado[i];
                
                // Ajusta o indice para nao ultrapassar os limites do array de sprites
                int indiceSprite = Mathf.Clamp(tipoPlanta, 0, spritesPlantas.Length - 1);
                
                // Define o sprite correspondente ao tipo de planta atual
                imagensAreasPlantio[i].sprite = spritesPlantas[indiceSprite];
            }
        }
    }
    
    /// <summary>
    /// Metodo chamado quando um botao de area de plantio e clicado.
    /// Abre o menu de selecao para a area especifica.
    /// </summary>
    /// <param name="numeroArea">Numero da area de plantio (1 a 4).</param>
    public void OnAreaPlantioClicada(int numeroArea)
    {
        // Armazena a area selecionada (subtrai 1 para converter para indice 0-based)
        areaSelecionada = numeroArea - 1;
        
        // Ativa e exibe o menu de selecao de plantio
        AbrirMenuSelecao();
    }
    
    /// <summary>
    /// Abre e exibe o menu de selecao de plantio na posicao adequada.
    /// </summary>
    private void AbrirMenuSelecao()
    {
        // Verifica se a referencia para o menu existe
        if (hudSelecionarPlantio != null)
        {
            // Ativa o objeto do menu
            hudSelecionarPlantio.SetActive(true);
            
            // Atualiza a variavel do animador para mostrar a animacao correta
            if (animadorPlantio != null)
            {
                animadorPlantio.SetInteger("EstadoMenu", 1);
            }
        }
    }
    
    /// <summary>
    /// Fecha e oculta o menu de selecao de plantio.
    /// </summary>
    private void FecharMenuSelecao()
    {
        // Verifica se a referencia para o menu existe
        if (hudSelecionarPlantio != null)
        {
            // Desativa o objeto do menu
            hudSelecionarPlantio.SetActive(false);
            
            // Reseta a area selecionada
            areaSelecionada = -1;
            
            // Atualiza a variavel do animador para esconder o menu
            if (animadorPlantio != null)
            {
                animadorPlantio.SetInteger("EstadoMenu", 0);
            }
        }
    }
    
    /// <summary>
    /// Verifica se o clique do mouse foi em algum botao do menu de selecao.
    /// </summary>
    /// <returns>Verdadeiro se o clique foi em um botao do menu, falso caso contrario.</returns>
    private bool VerificarCliqueEmBotaoMenu()
    {
        // Cria um raycast a partir da posicao do mouse
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        // Verifica se o raycast acertou algum objeto
        if (hit.collider != null)
        {
            // Verifica se o objeto acertado e um botao do menu de selecao
            if (hit.collider.gameObject.transform.IsChildOf(hudSelecionarPlantio.transform))
            {
                return true;
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// Metodo chamado quando um botao de tipo de planta e clicado no menu de selecao.
    /// Planta o tipo selecionado na area previamente escolhida.
    /// </summary>
    /// <param name="tipoPlanta">Tipo de planta a ser plantado (0-Vazio, 1-Milho, 2-Ervilha, 3-Cana, 4-Amendoim, 5-Grama).</param>
    public void OnTipoPlantaSelecionado(int tipoPlanta)
    {
        // Verifica se uma area valida estava selecionada
        if (areaSelecionada >= 0 && areaSelecionada < 4)
        {
            // Atualiza o banco de dados com o novo tipo de planta na area selecionada
            BancoDeDados.PlantarNoCampo(areaSelecionada, tipoPlanta);
            
            // Atualiza a interface para refletir a mudanca
            AtualizarAreasPlantio();
        }
        
        // Fecha o menu de selecao apos a escolha
        FecharMenuSelecao();
    }
    
    /// <summary>
    /// Metodo chamado quando o botao de limpar plantio e clicado.
    /// Define a area selecionada como vazia (tipo 0).
    /// </summary>
    public void OnLimparPlantio()
    {
        // Chama o metodo de selecao de planta com tipo 0 (Vazio)
        OnTipoPlantaSelecionado(0);
    }
}

