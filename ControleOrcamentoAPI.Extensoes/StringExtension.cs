using System;

namespace ControleOrcamentoAPI.Extensoes
{
    /// <summary>
    /// Responsável por conter os métodos de extensão da classe strig
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Responsável por comparar string ignorando camel case
        /// </summary>
        /// <param name="value"> é o valor que será comparado</param>
        /// <param name="valueCompare"> é o Valor à comparar</param>
        /// <returns>True se o conteúdo for igual ou false se o conteudo for diferente</returns>
        public static bool EqualsIgnoreCase(this string value, string valueCompare)
        {
            if (value == null && valueCompare == null) return true;
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Equals(valueCompare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
