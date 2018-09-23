using System;
using System.Linq;
using System.Collections.Generic;

namespace ControleOrcamentoAPI.Utils
{
    //Referência
    //https://weblog.west-wind.com/posts/2015/Feb/10/Back-to-Basics-UTC-and-TimeZones-in-NET-Web-Apps

    /// <summary>
    /// Responsável por auxiliar na conversão do TimeZone configurada no usuário da aplicação
    /// </summary>
    public static class HelpTimeZone
    {
        /// <summary>
        /// Armazena os registros de todas as time zone
        /// </summary>
        internal static readonly IList<TimeZoneInfo> _listaTimezone;

        /// <summary>
        /// Carrega as time zone assim que a aplicação é iniciada
        /// </summary>
        static HelpTimeZone()
        {
            _listaTimezone = TimeZoneInfo.GetSystemTimeZones();
        }

        /// <summary>
        /// Lista as time zone com seu display e id
        /// </summary>
        /// <returns></returns>
        public static ListaTimeZone[] ListaTimeZone()
        {
            return _listaTimezone.Select(tz => new ListaTimeZone
            {
                Text = tz.DisplayName,
                Value = tz.Id
            }).OrderBy(o => o.Text).ToArray();
        }

        /// <summary>
        /// Recupera a time zone padrão da aplicação
        /// </summary>
        /// <returns></returns>
        public static string RecuperarIDTimeZonePadrao()
        {
            return _listaTimezone.FirstOrDefault(tz => tz.DisplayName == "(UTC-03:00) Brasilia").Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsIDUTCValid(string id)
        {
            return _listaTimezone.Where(u => u.Id.Equals(id)).Count() > 0;
        }
    }
}
