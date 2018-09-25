namespace ControleOrcamentoAPI.Models.Mapper
{
    /// <summary>
    /// Classe utilizada para carregar a definição do mapeamento para usar o AutoMapper
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Responsável por registrar o mapeamento
        /// </summary>
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banco, Banco>()
                .ForMember(m => m.ID, o => o.Ignore())
                .ForMember(m => m.UsuarioInclusao, o => o.Ignore())
                .ForMember(m => m.DataInclusao, o => o.Ignore())
                .ForMember(m => m.UsuarioAlteracao, o => o.Ignore())
                .ForMember(m => m.DataAlteracao, o => o.Ignore())
                .ForMember(m => m.UsuarioCancelamento, o => o.Ignore())
                .ForMember(m => m.DataCancelamento, o => o.Ignore());

                cfg.CreateMap<Agencia, Agencia>()
                .ForMember(m => m.ID, o => o.Ignore())
                .ForMember(m => m.UsuarioInclusao, o => o.Ignore())
                .ForMember(m => m.DataInclusao, o => o.Ignore())
                .ForMember(m => m.UsuarioAlteracao, o => o.Ignore())
                .ForMember(m => m.DataAlteracao, o => o.Ignore())
                .ForMember(m => m.UsuarioCancelamento, o => o.Ignore())
                .ForMember(m => m.DataCancelamento, o => o.Ignore());

                cfg.CreateMap<Usuario, Usuario>()
               .ForMember(m => m.ID, o => o.Ignore())
               .ForMember(m => m.UsuarioInclusao, o => o.Ignore())
               .ForMember(m => m.DataInclusao, o => o.Ignore())
               .ForMember(m => m.UsuarioAlteracao, o => o.Ignore())
               .ForMember(m => m.DataAlteracao, o => o.Ignore())
               .ForMember(m => m.UsuarioCancelamento, o => o.Ignore())
               .ForMember(m => m.DataCancelamento, o => o.Ignore())
               .ForMember(m => m.DataVerificacao, o => o.Ignore())
               .ForMember(m => m.UsuarioBloqueio, o => o.Ignore())
               .ForMember(m => m.DataBloqueio, o => o.Ignore())
               .ForMember(m => m.Claim, o => o.Ignore())
               .ForMember(m => m.Senha, o => o.Ignore())
               .ForMember(m => m.Salt, o => o.Ignore());
            });
        }
    }
}
