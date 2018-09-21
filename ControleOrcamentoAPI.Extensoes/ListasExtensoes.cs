using System;
using System.Linq;
using System.Collections.Generic;

namespace ControleOrcamentoAPI.Extensoes
{
    /// <summary>
    /// Extensões para os itepos de lista genérica
    /// </summary>
    public static class ListasExtensoes
    {
        /// <summary>
        /// Responsável por adicionar novos itens em uma lista, porém o método permite adicinar item unitário, 
        /// não necessitando criar uma array para isto
        /// </summary>
        /// <typeparam name="T">Tipo da lista genérica</typeparam>
        /// <param name="lista">Lista que receberá a extensão</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="itens"/> for null</exception>
        /// <param name="itens">Item ou itens que deseja adicionar na lista</param>
        public static void AdicionarVarios<T>(this IList<T> lista, params T[] itens)
        {
            if (itens == null)
                throw new ArgumentException("Não enviado a ista de item", nameof(itens));
            itens.ToList().ForEach(e => lista.Add(e));
        }
    }
}
