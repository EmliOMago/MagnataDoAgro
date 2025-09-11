using UnityEngine;

/// <summary>
/// Script responsavel por controlar o comportamento da camera principal em um ambiente 3D.
/// Segue um alvo (player) com suavidade e permite definir limites de movimento para a camera.
/// </summary>
public class HUDConstrucao : MonoBehaviour
{
    /// <summary>
    /// Referencia para o transform do alvo que a camera deve seguir.
    /// O atributo [HideInInspector] oculta esta variavel no Inspector do Unity,
    /// pois ela sera definida automaticamente durante a execucao.
    /// </summary>
    [HideInInspector] public Transform alvo;
    
    /// <summary>
    /// Fator de suavidade do movimento da camera.
    /// Valores mais altos resultam em movimento mais suave, valores mais baixos em movimento mais rigido.
    /// </summary>
    public float suavidade = 1.5f;
    
    /// <summary>
    /// Offset (deslocamento) da camera em relacao ao alvo no espaco 3D.
    /// Define a posicao relativa que a camera mantem em relacao ao objeto seguido.
    /// </summary>
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    
    /// <summary>
    /// Cabecalho para organizar as propriedades de limites no Inspector do Unity.
    /// </summary>
    [Header("Limites da Camera 3D")]
    
    /// <summary>
    /// Flag que indica se os limites de movimento da camera devem ser aplicados.
    /// Quando verdadeiro, a camera nao ultrapassara os limites definidos.
    /// </summary>
    public bool usarLimites = false;
    
    /// <summary>
    /// Valor minimo permitido para a coordenada X da camera no mundo 3D.
    /// </summary>
    public float minX = -10f;
    
    /// <summary>
    /// Valor maximo permitido para a coordenada X da camera no mundo 3D.
    /// </summary>
    public float maxX = 10f;
    
    /// <summary>
    /// Valor minimo permitido para a coordenada Y da camera no mundo 3D.
    /// </summary>
    public float minY = 2f;
    
    /// <summary>
    /// Valor maximo permitido para a coordenada Y da camera no mundo 3D.
    /// </summary>
    public float maxY = 15f;
    
    /// <summary>
    /// Valor minimo permitido para a coordenada Z da camera no mundo 3D.
    /// </summary>
    public float minZ = -20f;
    
    /// <summary>
    /// Valor maximo permitido para a coordenada Z da camera no mundo 3D.
    /// </summary>
    public float maxZ = -5f;
    
    /// <summary>
    /// Metodo chamado uma unica vez antes da primeira execucao do Update.
    /// Inicializa a referencia para o alvo que a camera deve seguir.
    /// </summary>
    void Start()
    {
        // Busca automaticamente o objeto com a tag "Player" na cena
        // e armazena sua referencia de transform para seguir
        alvo = GameObject.FindWithTag("Player").transform;
    }
    
    /// <summary>
    /// Metodo chamado apos todos os updates terem sido processados.
    /// Ideal para movimentos de camera para evitar tremores ou comportamentos erraticos.
    /// </summary>
    void LateUpdate()
    {
        // Verifica se existe um alvo valido para seguir
        if (alvo != null)
        {
            // Calcula a posicao desejada da camera somando o offset a posicao do alvo
            Vector3 posicaoAlvo = alvo.position + offset;
            
            // Aplica limites de movimento se a opcao estiver habilitada
            if (usarLimites)
            {
                // Restringe a posicao X da camera entre os valores minimo e maximo
                posicaoAlvo.x = Mathf.Clamp(posicaoAlvo.x, minX, maxX);
                
                // Restringe a posicao Y da camera entre os valores minimo e maximo
                posicaoAlvo.y = Mathf.Clamp(posicaoAlvo.y, minY, maxY);
                
                // Restringe a posicao Z da camera entre os valores minimo e maximo
                posicaoAlvo.z = Mathf.Clamp(posicaoAlvo.z, minZ, maxZ);
            }
            
            // Interpola suavemente a posicao atual da camera para a posicao desejada
            // Usa Time.deltaTime para garantir movimento suave independente do framerate
            transform.position = Vector3.Lerp(transform.position, posicaoAlvo, suavidade * Time.deltaTime);
            
            // Mantem a camera sempre olhando para o alvo (opcional para camera 3D)
            // transform.LookAt(alvo);
        }
    }
}
