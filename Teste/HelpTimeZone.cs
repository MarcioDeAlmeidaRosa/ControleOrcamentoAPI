using System;
using System.Collections.Generic;
using System.Linq;

namespace Teste
{
    public class HelpTimeZone
    {
        /// <summary>
        /// Armazena os registros de todas as time zone
        /// </summary>
        private static readonly IList<TimeZoneInfo> _listaTimezone;

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
        public static dynamic ListaTimeZone()
        {
            return _listaTimezone.Select(tz => new
            {
                Text = tz.DisplayName,
                Value = tz.Id
            }).ToArray();
        }

        /// <summary>
        /// Recupera a time zone padrão da aplicação
        /// </summary>
        /// <returns></returns>
        public static string RecuperarTimeZonePadrao()
        {
            return _listaTimezone.FirstOrDefault(tz => tz.DisplayName == "(UTC-03:00) Brasilia").Id;
        }
    }
}
