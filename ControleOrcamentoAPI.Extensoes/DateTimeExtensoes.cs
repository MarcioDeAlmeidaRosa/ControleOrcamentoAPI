using System;

namespace ControleOrcamentoAPI.Extensoes
{
    /// <summary>
    /// Responsável por atribuir novas funcionalidades ao tipo DateTime
    /// </summary>
    public static class ExtensoesDateTime
    {
        /// <summary>
        /// Retorna o tempo ajustado de fuso horário para um dado de um Utc ou hora local.
        /// A data é convertida primeiro em UTC e depois ajustada.
        /// </summary>
        /// <param name="time"> DateTime que receberá a extensao</param>
        /// <param name="timeZoneId"> ID do time zone</param>
        /// <returns>DateTime convertida em UTC</returns>
        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return time.ToTimeZoneTime(tzi);
        }

        /// <summary>
        /// Retorna o tempo ajustado de fuso horário para um dado de um Utc ou hora local.
        /// A data é convertida primeiro em UTC e depois ajustada.
        /// </summary>
        /// <param name="time"> DateTime que receberá a extensao</param>
        /// <param name="tzi"> Instância de informações do time zone</param>
        /// <returns>DateTime convertida em UTC</returns>
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }
    }
}
